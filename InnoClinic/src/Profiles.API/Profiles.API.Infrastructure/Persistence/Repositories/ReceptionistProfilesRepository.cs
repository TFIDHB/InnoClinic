using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ReceptionistProfilesRepository(ProfilesDbContext context) 
        : BaseRepository<ReceptionistProfile, Guid>(context), IReceptionistProfilesRepository
    {
        public async Task<ReceptionistProfile?> GetByAccountIdAsync(Guid id, CancellationToken ct = default)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.AccountId == id, ct);
        }
    }
}
