using Application.DTOs;
using Application.Interfaces;
using System.Net.Http.Json;

namespace Infrastructure.Persistence.Repositories
{
    public class AppointmentsClient(HttpClient httpClient) : IAppointmentsClient
    {
        public async Task<IEnumerable<AppointmentSlotDto>> GetAppointmentsAsync(DateOnly date, Guid? doctorId, CancellationToken ct = default)
        {
            var url = $"/api/v1/appointments?date={date:yyyy-MM-dd}&doctorId={doctorId}";
            var response = await httpClient.GetFromJsonAsync<IEnumerable<AppointmentSlotDto>>(url, ct);
            return response ?? [];
        }
    }
}
