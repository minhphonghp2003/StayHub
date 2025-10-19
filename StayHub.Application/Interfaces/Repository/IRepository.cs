using StayHub.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.Interfaces.Repository
{
    public interface IRepository<T> where T: BaseEntity
    {
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<T,int,TResult> selector);
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task Delete(T entity);
        Task<bool> ExistsAsync(int id);
        Task SaveAsync();
    }
}
