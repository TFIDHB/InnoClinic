using Application.DTOs;
using Application.Interfaces;
using System.Net.Http.Json;

namespace Infrastructure.Persistence.Repositories
{
    public class AuthClient(HttpClient httpClient) : IAuthClient
    {
        public async Task<CreateStaffAccountResponseDto> CreateStaffAccountAsync(string email, CancellationToken ct = default)
        {
            var response = await httpClient.PostAsJsonAsync("/api/auth/create-staff-account", new { Email = email }, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<CreateStaffAccountResponseDto>(cancellationToken: ct)
                ?? throw new InvalidOperationException();
        }
    }
}
