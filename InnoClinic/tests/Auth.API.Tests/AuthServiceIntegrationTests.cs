
using AutoMapper;
using BLL.AutoMapper;
using BLL.DTOs;
using BLL.Services;
using BLL.Settings;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Testcontainers.MsSql;

namespace Auth.API.Tests
{
    public class AuthServiceIntegrationTests : IAsyncLifetime
    {
        private readonly MsSqlContainer _sqlContainer;
        private AuthDbContext _dbContext;
        private AuthService _authService;
        public AuthServiceIntegrationTests() 
        {
            _sqlContainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .Build();
        }
        public async Task InitializeAsync()
        {
            await _sqlContainer.StartAsync();

            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseSqlServer(_sqlContainer.GetConnectionString())
                .Options;

            _dbContext = new AuthDbContext(options);
            await _dbContext.Database.MigrateAsync();

            var userRepository = new UserRepository(_dbContext);

            var services = new ServiceCollection();
            services.AddScoped<IUserRepository>(_ => userRepository);
            var serviceProvider = services.BuildServiceProvider();
            var unitOfWork = new AuthUnitOfWork(_dbContext, serviceProvider);

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserMapper>()).CreateMapper();

            var jwtSettings = Options.Create(new JwtSettings
            {
                Secret = "test-secret-key-for-jwt-settings",
                Issuer = "test",
                Audience = "test",
                ExpirationMinutes = 5
            });
            var tokenService = new TokenService(jwtSettings);

            _authService = new AuthService(unitOfWork, mapper, tokenService);
        }
        public async Task DisposeAsync()
        {
            await _dbContext.DisposeAsync();
            await _sqlContainer.DisposeAsync();
        }

        [Fact]
        public async Task RegisterAsync_WhenEmailIsNew_AddUserToDatabase() 
        { 
            var dto = new RegisterRequestDto{ Email = "test@test.com", Password = "123456" };

            await _authService.RegisterAsync(dto);

            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == dto.Email);
            Assert.NotNull(user);
        }
    }
}
