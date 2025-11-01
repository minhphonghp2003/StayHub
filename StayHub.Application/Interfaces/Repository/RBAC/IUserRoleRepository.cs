using StayHub.Application.DTO.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<IEnumerable<RoleDTO>> GetAllRolesOfUser(int userId);

    }
}
