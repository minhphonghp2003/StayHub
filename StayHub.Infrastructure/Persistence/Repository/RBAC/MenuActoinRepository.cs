using Microsoft.EntityFrameworkCore;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class MenuActionRepository : Repository<MenuAction>,IMenuActionRepository 
    {

        public MenuActionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
