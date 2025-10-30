using Microsoft.EntityFrameworkCore;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class RoleActionRepository : Repository<RoleAction>,IRoleActionRepository 
    {

        public RoleActionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
