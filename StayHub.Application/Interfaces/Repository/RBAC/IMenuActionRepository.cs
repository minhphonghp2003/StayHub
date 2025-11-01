using StayHub.Application.DTO.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface IMenuActionRepository : IRepository<MenuAction>
    {

        Task<IEnumerable<ActionDTO>> GetAllActionOfMenu(int menuId);
    }
}
