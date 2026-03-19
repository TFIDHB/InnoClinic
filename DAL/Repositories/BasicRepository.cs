using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BasicRepository<T> : IRepository<T> where T : class
    {
        private readonly AuthDbContext _context;
        protected readonly DbSet<T> DbSet;

        public BasicRepository(AuthDbContext context){
            _context = context;
            DbSet = context.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) { 
                DbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync() => 
            await DbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => 
            await DbSet.FindAsync(id);

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
