using StayHub.Application.DTO.RBAC;
using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface IUserRepository : IPagingAndSortingRepository<User>
    {

        Task<bool> ExistsByUsernameAsync(string Username);
        Task<(List<UserDTO>, int)> GetAllUser(int pageNumber, int pageSize, string? searchKey);
        Task<(List<UserDTO>,int)> GetUserOfRole(int roleId, int pageNumber, int pageSize);
        Task<bool> SetActivated(int id, bool activated);
        Task<ProfileDTO> GetProfile(int userId);
    }
}
