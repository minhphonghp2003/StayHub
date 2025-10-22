using StayHub.Domain.Entity.RBAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.Interfaces.Services
{
    public interface IJwtService
    {
        Task<(string, DateTime)> GenerateJwtToken(User user, List<string> roles);
        Task<(string, DateTime)> GenerateRefreshToken(User user);
    }
}
