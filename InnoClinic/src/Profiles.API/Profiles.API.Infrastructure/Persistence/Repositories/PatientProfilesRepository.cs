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
    }
}
