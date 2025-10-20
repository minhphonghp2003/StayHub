using StayHub.Domain.Entity;

namespace StayHub.Application.Interfaces.Repository
{
    public interface IPagingAndSortingRepository<T> : IRepository<T> where T : BaseEntity
    {
    }
}
