using Microsoft.EntityFrameworkCore;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class RoleActionRepository : Repository<RoleAction>, IRoleActionRepository
    {

        public RoleActionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ActionDTO>> GetAllActionOfRole(int roleId)
        {
            return await GetManyAsync(filter: e => e.RoleId == roleId, selector: (e, i) => new ActionDTO
            {
                Id = e.ActionId,
                Path = e.Action.Path,
                Method = e.Action.Method,
                AllowAnonymous = e.Action.AllowAnonymous,
                CreatedAt = e.Action.CreatedAt,
                UpdatedAt = e.Action.UpdatedAt
            }, include: e => e.Include(j => j.Action), tracking: false);

        }
    }
}
