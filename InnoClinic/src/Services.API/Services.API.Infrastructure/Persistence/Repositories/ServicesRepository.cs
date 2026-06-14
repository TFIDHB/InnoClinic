using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ServicesRepository(ServicesDbContext context) : BaseRepository<Service, Guid>(context), IServicesRepository
    {
        public async Task<IEnumerable<Service>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
            => await context.Services
                .Where(e => ids.Contains(e.Id))
                .ToListAsync(ct);
        public async Task<bool> CategoryExistsAsync(Guid categoryId, CancellationToken ct = default)
            => await context.Set<ServiceCategory>().AnyAsync(e => e.Id == categoryId, ct);
    }
}
