using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository : BasicRepository<User, int>, IUserRepository
    {
        public UserRepository(AuthDbContext context) : base(context)
        {
        }
        public async Task<bool> ExistsByEmailAsync(string email) =>
            await DbSet.AnyAsync(e => e.Email == email);
        public async Task<User?> GetByEmailAsync(string email) =>
          await DbSet.FirstOrDefaultAsync(e => e.Email == email);
        public async Task<User?> GetByRefreshTokenAsync(string refreshToken) =>
            await DbSet.FirstOrDefaultAsync(e => e.RefreshToken == refreshToken);
        public async Task<User?> GetByRefreshTokenIdAsync(string refreshTokenId) =>
            await DbSet.FirstOrDefaultAsync(e => e.RefreshTokenId == refreshTokenId);
    }
}
