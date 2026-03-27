using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class BasicRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        private readonly AuthDbContext _context;
        protected readonly DbSet<TEntity> DbSet;

        public BasicRepository(AuthDbContext context){
            _context = context;
            DbSet = context.Set<TEntity>();
        }
        public async Task CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) { 
                DbSet.Remove(entity);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => 
            await DbSet.AsNoTracking().ToListAsync();

        public async Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public async Task<TEntity?> GetByIdAsync(TId id)
        {
           return await DbSet.FindAsync(id);
        }
    }
}
