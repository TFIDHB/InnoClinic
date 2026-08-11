using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAccountSearchService
    {
        Task<AccountProfileInfoDto?> GetByAccountIdAsync(Guid accountId, CancellationToken ct = default);
    }
}
