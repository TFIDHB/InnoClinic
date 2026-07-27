using Application.DTOs;
using BLL.DTOs;

namespace Application.Interfaces
{
    public interface IAuthClient
    {
        Task<CreateStaffAccountResponseDto> CreateStaffAccountAsync(string email, CancellationToken ct = default);
        Task<UserAccountInfoDto?> GetUserAccountInfoDtoAsync(Guid userId, CancellationToken ct = default);
        Task UpdateUserAccountInfoDtoAsync(Guid userId, UpdateUserAccountInfoDto dto, CancellationToken ct = default);
    }
}
