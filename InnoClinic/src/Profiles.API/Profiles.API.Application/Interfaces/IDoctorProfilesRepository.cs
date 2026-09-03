using Domain.Entities;
using Domain.Enums;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IDoctorProfilesRepository : IRepository<DoctorProfile, Guid>
    {
        Task<DoctorProfile?> GetByAccountIdAsync(Guid id, CancellationToken ct = default);

        Task<IEnumerable<DoctorProfile>> GetFilteredAsync(
            Guid? specializationId,
            Guid? officeId,
            string? search,
            DoctorStatus? status,
            CancellationToken ct = default);

        Task<IEnumerable<DoctorProfile>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default);
    }
}
