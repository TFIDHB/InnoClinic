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
    }
}
