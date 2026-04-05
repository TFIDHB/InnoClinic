using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<bool> ExistsByEmailAsync(string email);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByRefreshTokenAsync(string refreshToken);
        Task<User?> GetByRefreshTokenIdAsync(string refreshTokenId);
    }
}
