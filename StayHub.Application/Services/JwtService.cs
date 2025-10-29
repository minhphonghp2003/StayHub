using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Application.Interfaces.Services;
using StayHub.Domain.Entity.RBAC;
using StayHub.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly ISigningKeyRepository signingKeyRepository;
        private readonly HashService _hashService;
        public JwtService(ISigningKeyRepository signingKeyRepo, IConfiguration configuration,HashService hashService)
        {
            _configuration = configuration;
            signingKeyRepository = signingKeyRepo;
            _hashService = hashService;

        }
        public async Task<(string, DateTime)> GenerateJwtToken(User user, List<string> roles)
        {
            var signingKey = await signingKeyRepository.FindOneAsync(k => k.IsActive);
            if (signingKey == null)
            {
                throw new Exception("No active signing key available.");
            }
            var creds = _hashService.SigningKey(privateKey:signingKey.PrivateKey,id:signingKey.Id.ToString());
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            foreach (var userRole in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpireMinutes"]));
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"], claims: claims,
                expires: expires,
                signingCredentials: creds);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenDescriptor);
            return (token, expires);
        }

       
    }
}
