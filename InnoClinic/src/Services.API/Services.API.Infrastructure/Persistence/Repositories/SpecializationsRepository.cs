using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class SpecializationsRepository(ServicesDbContext context): BaseRepository<Specialization, Guid>(context), ISpecializationsRepository
    {
        public async Task<Specialization?> GetByIdWithServicesAsync(Guid id, CancellationToken ct = default)
            => await context.Specializations
            .Include(s => s.Services)
            .FirstOrDefaultAsync(s => s.Id == id, ct);

        public async Task<IEnumerable<Specialization>> GetAllWithServicesAsync(CancellationToken ct = default)
            => await context.Specializations
            .Include(s => s.Services)
            .ToListAsync(ct);
    }
}
