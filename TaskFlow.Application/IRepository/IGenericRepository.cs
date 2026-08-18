using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Application.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(Guid Id);
        Task DeleteAsync(Guid Id);
        Task UpdateAsync(T entity);
    }
}
