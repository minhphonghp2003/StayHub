using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<bool> HasAccessToAction(int userId, string action, string method);
    }
}
