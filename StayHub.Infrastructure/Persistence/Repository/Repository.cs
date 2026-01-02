using Microsoft.EntityFrameworkCore;
using StayHub.Application.Interfaces.Repository;
using StayHub.Domain.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StayHub.Infrastructure.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
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
        public async Task<List<T>> AddRangeIfNotExitsAsync(List<T> entities, Func<T,DbSet<T>, bool> existFilter)
        {
            var newEntities = entities.Where(e => existFilter(e, _dbSet)).ToList();
            if (newEntities.Any())
            {
                _dbSet.AddRange(newEntities);
                await SaveAsync();
            }
            return newEntities;
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await GetEntityByIdAsync(id);
            return entity != null;
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).AsNoTracking().AnyAsync();
        }
        public async Task<IEnumerable<TResult>> GetManyAsync<TResult>(Expression<Func<T, bool>> filter, Func<T, int, TResult> selector, Func<IQueryable<T>, IQueryable<T>>? include = null, bool? tracking = null)
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

        public async Task<IEnumerable<T>> GetManyEntityAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>>? include = null, bool? tracking = false)
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
            return await result.ToListAsync();
        }


        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<T, int, TResult>? selector)
        {
            var result = _dbSet.AsNoTracking();
            return result.Select(selector);
        }

        public async Task<T?> GetEntityByIdAsync(int id)
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
            _appDbContext.SaveChanges();
        }

        public async Task<T?> FindOneEntityAsync(Expression<Func<T, bool>>? filter, Func<IQueryable<T>, IQueryable<T>>? include, bool trackChange = false)
        {
            var query = trackChange ? _dbSet.Where(filter) : _dbSet.Where(filter).AsNoTracking();
            if (include != null)
            {
                query = include(query);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<TResult> FindOneAsync<TResult>(Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> selector, Func<IQueryable<T>, IQueryable<T>>? include, bool trackChange = false)
        {
            var query = trackChange ? _dbSet.Where(filter) : _dbSet.Where(filter).AsNoTracking();
            if (include != null)
            {
                query = include(query);
            }
            return await query.Select(selector).FirstOrDefaultAsync();
        }

        }
}
