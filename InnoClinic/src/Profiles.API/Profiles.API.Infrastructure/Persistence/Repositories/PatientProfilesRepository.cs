using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;

namespace Infrastructure.Persistence.Repositories
{
    public class PatientProfilesRepository(ProfilesDbContext context) : BaseRepository<PatientProfile, Guid>(context), IPatientProfilesRepository
    {
    }
}
