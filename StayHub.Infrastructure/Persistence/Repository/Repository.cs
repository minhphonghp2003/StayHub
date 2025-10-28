using Microsoft.EntityFrameworkCore;
using StayHub.Application.Interfaces.Repository;
using StayHub.Domain.Entity;
using System.Linq.Expressions;

namespace StayHub.Infrastructure.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T :BaseEntity 
    {
        public Repository(AppDbContext context)
        {
            _appDbContext = context;
            _dbSet = context.Set<T>();
        }
        protected readonly DbSet<T> _dbSet;
        protected readonly AppDbContext _appDbContext;
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }
        public async Task<IEnumerable<TResult>> GetManyAsync<TResult>(Expression<Func<T, bool>>? filter, Func<IQueryable<T>, IQueryable<T>>? include, Func<T, int, TResult>? selector, bool? tracking)
        {
            var result = tracking == true ? _dbSet.AsNoTracking() : _dbSet;
            if (filter != null)
            {
                result = result.Where(filter);
            }
            if (include != null)
            {
                result = include(result);
            }
            return result.Select(selector);
        }


        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<T, int, TResult>? selector)
        {
            var result = _dbSet.AsNoTracking();
            return result.Select(selector);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public Task<T?> FindOneAsync(Expression<Func<T, bool>>? filter,  Func<IQueryable<T>, IQueryable<T>>? include = null,bool trackChange = false)
        {
            var query =trackChange? _dbSet.Where(filter):_dbSet.Where(filter).AsNoTracking();
            if(include != null)
            {
                query = include(query);
            }
            return query.FirstOrDefaultAsync();
        }
    }
}
