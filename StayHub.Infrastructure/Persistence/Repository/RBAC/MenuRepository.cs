using MediatR;
using Microsoft.EntityFrameworkCore;
using StayHub.Application.DTO.RBAC;
using StayHub.Application.Interfaces.Repository;
using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;
using StayHub.Infrastructure.Migrations;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class MenuRepository(AppDbContext context, IRoleRepository roleRepository)
        : PagingAndSortingRepository<Menu>(context), IMenuRepository
    {
        public async Task<(List<MenuDTO>, int)> GetAllCompactPaginatedMenu(int pageNumber, int pageSize,
            string? search = null, int? menuGroupId = null)
        {
            return await GetManyPagedAsync(pageNumber: pageNumber, pageSize: pageSize,
                filter: e =>
                    e.IsActive == true && !e.SubMenus.Any() && (string.IsNullOrEmpty(search) ||
                                                                e.Name.Contains(search ?? "") || e.Name == search),
                include: e => e.Include(j => j.SubMenus),
                orderBy: e => e.OrderBy(j => j.Order),
                selector: (e, i) => new MenuDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Path = e.Path,
                }
            );
        }

        public async Task<(List<MenuDTO>, int)> GetAllPaginatedMenu(int pageNumber, int pageSize, string? search = null,
            int? menuGroupId = null)
        {
            var (items, count) = await GetManyPagedAsync(
                pageNumber: pageNumber,
                pageSize: pageSize,
                orderBy: e => e.OrderBy((j) => j.Order),
                filter: e =>
                    (string.IsNullOrEmpty(search) || (e.Name.ToLower().Contains(search.ToLower() ?? "") ||
                                                      e.Name.ToLower() == search.ToLower())) &&
                    (menuGroupId == null || e.MenuGroupId == menuGroupId),
                include: e => e.Include(j => j.MenuGroup).Include(j => j.Parent),
                selector: (e, i) => new MenuDTO
                {
                    Id = e.Id,
                    Path = e.Path,
                    Name = e.Name,
                    Description = e.Description,
                    Icon = e.Icon,
                    GroupId = e.MenuGroupId,
                    GroupName = e.MenuGroup?.Name,
                    ParentId = e.ParentId,
                    ParentName = e.Parent?.Name,
                    IsActive = e.IsActive,
                    Order = e.Order,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                });
            return (items, count);
        }

        public async Task<List<int>> GetMenusOfRole(int roleId)
        {
            var result =
                (await GetManyAsync(
                    filter: menu => !menu.SubMenus.Any() && menu.MenuActions.Any() && (menu.MenuActions.All(ma =>
                        ma.Action.RoleActions.Any() && ma.Action.RoleActions.Any(e => e.RoleId == roleId))),
                    selector: (e, i) => e.Id)).ToList();
            return result;
        }

        public async Task<List<int>> GetMenusOfTier(int tierId)
        {
            var result =
                (await GetManyAsync(
                    filter: menu =>
                        !menu.SubMenus.Any() && menu.MenuActions.Any() && (menu.MenuActions.All(ma =>
                            ma.Action.Tiers.Any() && ma.Action.Tiers.Any(e => e.Id == tierId))),
                    selector: (e, i) => e.Id)).ToList();
            return result;
        }


        public async Task<List<MenuGroupDTO>> GetUserMenu(bool isSystemuser, int userId, int? propertyId)
        {
            var result = await _dbSet.Where(e => e.IsActive == true && e.MenuActions.All(ma =>
                        ma.Action.AllowAnonymous || (
                            (!ma.Action.RoleActions.Any() ||
                             ma.Action.RoleActions.Any(ra =>
                                 ra.Role.UserRoles.Any(ur => ur.UserId == userId)
                             )
                            ) &&
                            (propertyId == null || isSystemuser ||
                             ma.Action.Tiers.Any(t =>
                                 t.Properties.Any(p => p.Id == propertyId)
                             )
                            )
                        )
                    )
                )
                .GroupBy(e => new { GroupId = e.MenuGroupId, GroupName = e.MenuGroup.Name })
                .OrderBy(e => e.Min(j => j.Order))
                .Select(g => new MenuGroupDTO
                {
                    Name = g.Key.GroupName,
                    Items = g.Where(e => e.ParentId == null).OrderBy(e => e.Order).Select(e => new MenuDTO
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Path = e.Path,
                        Icon = e.Icon,
                        GroupId = e.MenuGroupId,
                        Children = g.Where(c => c.ParentId == e.Id).Select(c => new MenuDTO
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Path = c.Path,
                            Icon = c.Icon,
                            GroupId = c.MenuGroupId,
                            ParentId = c.ParentId
                        }).ToList(),
                        ParentId = e.ParentId
                    }).ToList()
                }).ToListAsync();
            return result;
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