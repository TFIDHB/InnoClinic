using Application.DTOs;
using Domain.Entities;
using InnoClinic.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IDoctorProfilesRepository : IRepository<DoctorProfile, Guid>
    {
        Task<DoctorProfile?> GetByAccountIdAsync(Guid id, CancellationToken ct = default);
    }
}
