using Application.Interfaces;
using System.Net.Http.Json;

namespace Infrastructure.Persistence.Repositories
{
    public class ServicesClient(HttpClient httpClient) : IServicesClient
    {
        public async Task<int> GetTimeSlotSizeAsync(Guid serviceId, CancellationToken ct = default)
        {
            var result = await httpClient.GetFromJsonAsync<int>(
                $"/api/v1/services/{serviceId}/time-slot-size", ct);
            return result;
        }
    }
}
