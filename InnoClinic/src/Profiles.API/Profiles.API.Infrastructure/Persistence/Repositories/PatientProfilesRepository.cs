using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PatientProfilesRepository(ProfilesDbContext context) : BaseRepository<PatientProfile, Guid>(context), IPatientProfilesRepository
    {
        public async Task<PatientProfile?> GetByAccountIdAsync(Guid id, CancellationToken ct = default)
        {
            return await context.Set<PatientProfile>()
                .FirstOrDefaultAsync(profile => profile.AccountId == id, ct);
        }

        public async Task<IEnumerable<PatientProfile>> GetFilteredAsync(string? search, CancellationToken ct = default)
        {
            var query = context.Set<PatientProfile>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var terms = search.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var term in terms)
                {
                    query = query.Where(d =>
                        EF.Functions.Like(d.FirstName, $"%{term}%") ||
                        EF.Functions.Like(d.LastName, $"%{term}%") ||
                        (d.MiddleName != null && EF.Functions.Like(d.MiddleName, $"%{term}%")));
                }
            }

            return await query.ToListAsync(ct);
        }

        public async Task<IEnumerable<PatientProfile>> GetUnlinkedProfilesAsync(CancellationToken ct = default)
        {
            return await context.PatientProfiles
                .Where(p => !p.IsLinkedToAccount)
                .ToListAsync(ct);
        }
    }
}
