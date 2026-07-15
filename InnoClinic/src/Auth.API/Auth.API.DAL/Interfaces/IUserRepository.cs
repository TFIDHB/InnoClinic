using DAL.Entities;
using InnoClinic.Shared.Interfaces;

namespace DAL.Interfaces
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken ct = default);
    }
}
