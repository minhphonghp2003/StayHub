using StayHub.Application.DTO.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface IUserRepository : IPagingAndSortingRepository<User>
    {

        Task<bool> ExistsByUsernameAsync(string Username);
        Task<(List<UserDTO>, int)> GetAllUser(int pageNumber, int pageSize, string? searchKey);
        Task<List<UserDTO>> GetUserOfRole(int roleId);
        Task<bool> SetActivated(int id, bool activated);
    }
}
