using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository(AuthDbContext context) : BaseRepository<User, int>(context), IUserRepository
    {
        public async Task<bool> ExistsByEmailAsync(string email) =>
            await DbSet.AnyAsync(e => e.Email == email);
        public async Task<User?> GetByEmailAsync(string email) =>
          await DbSet.FirstOrDefaultAsync(e => e.Email == email);
        public async Task<User?> GetByRefreshTokenAsync(string refreshToken) =>
            await DbSet.FirstOrDefaultAsync(e => e.RefreshToken == refreshToken);
    }
}
