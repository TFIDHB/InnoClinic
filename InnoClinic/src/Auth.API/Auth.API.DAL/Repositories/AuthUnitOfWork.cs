using DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.Repositories
{
    public class AuthUnitOfWork(AuthDbContext context, IServiceProvider provider) : IAuthUnitOfWork, IDisposable
    {
        private IUserRepository? _userRepository;
        public IUserRepository UserRepository =>
            _userRepository ??= provider.GetRequiredService<IUserRepository>();
        public void Dispose() => context.Dispose();
        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await context.SaveChangesAsync(ct);
    }
}
