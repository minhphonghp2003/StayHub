using Microsoft.EntityFrameworkCore;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;
using StayHub.Infrastructure.Migrations;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class MenuRepository(AppDbContext context, IRoleRepository roleRepository) : Repository<Menu>(context), IMenuRepository
    {
        public async Task<List<MenuDTO>> GetUserMenu(int userId)
        {
            var menus = await GetManyAsync(filter: e => e.IsActive == true && e.MenuActions.Any() && e.MenuActions.All(ma => ma.Action.RoleActions.Any(e => e.Role.UserRoles.Any(e => e.UserId == userId))), selector: (e, i) => new MenuDTO
            {
                Id = e.Id,
                Name = e.Name,
                Path = e.Path,
                Icon = e.Icon,
                ParentId = e.ParentId,
            });
            return menus.ToList();
        }

        public async Task<bool> SetActivated(int id, bool activated)
        {
            var menu = new Menu
            {
                Id = id,
                IsActive = activated
            };
            context.Attach(menu);
            context.Entry(menu).Property(x => x.IsActive).IsModified = true;
            await SaveAsync();
            return activated;
        }
    }
}
