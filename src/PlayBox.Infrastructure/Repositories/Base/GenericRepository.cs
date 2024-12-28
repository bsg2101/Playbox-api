using Microsoft.EntityFrameworkCore;
using PlayBox.Domain.Entities;
using PlayBox.Domain.Entities.Base;
using PlayBox.Domain.Interfaces;
using PlayBox.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Infrastructure.Repositories.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.UtcNow;
            }
        }
        public async Task<T> GetByEmailAsync(string email)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => (e as User).Email == email);
        }
    }
}
