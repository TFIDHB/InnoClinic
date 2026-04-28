using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class BasicRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        private readonly AppointmentDbContext _context;
        private readonly DbSet<TEntity> DbSet;

        public BasicRepository(AppointmentDbContext context)
        {
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
            if (entity != null)
            {
                DbSet.Remove(entity);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await DbSet.AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
        }
    }
}
