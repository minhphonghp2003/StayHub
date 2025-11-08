using Microsoft.EntityFrameworkCore;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;
using StayHub.Infrastructure.Migrations;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class MenuRepository(AppDbContext context, IRoleRepository roleRepository) : Repository<Menu>(context), IMenuRepository
    {
        public async Task<List<MenuGroupDTO>> GetUserMenu(int userId)
        {
            return await _dbSet.Where(e => e.IsActive == true  && e.MenuActions.All(ma => ma.Action.RoleActions.Any(e => e.Role.UserRoles.Any(e => e.UserId == userId)))).GroupBy(e => new { GroupId = e.MenuGroupId, GroupName = e.MenuGroup.Name }).Select(g => new MenuGroupDTO
            {
                Name = g.Key.GroupName,
                Items = g.Select(e => new MenuDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Path = e.Path,
                    Icon = e.Icon,
                    GroupId = e.MenuGroupId,
                    ParentId = e.ParentId
                }).ToList()
            }).ToListAsync();
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
