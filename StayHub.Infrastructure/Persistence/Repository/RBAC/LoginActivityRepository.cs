using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;
using Action = StayHub.Domain.Entity.RBAC.Action;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class LoginActivityRepository : Repository<LoginActivity>, ILoginActivityRepository
    {
        public LoginActivityRepository(AppDbContext context) : base(context)
        {
        }
    }
}
