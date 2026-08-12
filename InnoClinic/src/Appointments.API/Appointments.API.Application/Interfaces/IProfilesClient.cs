using Application.DTOs;

namespace Infrastructure.Clients
{
    public interface IProfilesClient
    {
        Task<PatientInfoDto?> GetPatientInfoAsync(Guid patientId, CancellationToken ct = default);
        Task<DoctorInfoDto?> GetDoctorInfoAsync(Guid doctorId, CancellationToken ct = default);
    }
}
