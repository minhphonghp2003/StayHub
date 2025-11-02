using Microsoft.EntityFrameworkCore;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class MenuActionRepository : Repository<MenuAction>, IMenuActionRepository
    {
        IUserRoleRepository _userRoleRepository;
        public MenuActionRepository(AppDbContext context, IUserRoleRepository userRoleRepository) : base(context)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IEnumerable<ActionDTO>> GetAllActionOfMenu(int menuId)
        {
            var result = await GetManyAsync(filter: e => e.MenuId == menuId, selector: (e, i) => new ActionDTO
            {
                Id = e.ActionId,
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
