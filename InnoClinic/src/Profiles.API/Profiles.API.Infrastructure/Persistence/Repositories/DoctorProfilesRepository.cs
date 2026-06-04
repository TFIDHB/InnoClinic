using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class DoctorProfilesRepository(ProfilesDbContext context) : BaseRepository<DoctorProfile, Guid>(context), IDoctorProfilesRepository
    {
    }
}
