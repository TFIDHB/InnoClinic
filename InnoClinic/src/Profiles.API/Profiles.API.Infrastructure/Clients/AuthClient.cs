using Application.DTOs;
using Application.Interfaces;
using BLL.DTOs;
using InnoClinic.Shared.Exceptions;
using System.Net.Http.Json;

namespace Infrastructure.Clients
{
    public class AuthClient(HttpClient httpClient) : IAuthClient
    {
        public async Task<CreateStaffAccountResponseDto> CreateStaffAccountAsync(string email, CancellationToken ct = default)
        {
            var response = await httpClient.PostAsJsonAsync("/api/v1/auth/create-staff-account", new { Email = email }, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<CreateStaffAccountResponseDto>(ct)
                ?? throw new ExternalServiceException("Auth.API");
        }

        public async Task<UserAccountInfoDto?> GetUserAccountInfoAsync(Guid userId, CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"/api/v1/auth/accounts/{userId}", ct);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserAccountInfoDto>(ct)
                ?? throw new ExternalServiceException("Auth.API");
        }

        public async Task UpdateUserAccountInfoAsync(Guid userId, UpdateUserAccountInfoDto dto, CancellationToken ct = default)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/v1/auth/accounts/{userId}", dto, ct);
            response.EnsureSuccessStatusCode();
        }
    }
}
