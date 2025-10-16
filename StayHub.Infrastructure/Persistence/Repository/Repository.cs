using Microsoft.EntityFrameworkCore;
using StayHub.Application.Interfaces.Repository;
using StayHub.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Infrastructure.Persistence.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public Repository(AppDbContext context)
        {
           _appDbContext = context; 
            _dbSet = context.Set<T>();
        }
        private readonly DbSet<T> _dbSet;
        private readonly AppDbContext _appDbContext;
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
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
    }
}
