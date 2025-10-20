using StayHub.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StayHub.Application.Interfaces.Repository
{
    public interface IRepository<T> where T: BaseEntity
    {
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<T,int,TResult> selector);
        Task<IEnumerable<TResult>> GetManyAsync<TResult>(Expression<Func<T, bool>>? filter, Expression<Func<T, T>>? include, Func<T, int, TResult>? selector, bool? tracking);
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task Delete(T entity);
        Task<bool> ExistsAsync(int id);
        Task SaveAsync();
    }
}
