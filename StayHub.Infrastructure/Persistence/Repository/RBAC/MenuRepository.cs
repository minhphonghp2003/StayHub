using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(AppDbContext context) : base(context)
        {
        }
    }
}
