using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistance.Repositories
{
    public class OfficesUnitOfWork(OfficesDbContext context, IServiceProvider provider) : IOfficesUnitOfWork, IDisposable
    {
        private IOfficesRepository? _officesRepository;

        public IOfficesRepository OfficesRepository =>
            _officesRepository ??= provider.GetRequiredService<IOfficesRepository>();
        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await context.SaveChangesAsync(ct);

        public void Dispose() => context.Dispose();
    }
}
