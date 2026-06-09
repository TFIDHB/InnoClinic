using AutoMapper;
using BLL.AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Services;
using BLL.Settings;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Testcontainers.MsSql;

namespace Auth.API.Tests.Integration_Tests
{
    public class ServiceAssemblyResult
    {
        public AuthDbContext DbContext { get; set; }
        public AuthService AuthService { get; set; }
    }

    public class AuthServiceIntegrationTests : IAsyncLifetime
    {
        private readonly MsSqlContainer _sqlContainer;
        private DbContextOptions<AuthDbContext> _contextOptions;
        private AuthDbContext _dbContext;
        private AuthService _authService;
        private IMapper _mapper;
        private TokenService _tokenService;

        public AuthServiceIntegrationTests()
        {
            _sqlContainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _sqlContainer.StartAsync();

            _contextOptions = new DbContextOptionsBuilder<AuthDbContext>()
                .UseSqlServer(_sqlContainer.GetConnectionString())
                .Options;

            _dbContext = new AuthDbContext(_contextOptions);
            await _dbContext.Database.MigrateAsync();

            var userRepository = new UserRepository(_dbContext);

            var services = new ServiceCollection();
            services.AddScoped<IUserRepository>(_ => userRepository);
            var serviceProvider = services.BuildServiceProvider();
            var unitOfWork = new AuthUnitOfWork(_dbContext, serviceProvider);

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserMapper>()).CreateMapper();

            var jwtSettings = Options.Create(new JwtSettings
            {
                Secret = "test-secret-key-for-jwt-settings",
                Issuer = "test",
                Audience = "test",
                ExpirationMinutes = 5
            });
            _tokenService = new TokenService(jwtSettings);

            _authService = new AuthService(unitOfWork, _mapper, _tokenService);
        }

        private ServiceAssemblyResult CreateServiceContext()
        {
            var dbContext = new AuthDbContext(_contextOptions);
            var userRepository = new UserRepository(dbContext);

            var services = new ServiceCollection();
            services.AddScoped<IUserRepository>(_ => userRepository);
            var serviceProvider = services.BuildServiceProvider();

            var unitOfWork = new AuthUnitOfWork(dbContext, serviceProvider);
            var authService = new AuthService(unitOfWork, _mapper, _tokenService);

            return new ServiceAssemblyResult
            {
                DbContext = dbContext,
                AuthService = authService
            };
        }

        public async Task DisposeAsync()
        {
            await _dbContext.DisposeAsync();
            await _sqlContainer.DisposeAsync();
        }

