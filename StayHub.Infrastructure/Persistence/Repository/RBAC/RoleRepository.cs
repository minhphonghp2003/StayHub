using Microsoft.EntityFrameworkCore;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> HasAccessToAction(int userId, string action, string method)
        {
            if (!_dbSet.Any(r => r.RoleActions.Any(ra => ra.Action.Path == action && ra.Action.Method == method)))
            {
                return await Task.FromResult(true);
            }
            return await _dbSet.AnyAsync(r => r.UserRoles.Any(u => u.Id == userId) && r.RoleActions.Any(ra => ra.Action.Path == action && ra.Action.Method == method));
        }
    }
}
