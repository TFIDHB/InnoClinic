using DAL.Interfaces;

namespace DAL.Repositories
{
    public class AuthUnitOfWork(AuthDbContext context, IUserRepository userRepository) : IAuthUnitOfWork, IDisposable
    {
        public IUserRepository UserRepository { get; } = userRepository;
        public void Dispose() => context.Dispose();
        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await context.SaveChangesAsync(ct);
    }
}
