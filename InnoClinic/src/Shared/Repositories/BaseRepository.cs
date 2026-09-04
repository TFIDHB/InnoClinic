using InnoClinic.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InnoClinic.Shared.Repositories
{
    public abstract class BaseRepository<TEntity, TId>(DbContext context) : IRepository<TEntity, TId>
    where TEntity : class
    {
        protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

        public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken ct = default) =>
            await DbSet.FindAsync([id], ct);

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = default) =>
            await DbSet.AsNoTracking().ToListAsync(ct);

        public virtual async Task CreateAsync(TEntity entity, CancellationToken ct = default) =>
            await DbSet.AddAsync(entity, ct);

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken ct = default) =>
            DbSet.Update(entity);

        public virtual async Task DeleteAsync(TId id, CancellationToken ct = default)
        {
            var entity = await GetByIdAsync(id, ct);
            if (entity != null)
            {
                DbSet.Remove(entity);
            }
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default) =>
            await DbSet.AnyAsync(predicate, ct);
    }
}