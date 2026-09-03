using DAL.Entities;
using DAL.Interfaces;
using InnoClinic.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class UserRepository(AuthDbContext context): BaseRepository<User, Guid>(context), IUserRepository
    {
        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct = default) =>
            await DbSet.AnyAsync(e => e.Email == email, ct);

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default) =>
            await DbSet.FirstOrDefaultAsync(e => e.Email == email, ct);

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken ct = default) =>
            await DbSet.FirstOrDefaultAsync(e => e.RefreshToken == refreshToken, ct);
    }
}
