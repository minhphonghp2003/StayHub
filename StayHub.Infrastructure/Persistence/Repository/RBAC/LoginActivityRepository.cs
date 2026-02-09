using StayHub.Application.Interfaces.Repository.RBAC;
using StayHub.Domain.Entity.RBAC;
using Action = StayHub.Domain.Entity.RBAC.Action;

namespace StayHub.Infrastructure.Persistence.Repository.RBAC
{
    public class LoginActivityRepository : PagingAndSortingRepository<LoginActivity>,ILoginActivityRepository 
    {
        public LoginActivityRepository(AppDbContext context) : base(context)
        {
        }

        public Task<(List<LoginActivity>, int)> GetAllPaginatedActivity(int userId,int pageNumber, int pageSize)
        {
            return GetManyPagedAsync(pageNumber: pageNumber, pageSize:pageSize,filter:e=>e.UserId ==userId ,selector:(e,i)=>e);
            
        }
    }
}
