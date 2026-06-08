using DAL;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Auth.API.Tests
{
    public class UserRepositoryIntegrationTests : IAsyncLifetime
    {
        private readonly MsSqlContainer _sqlContainer;
        private DbContextOptions<AuthDbContext> _contextOptions;
        public UserRepositoryIntegrationTests()
        {
            _sqlContainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .Build();
        }
        public async Task DisposeAsync()
        {
            await _sqlContainer.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            await _sqlContainer.StartAsync();

            _contextOptions = new DbContextOptionsBuilder<AuthDbContext>()
                .UseSqlServer(_sqlContainer.GetConnectionString())
                .Options;

            using var context = new AuthDbContext(_contextOptions);
            await context.Database.MigrateAsync();
        }
        private AuthDbContext CreateContext() => new(_contextOptions);

        [Fact]
        public async Task ExistsByEmailAsync_WhenEmailExists_ReturnsTrue() 
        {
            var email = "exists@test.com";
            using (var context = CreateContext())
            {
                await context.Users.AddAsync(new User { Email = email, PasswordHash = "passwordHash" });
                await context.SaveChangesAsync();
            }

            using (var context = CreateContext())
            {
                var repository = new UserRepository(context);

                var result = await repository.ExistsByEmailAsync(email);

                Assert.True(result);
            }
        }

        [Fact]
        public async Task ExistsByEmailAsync_WhenEmailDoesNotExist_ReturnsFalse() 
        {
            using var context = CreateContext();
            var repository = new UserRepository(context);

            var result = await repository.ExistsByEmailAsync("doesnotexists@test.com");

            Assert.False(result);
        }

        [Fact]
        public async Task GetByEmailAsync_WhenEmailExists_ReturnsUser() 
        { 
            var email = "getbyemail@test.com";
            var expectedUser = new User { Email = email, PasswordHash = "passwordHash" };
            using (var context = CreateContext())
            {
                await context.Users.AddAsync(expectedUser);
                await context.SaveChangesAsync();
            }

            using (var context = CreateContext())
            {
                var repository = new UserRepository(context);

                var actualUser = await repository.GetByEmailAsync(email);

                Assert.NotNull(actualUser);
                Assert.Equal(expectedUser.Email, actualUser.Email);
                Assert.Equal(expectedUser.PasswordHash, actualUser.PasswordHash);
            }
        }

        [Fact]
        public async Task GetByEmailAsync_WhenEmailDoesNotExist_ReturnsNull() 
        {
            using var context = CreateContext();
            var repository = new UserRepository(context);

            var result = await repository.GetByEmailAsync("noemail@test.com");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByRefreshTokenAsync_WhenTokenExists_ReturnsUser() 
        {
            var refreshToken = "valid-refresh-token";
            var expectedUser = new User { Email = "owner@test.com", PasswordHash = "passwordHash", RefreshToken = refreshToken };

            using (var context = CreateContext())
            {
                await context.Users.AddAsync(expectedUser);
                await context.SaveChangesAsync();
            }

            using (var context = CreateContext())
            {
                var repository = new UserRepository(context);

                var actualUser = await repository.GetByRefreshTokenAsync(refreshToken);

                Assert.NotNull(actualUser);
                Assert.Equal(expectedUser.Email, actualUser.Email);
                Assert.Equal(refreshToken, actualUser.RefreshToken);
            }
        }

        [Fact]
        public async Task GetByRefreshTokenAsync_WhenTokenDoesNotExist_ReturnsNull() 
        {
            using var context = CreateContext();
            var repository = new UserRepository(context);

            var result = await repository.GetByRefreshTokenAsync("fake-refresh-token");

            Assert.Null(result);
        }
    }
}
