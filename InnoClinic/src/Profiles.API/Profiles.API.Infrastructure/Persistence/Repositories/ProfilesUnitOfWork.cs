using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Repositories
{
    public class ProfilesUnitOfWork(ProfilesDbContext context, IServiceProvider provider) : IProfilesUnitOfWork
    {
        private IProfilesRepository? _profilesRepository;

        public IProfilesRepository ProfilesRepository =>
            _profilesRepository ??= provider.GetRequiredService<IProfilesRepository>();

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await context.SaveChangesAsync(ct);

        public void Dispose() => context.Dispose();
    }
}
