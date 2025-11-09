using Microsoft.EntityFrameworkCore;
using StayHub.Application.Interfaces.Repository;
using StayHub.Domain.Entity;
using System.Linq.Expressions;

namespace StayHub.Infrastructure.Persistence.Repository
{
    public class PagingAndSortingRepository<T> : Repository<T>, IPagingAndSortingRepository<T> where T : BaseEntity
    {
        public PagingAndSortingRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<(List<TResult>, int)> GetManyPagedAsync<TResult>(Expression<Func<T, bool>> filter, Func<T, int, TResult> selector, int pageNumber = 1, int pageSize = 10, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IQueryable<T>>? include = null, bool? tracking = false)
        {
            pageSize = Math.Max(pageSize, 1);
            pageNumber = Math.Max(pageNumber, 1);
            var result = tracking == true ? _dbSet.AsNoTracking() : _dbSet;

            if (filter != null)
            {
                result = result.Where(filter);
            }
            if (include != null)
            {
                result = include(result);
            }
            if (orderBy != null)
            {
                result = orderBy(result);
            }
            var count = await result.CountAsync();
            return (result.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(selector).ToList(), count);

        }
    }
}
