using Application.DTOs;
using Application.Interfaces;
using InnoClinic.Shared.Exceptions;
using System.Net;
using System.Net.Http.Json;

namespace Infrastructure.Clients
{
    public class ProfilesClient(HttpClient httpClient) : IProfilesClient
    {
        public async Task<PatientInfoDto?> GetPatientInfoAsync(Guid patientId, CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"/api/v1/patients/{patientId}", ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PatientInfoDto>(ct)
                ?? throw new ExternalServiceException("Profiles.API");
        }

        public async Task<DoctorInfoDto?> GetDoctorInfoAsync(Guid doctorId, CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"/api/v1/doctors/{doctorId}", ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DoctorInfoDto>(ct)
                ?? throw new ExternalServiceException("Profiles.API");
        }

        public async Task<Guid> GetMyPatientProfileIdAsync(CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"/api/v1/patients/me", ct);
            response.EnsureSuccessStatusCode();

            var profile = await response.Content.ReadFromJsonAsync<PatientInfoDto>(ct)
                ?? throw new ExternalServiceException("Profiles.API");
            return profile.Id;
        }

        public async Task<Guid> GetMyDoctorProfileIdAsync(CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"/api/v1/doctors/me", ct);
            response.EnsureSuccessStatusCode();

            var profile = await response.Content.ReadFromJsonAsync<DoctorInfoDto>(ct)
                ?? throw new ExternalServiceException("Profiles.API");
            return profile.Id;
        }
    }
}
