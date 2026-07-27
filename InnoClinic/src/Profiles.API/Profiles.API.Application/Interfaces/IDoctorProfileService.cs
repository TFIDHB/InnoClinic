using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDoctorProfileService : IProfilesService<DoctorProfileDto, CreateDoctorProfileRequestDto, UpdateDoctorProfileRequestDto>
    {
        Task<DoctorProfileDto> GetByAccountIdAsync(Guid accountId, CancellationToken ct = default);
        Task<IEnumerable<DoctorProfileDto>> GetFilteredDoctorsAsync(Guid? specializationId, Guid? officeId, string? search, CancellationToken ct = default);
    }
}
