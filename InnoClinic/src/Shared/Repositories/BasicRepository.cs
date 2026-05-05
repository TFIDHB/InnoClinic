using InnoClinic.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InnoClinic.Shared.Repositories
{
    public abstract class BaseRepository<TEntity, TId>(DbContext context) : IRepository<TEntity, TId>
        where TEntity : class
    {
        protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

        public virtual async Task<TEntity?> GetByIdAsync(TId id) => await DbSet.FindAsync(id);

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await DbSet.AsNoTracking().ToListAsync();

        public virtual async Task CreateAsync(TEntity entity) => await DbSet.AddAsync(entity);

        public virtual async Task UpdateAsync(TEntity entity) => DbSet.Update(entity);

        public virtual async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) DbSet.Remove(entity);
        }
    }
}