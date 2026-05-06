using DAL.Interfaces;

namespace DAL.Repositories
{
    public class AuthUnitOfWork(AuthDbContext context) : IAuthUnitOfWork
    {
        public void Dispose() => context.Dispose();

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await context.SaveChangesAsync(ct);
    }
}
