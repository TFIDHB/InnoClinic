using Application.DTOs;

namespace Application.Interfaces
{
    public interface IPatientProfileService : IProfilesService<PatientProfileDto, CreatePatientProfileRequestDto, UpdatePatientProfileRequestDto>
    {
        Task<PatientProfileDto> CreateOrMatchProfileAsync(Guid accountId, CreateMyPatientProfileRequestDto dto, CancellationToken ct = default);
        Task<PatientProfileDto> LinkProfileToAccountAsync(Guid profileId, Guid accountId, IPatientFields fields, CancellationToken ct = default);
        Task<PatientProfileDto> GetByAccountIdAsync(Guid accountId, CancellationToken ct = default);
        Task<IEnumerable<PatientProfileDto>> GetFilteredPatientsAsync(string? search, CancellationToken ct = default);
    }
}
