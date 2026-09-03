using System.Net.Http.Headers;
using System.Net.Http.Json;
using Application.DTOs;
using Application.Interfaces;
using BLL.DTOs;
using InnoClinic.Shared.Exceptions;
using InnoClinic.Shared.Generators;
using InnoClinic.Shared.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Clients
{
    public class AuthClient(HttpClient httpClient, IOptions<JwtSettings> jwtSettings): IAuthClient
    {
        public async Task<CreateStaffAccountResponseDto> CreateStaffAccountAsync(string email, CancellationToken ct = default)
        {
            var response = await httpClient.PostAsJsonAsync("/api/v1/auth/create-staff-account", new { Email = email }, ct);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<CreateStaffAccountResponseDto>(ct)
                ?? throw new ExternalServiceException("Auth.API");
        }

        public async Task<UserAccountInfoDto?> GetAccountInfoAsStaffAsync(Guid userId, CancellationToken ct = default)
        {
            var internalToken = InternalServiceTokenGenerator.Generate(jwtSettings.Value);

            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/v1/accounts/{userId}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", internalToken);

            var response = await httpClient.SendAsync(request, ct);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserAccountInfoDto>(ct)
                ?? throw new ExternalServiceException("Auth.API");
        }

        public async Task<UserAccountInfoDto?> GetUserAccountInfoAsync(Guid userId, CancellationToken ct = default)
        {
            var response = await httpClient.GetAsync($"/api/v1/auth/accounts/{userId}", ct);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

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
