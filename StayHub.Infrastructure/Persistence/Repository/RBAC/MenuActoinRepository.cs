using Microsoft.EntityFrameworkCore;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class MenuActionRepository : Repository<MenuAction>, IMenuActionRepository
    {

        public MenuActionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ActionDTO>> GetAllActionOfMenu(int menuId)
        {
            var result = await GetManyAsync(filter: e => e.MenuId == menuId, selector: (e, i) => new ActionDTO
            {
                Id = e.Id,
                Path = e.Action.Path,
                Method = e.Action.Method,
                AllowAnonymous = e.Action.AllowAnonymous,
                CreatedAt = e.Action.CreatedAt,
                UpdatedAt = e.Action.UpdatedAt
            }, include: e => e.Include(j => j.Action), tracking: false);
            return result;
        }
    }
}
