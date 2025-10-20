using StayHub.Application.Interfaces.Repository;
using StayHub.Domain.Entity;

namespace StayHub.Infrastructure.Persistence.Repository
{
    public class PagingAndSortingRepository<T> : Repository<T>, IPagingAndSortingRepository<T> where T : BaseEntity
    {
        public PagingAndSortingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
