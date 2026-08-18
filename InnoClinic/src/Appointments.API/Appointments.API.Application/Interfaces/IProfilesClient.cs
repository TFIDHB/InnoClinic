using Application.DTOs;

namespace Application.Interfaces
{
    public interface IProfilesClient
    {
        Task<PatientInfoDto?> GetPatientInfoAsync(Guid patientId, CancellationToken ct = default);
        Task<DoctorInfoDto?> GetDoctorInfoAsync(Guid doctorId, CancellationToken ct = default);
        Task<Guid> GetMyPatientProfileIdAsync(CancellationToken ct = default);
        Task<Guid> GetMyDoctorProfileIdAsync(CancellationToken ct = default);
    }
}
