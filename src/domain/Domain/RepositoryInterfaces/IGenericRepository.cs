using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task<T> GetByIdAsync(long id);        
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<T> FirstOrDefaultAsyncTracked(Expression<Func<T, bool>> expression);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetDataAsync(
           Expression<Func<T, bool>> expression = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           int? skip = null,
           int? take = null);
    }
}
