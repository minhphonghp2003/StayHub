using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Application.Interfaces.Services;
using StayHub.Domain.Entity.RBAC;
using Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.Services
{
    public class AuthService(ITokenRepository tokenRepository, IJwtService jwtService, HashService hashService) : IAuthService
    {

        public async Task<(string, DateTime)> GenerateRefreshToken(int userId)
        {
            string newToken;
            DateTime expires = DateTime.UtcNow.AddDays(7);
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                newToken = hashService.HashToken(Convert.ToBase64String(randomNumber));
            }

            await tokenRepository.AddAsync(new Domain.Entity.RBAC.Token
            {
                UserId = userId,
                RefreshToken = newToken,
                ExpireDate = expires,
            });
            return (newToken, expires);
        }
    }
}
