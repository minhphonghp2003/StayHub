using StayHub.Application.Interfaces.Repository.RBAC;
using Action = StayHub.Domain.Entity.RBAC.Action;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class ActionRepository : Repository<Action>, IActionRepository
    {
        public ActionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
