using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequestDto dto, CancellationToken ct = default);
        Task<AuthTokenDto> LoginAsync(LoginRequestDto dto, CancellationToken ct = default);
        Task LogoutAsync(LogOutRequestDto dto, Guid userId, CancellationToken ct = default);
    }
}
