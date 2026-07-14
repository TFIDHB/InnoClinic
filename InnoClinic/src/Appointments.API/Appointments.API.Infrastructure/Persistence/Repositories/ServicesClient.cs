using Application.Interfaces;
using InnoClinic.Shared.Exceptions;
using System.Net;
using System.Net.Http.Json;

namespace Infrastructure.Persistence.Repositories
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
    }
}
