using Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IPatientProfilesRepository : IRepository<PatientProfile, Guid>
    {
        Task<PatientProfile?> GetByAccountIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<PatientProfile>> GetUnlinkedProfilesAsync(CancellationToken ct = default);
        Task<IEnumerable<PatientProfile>> GetFilteredAsync(string? search, CancellationToken ct = default);
    }
}
