using StayHub.Application.DTO.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface IMenuRepository : IPagingAndSortingRepository<Menu>
    {
        Task<List<MenuGroupDTO>> GetUserMenu(int userId);
        Task<bool> SetActivated(int id, bool activated);
        Task<(List<MenuDTO>, int)> GetAllPaginatedMenu(int pageNumber, int pageSize, string? search = null, int? menuGroupId = null);
        Task<(List<MenuDTO>, int)> GetAllCompactPaginatedMenu(int pageNumber, int pageSize, string? search = null, int? menuGroupId = null);
        Task<List<int>> GetMenusOfRole(int roleId);
    }
}
