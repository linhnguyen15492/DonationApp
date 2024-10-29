using DonationApp.Core.Interfaces;
using DonationApp.Core.Interfaces.Repositories;
using DonationApp.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DonationApp.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationContext _context;

        protected DbSet<T> _dbSet;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();

        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            entity.GetType().GetProperty("Id")?.SetValue(entity, entity.GetType().GetProperty("Id")?.GetValue(entity));

            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            var data = await GetByIdAsync(entity.GetType().GetProperty("Id")?.GetValue(entity)!);

            if (data == null)
            {
                throw new Exception("Not Found");
            }
            else
            {
                _context.Entry(data).CurrentValues.SetValues(entity);
            }
        }

        public virtual async Task DeleteAsync(object id)
        {
            T? existing = await _dbSet.FindAsync(id);
            if (existing != null)
            {
                _dbSet.Remove(existing);
            }
        }

        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}

