using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class ReceptionistProfilesRepository(ProfilesDbContext context) : BaseRepository<ReceptionistProfile, Guid>(context), IReceptionistProfilesRepository
    {
    }
}
