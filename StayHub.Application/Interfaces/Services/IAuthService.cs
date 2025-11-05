using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<(string,DateTime)> GenerateRefreshToken(int userId);
    }
}
