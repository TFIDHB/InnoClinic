using Application.DTOs;
using Application.Interfaces;
using BLL.DTOs;
using System.Net.Http.Json;

namespace Infrastructure.Persistence.Repositories
{
    public class AuthClient(HttpClient httpClient) : IAuthClient
    {
        public async Task<CreateStaffAccountResponseDto> CreateStaffAccountAsync(string email, CancellationToken ct = default)
        {
            var response = await httpClient.PostAsJsonAsync("/api/auth/create-staff-account", new { Email = email }, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<CreateStaffAccountResponseDto>(ct)
                ?? throw new InvalidOperationException();
        }

        public async Task<UserAccountInfoDto?> GetUserAccountInfoDtoAsync(Guid userId, CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"/api/auth/accounts/{userId}", ct);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserAccountInfoDto>(ct)
                ?? throw new InvalidOperationException();
        }

        public async Task UpdateUserAccountInfoDtoAsync(Guid userId, UpdateUserAccountInfoDto dto, CancellationToken ct = default)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/auth/accounts/{userId}", dto, ct);
            response.EnsureSuccessStatusCode();
        }
    }
}
