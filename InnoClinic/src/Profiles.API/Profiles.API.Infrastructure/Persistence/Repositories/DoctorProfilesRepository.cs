using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class DoctorProfilesRepository(ProfilesDbContext context) : BaseRepository<DoctorProfile, Guid>(context), IDoctorProfilesRepository
    {
        public async Task<DoctorProfile?> GetByAccountIdAsync(Guid id, CancellationToken ct = default)
        {
            return await context.Set<DoctorProfile>()
                .FirstOrDefaultAsync(profile => profile.AccountId == id, ct);
        }

        public async Task<IEnumerable<DoctorProfile>> GetFilteredAsync(Guid? specializationId, Guid? officeId, CancellationToken ct = default)
        {
            var query = context.Set<DoctorProfile>().AsQueryable();

            if (specializationId.HasValue)
                query = query.Where(d => d.SpecializationId == specializationId.Value);

            if (officeId.HasValue)
                query = query.Where(d => d.OfficeId == officeId.Value);

            return await query.ToListAsync(ct);
        }
    }
}
