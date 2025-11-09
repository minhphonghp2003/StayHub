using StayHub.Domain.Entity;
using System.Linq.Expressions;

namespace StayHub.Application.Interfaces.Repository
{
    public interface IPagingAndSortingRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<(List<TResult>, int)> GetManyPagedAsync<TResult>(Expression<Func<T, bool>> filter, Func<T, int, TResult> selector, int pageNumber = 1, int pageSize = 10, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IQueryable<T>>? include = null, bool? tracking = false);
    }
}
