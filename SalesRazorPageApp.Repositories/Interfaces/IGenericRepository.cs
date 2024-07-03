using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SalesRazorPageApp.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);

        Task<T?> FindOneAsync(Expression<Func<T, bool>> expression, bool hasTrackings = true);

        Task<T?> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        Task AddAsync(T TEntity);

        Task UpdateAsync(T TEntity);

        Task DeleteAsync(T TEntity);

        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task ExecuteDeleteAsync(Expression<Func<T, bool>> expression);

        Task<int> CountAsync(Expression<Func<T, bool>> expression);
    }
}