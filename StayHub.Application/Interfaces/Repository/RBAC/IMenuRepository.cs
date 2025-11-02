using StayHub.Application.DTO.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<List<MenuDTO>> GetUserMenu(int userId);
        Task<bool> SetActivated(int id, bool activated);
    }
}
