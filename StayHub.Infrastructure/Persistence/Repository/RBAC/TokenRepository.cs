using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(AppDbContext context) : base(context)
        {
        }
    }
}
