using StayHub.Domain.Entity;
using System.Linq.Expressions;

namespace StayHub.Application.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<T, int, TResult> selector);
        Task<IEnumerable<TResult>> GetManyAsync<TResult>(Expression<Func<T, bool>> filter, Func<T, int, TResult> selector, Func<IQueryable<T>, IQueryable<T>>? include = null, bool? tracking = false);
        Task<IEnumerable<T>> GetManyEntityAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>>? include = null, bool? tracking = false);
        Task<T?> GetEntityByIdAsync(int id);
        Task<T?> FindOneEntityAsync(Expression<Func<T, bool>>? filter, Func<IQueryable<T>, IQueryable<T>>? include = null, bool trackChange = false);
        Task<TResult?> FindOneAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector, Func<IQueryable<T>, IQueryable<T>>? include = null, bool trackChange = false);
        Task AddAsync(T entity);
        Task<List<T>> AddRangeIfNotExitsAsync(List<T> entities, Func<T, List<T>, bool> existFilter);
        void Update(T entity);
        Task Delete(T entity);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task SaveAsync();
    }
}
