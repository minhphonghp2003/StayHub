using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;
using StayHub.Infrastructure.Persistence.Repository.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Infrastructure.Services
{
    public class KeyRotationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _rotationInterval = TimeSpan.FromDays(7);
        public KeyRotationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Perform the key rotation logic.
                await RotateKeysAsync();
                // Wait for the configured rotation interval before running again.
                await Task.Delay(_rotationInterval, stoppingToken);
            }
        }

        private async Task RotateKeysAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            // Retrieve the database context from the service provider.
            var signingKeyRepo = scope.ServiceProvider.GetRequiredService<ISigningKeyRepository>();
            var validKey = await signingKeyRepo.FindOne((e) => e.IsActive);
            if (validKey == null || validKey.ExpiresAt <= DateTime.UtcNow.AddDays(10))
            {
                if (validKey != null)
                {
                    validKey.IsActive = false;
                    signingKeyRepo.Update(validKey);

                }
                using var rsa = RSA.Create(2048);
                // Export the private key as a Base64-encoded string.
                var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
                // Export the public key as a Base64-encoded string.
                var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
                var newKey = new SigningKey
                {
                    PrivateKey = privateKey,
                    PublicKey = publicKey,
                    IsActive = true,
                    ExpiresAt = DateTime.UtcNow.AddDays(10)
                };
                await signingKeyRepo.AddAsync(newKey);
            }

        }
    }
}