        [Fact]
        public async Task RegisterAsync_WhenEmailIsNew_AddUserToDatabase()
        {
            var email = "test@test.com";
            var password = "123456";
            var dto = new RegisterRequestDto { Email = email, Password = password };

            await _authService.RegisterAsync(dto);

            _dbContext.ChangeTracker.Clear();
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == email);
            Assert.NotNull(user);
        }

        [Fact]
        public async Task RegisterAsync_WhenEmailAlreadyExists_ThrowsEmailAlreadyExistsException()
        {
            var assembly = CreateServiceContext();
            var email = "duplicate@test.com";
            var password = "123456";
            var dto = new RegisterRequestDto { Email = email, Password = password };

            await assembly.AuthService.RegisterAsync(dto);

            await Assert.ThrowsAsync<EmailAlreadyExistsException>(async () =>
            {
                await assembly.AuthService.RegisterAsync(dto);
            });
        }

        [Fact]
        public async Task LoginAsync_WhenCredentialsAreValid_ReturnsTokens()
        {
            var assembly = CreateServiceContext();
            var email = "login@test.com";
            var password = "123456";

            var registerDto = new RegisterRequestDto { Email = email, Password = password };
            await assembly.AuthService.RegisterAsync(registerDto);
            var loginDto = new LoginRequestDto { Email = email, Password = password };

            var tokens = await assembly.AuthService.LoginAsync(loginDto);

            Assert.NotNull(tokens);
            Assert.False(string.IsNullOrEmpty(tokens.AccessToken));
            Assert.False(string.IsNullOrEmpty(tokens.RefreshToken));
        }

        [Fact]
        public async Task LoginAsync_WhenUserDoesNotExist_ThrowsUserNotFoundException()
        {
            var assembly = CreateServiceContext();
            var email = "doesnotexist@test.com";
            var password = "123456";
            var loginDto = new LoginRequestDto { Email = email, Password = password };

            await Assert.ThrowsAsync<UserNotFoundException>(async () =>
            {
                await assembly.AuthService.LoginAsync(loginDto);
            });
        }

        [Fact]
        public async Task LoginAsync_WhenPasswordIsIncorrect_ThrowsInvalidPasswordException()
        {
            var assembly = CreateServiceContext();
            var email = "wrongpassword@test.com";
            var correctPassword = "123456";
            var wrongPassword = "wrongPassword";

            var registerDto = new RegisterRequestDto { Email = email, Password = correctPassword };
            await assembly.AuthService.RegisterAsync(registerDto);
            var loginDto = new LoginRequestDto { Email = email, Password = wrongPassword };

            await Assert.ThrowsAsync<InvalidPasswordException>(async () =>
            {
                await assembly.AuthService.LoginAsync(loginDto);
            });
        }

        [Fact]
        public async Task LogoutAsync_WhenDataIsValid_ClearsRefreshTokenInDatabase()
        {
            var assembly = CreateServiceContext();
            var email = "logout@test.com";
            var password = "123456";

            var registerDto = new RegisterRequestDto { Email = email, Password = password };
            await assembly.AuthService.RegisterAsync(registerDto);
            var loginDto = new LoginRequestDto { Email = email, Password = password };
            var tokens = await assembly.AuthService.LoginAsync(loginDto);
            int userId;
            assembly.DbContext.ChangeTracker.Clear();
            var user = await assembly.DbContext.Users.FirstAsync(u => u.Email == email);
            userId = user.Id;
            var logoutDto = new LogOutRequestDto { RefreshToken = tokens.RefreshToken };

            await assembly.AuthService.LogoutAsync(logoutDto, userId);

            assembly.DbContext.ChangeTracker.Clear();
            var userAfterLogout = await assembly.DbContext.Users.FirstAsync(u => u.Id == userId);
            Assert.Null(userAfterLogout.RefreshToken);
            Assert.Null(userAfterLogout.RefreshTokenExpiry);
        }

        [Fact]
        public async Task LogoutAsync_WhenUserDoesNotExist_ThrowsUserNotFoundException()
        {
            var assembly = CreateServiceContext();
            var fakeToken = "some-token";
            var logoutDto = new LogOutRequestDto { RefreshToken = fakeToken };

            await Assert.ThrowsAsync<UserNotFoundException>(async () =>
            {
                await assembly.AuthService.LogoutAsync(logoutDto, userId: 9999);
            });
        }

        [Fact]
        public async Task LogoutAsync_WhenRefreshTokenIsInvalid_ThrowsInvalidTokenException()
        {
            var assembly = CreateServiceContext();
            var email = "invalidtoken@test.com";
            var password = "123456";
            var invalidToken = "wrong-token";

            var registerDto = new RegisterRequestDto { Email = email, Password = password };
            await assembly.AuthService.RegisterAsync(registerDto);
            var loginDto = new LoginRequestDto { Email = email, Password = password };
            await assembly.AuthService.LoginAsync(loginDto);
            int userId;
            assembly.DbContext.ChangeTracker.Clear();
            var user = await assembly.DbContext.Users.FirstAsync(u => u.Email == email);
            userId = user.Id;
            var logoutDto = new LogOutRequestDto { RefreshToken = invalidToken };

            await Assert.ThrowsAsync<InvalidTokenException>(async () =>
            {
                await assembly.AuthService.LogoutAsync(logoutDto, userId);
            });
        }
    }
}