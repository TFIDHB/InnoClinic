using Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IPatientProfilesRepository : IRepository<PatientProfile, Guid>
    {
        Task<PatientProfile?> GetByAccountIdAsync(Guid id, CancellationToken ct = default);
    }
}
