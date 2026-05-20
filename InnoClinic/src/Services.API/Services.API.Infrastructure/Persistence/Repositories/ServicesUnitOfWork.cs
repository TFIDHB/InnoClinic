using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Repositories
{
    public class ServicesUnitOfWork(ServicesDbContext context, IServiceProvider provider) : IServicesUnitOfWork, IDisposable
    {
        private IServicesRepository? _servicesRepository;

        public IServicesRepository ServicesRepository =>
            _servicesRepository ??= provider.GetRequiredService<IServicesRepository>();

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await context.SaveChangesAsync(ct);

        public void Dispose() => context.Dispose();
    }
}
