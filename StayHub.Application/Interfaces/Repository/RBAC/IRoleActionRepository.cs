using StayHub.Application.DTO.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface IRoleActionRepository : IRepository<RoleAction>
    {
        Task<IEnumerable<ActionDTO>> GetAllActionOfRole(int roleId);

    }
}
