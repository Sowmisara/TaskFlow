using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.IRepository;
using TaskFlow.Infrastructure.AppDbContext;

namespace TaskFlow.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly TaskFlowDbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(TaskFlowDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            var data = await _dbSet.AddAsync(entity);
            return data.Entity;
        }

        public async Task DeleteAsync(Guid Id)
        {
            var data =await _dbSet.FindAsync(Id);
            if(data != null)
            {
                _dbSet.Remove(data);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var data =  await _dbSet.AsNoTracking().ToListAsync();
            return data;
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            var data =await _dbSet.FirstOrDefaultAsync(filter);
            return data;
        }

        public async Task<T> GetByIdAsync(Guid Id)
        {
            var data = await _dbSet.FindAsync(Id);
            return data;
        }

        public  Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;

        }
    }
}
