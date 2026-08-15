using Application.DTOs;
using Application.Interfaces;
using InnoClinic.Shared.Exceptions;
using System.Net;
using System.Net.Http.Json;

namespace Infrastructure.Clients
{
    public class ServicesClient(HttpClient httpClient) : IServicesClient
    {
        public async Task<int> GetTimeSlotSizeAsync(Guid serviceId, CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"/api/v1/services/{serviceId}/time-slot-size", ct);

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new NotFoundException("Service");

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<int>();
            return result;
        }

        public async Task<string?> GetServiceNameAsync(Guid serviceId, CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"/api/v1/services/{serviceId}", ct);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            var service = await response.Content.ReadFromJsonAsync<ServiceDto>(ct);
            return service?.Name;
        }

        public async Task<string?> GetSpecializationNameAsync(Guid specializationId, CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"/api/v1/specializations/{specializationId}", ct);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();
            var spec = await response.Content.ReadFromJsonAsync<SpecializationDto>(ct);
            return spec?.Name;
        }
    }
}
