using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Helpers;
using InnoClinic.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PatientProfilesRepository(ProfilesDbContext context) 
        : BaseRepository<PatientProfile, Guid>(context), IPatientProfilesRepository
    {
        public async Task<PatientProfile?> GetByAccountIdAsync(Guid id, CancellationToken ct = default)
        {
            return await DbSet.FirstOrDefaultAsync(profile => profile.AccountId == id, ct);
        }

        public async Task<IEnumerable<PatientProfile>> GetFilteredAsync(string? search, CancellationToken ct = default)
        {
            var query = context.Set<PatientProfile>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var terms = search.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var term in terms)
                {
                    var escaped = LikeTermHelper.EscapeLikeTerm(term);
                    query = query.Where(d =>
                        EF.Functions.Like(d.FirstName, $"%{escaped}%") ||
                        EF.Functions.Like(d.LastName, $"%{escaped}%") ||
                        (d.MiddleName != null && EF.Functions.Like(d.MiddleName, $"%{escaped}%")));
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
