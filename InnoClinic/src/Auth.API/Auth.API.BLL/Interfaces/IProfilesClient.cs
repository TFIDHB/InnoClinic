using BLL.DTOs;

namespace BLL.Interfaces
{
    public interface IProfilesClient
    {
        Task<AccountProfileInfoDto?> GetProfileInfoByAccountIdAsync(Guid id, CancellationToken ct = default);
    }
}
