using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthClient
    {
        Task<CreateStaffAccountResponseDto> CreateStaffAccountAsync(string email, CancellationToken ct = default);
    }
}
