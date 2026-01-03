using StayHub.Application.DTO.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface IUserRepository : IPagingAndSortingRepository<User>
    {

        Task<bool> ExistsByUsernameAsync(string Username);
        Task<(List<UserDTO>, int)> GetAllUser(int pageNumber, int pageSize, string? searchKey);
        Task<bool> SetActivated(int id, bool activated);
    }
}
