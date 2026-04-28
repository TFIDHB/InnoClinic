using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequestDto dto);
        Task<AuthTokenDto> LoginAsync(LoginRequestDto dto);
        Task LogoutAsync(LogOutRequestDto dto, int userId);
    }
}
