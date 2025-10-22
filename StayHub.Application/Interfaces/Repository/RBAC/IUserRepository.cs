using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface IUserRepository : IRepository<User>
    {

        Task<bool> ExistsByUsernameAsync(string Username);
    }
}
