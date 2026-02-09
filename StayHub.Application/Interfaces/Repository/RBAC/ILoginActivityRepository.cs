using StayHub.Domain.Entity.RBAC;

namespace StayHub.Application.Interfaces.Repository.RBAC
{
    public interface ILoginActivityRepository : IPagingAndSortingRepository<LoginActivity>
    {
        Task<(List<LoginActivity>, int)> GetAllPaginatedActivity(int userId,int pageNumber, int pageSize);

    }
}
