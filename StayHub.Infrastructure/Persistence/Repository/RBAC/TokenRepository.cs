using Microsoft.EntityFrameworkCore;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Token?> GetTokenInfo(string refreshToken)
        {
            return await FindOneEntityAsync(e => e.RefreshToken == refreshToken,include: m=>m.Include(n=>n.User).ThenInclude(u=>u.Profile));
        }
    }
}
