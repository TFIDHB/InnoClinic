using Azure;
using BLL.DTOs;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Testcontainers.MsSql;

namespace Auth.API.Tests
{
    public class AuthControllerIntegrationTests : IAsyncLifetime
    {
        private readonly MsSqlContainer _sqlContainer;
        private HttpClient _client;
        public AuthControllerIntegrationTests()
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

            var factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services => 
                    {
                        var descriptor = services.SingleOrDefault(e => e.ServiceType == typeof(DbContextOptions<AuthDbContext>));

                        services.AddDbContext<AuthDbContext>(opt => opt.UseSqlServer(_sqlContainer.GetConnectionString()));
                    });
                });

            _client = factory.CreateClient();
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            await db.Database.MigrateAsync();
        }

        [Fact]
        public async Task Register_WhenUserDataIsValid_ReturnsOk() 
        {
            var email = "test@test.com";
            var password = "123456";
            var dto = new RegisterRequestDto { Email = email, Password = password };

            var result = await _client.PostAsJsonAsync("api/auth/register", dto);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Register_WhenUserDataIsInvalid_ReturnsBadRequest()
        {
            var email = "1111";
            var password = "123456";
            var dto = new RegisterRequestDto { Email = email, Password = password };

            var result = await _client.PostAsJsonAsync("api/auth/register", dto);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Register_WhenEmailAlreadyExists_ReturnsBadRequest()
        {
            var email = "duplicate@test.com";
            var password = "123456";
            var dto = new RegisterRequestDto { Email = email, Password = password};
            await _client.PostAsJsonAsync("api/auth/register", dto);

            var result = await _client.PostAsJsonAsync("api/auth/register", dto);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Login_WhenUserCredentalsCorrect_ReturnsOkAndToken()
        {
            var email = "login@test.com";
            var password = "123456";

            var registerDto = new RegisterRequestDto { Email = email, Password = password };
            await _client.PostAsJsonAsync("api/auth/register", registerDto);
            var loginDto = new LoginRequestDto {Email = email, Password = password };

            var result = await _client.PostAsJsonAsync("api/auth/login", loginDto);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            var tokens = await result.Content.ReadFromJsonAsync<AuthTokenDto>();
            Assert.NotNull(tokens);
        }

        [Fact]
        public async Task Login_WhenUserDoesNotExist_ReturnsBadRequest()
        {
            var email = "doesnotexist@test.com";
            var password = "123456";
            var loginDto = new LoginRequestDto { Email = email, Password = password };

            var result = await _client.PostAsJsonAsync("api/auth/login", loginDto);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Login_WhenPasswordIsIncorrect_ReturnsBadRequest()
        {
            var email = "doesnotexist@test.com";
            var password = "123456";
            var registerDto = new RegisterRequestDto { Email = email, Password = password };
            await _client.PostAsJsonAsync("api/auth/register", registerDto);
            var loginDto = new LoginRequestDto { Email = email, Password = "wrong-password" };

            var result = await _client.PostAsJsonAsync("api/auth/login", loginDto);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Logout_WhenUserIsAuthorized_ReturnsOk() 
        {
            var email = "logout@test.com";
            var password = "123456";

            var registerDto = new RegisterRequestDto {Email = email, Password = password };
            await _client.PostAsJsonAsync("api/auth/register", registerDto);
            var loginDto = new LoginRequestDto {Email = email, Password = password };
            var loginResult = await _client.PostAsJsonAsync("api/auth/login", loginDto);
            var tokens = await loginResult.Content.ReadFromJsonAsync<AuthTokenDto>();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            var logoutDto = new LogOutRequestDto { RefreshToken = tokens.RefreshToken };

            var result = await _client.PostAsJsonAsync("api/auth/logout", logoutDto);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Logout_WhenUserIsUnauthorized_ReturnsBadRequest()
        {
            _client.DefaultRequestHeaders.Authorization = null;
            var logoutDto = new LogOutRequestDto { RefreshToken = "" };

            var result = await _client.PostAsJsonAsync("api/auth/logout", logoutDto);

            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async Task Logout_WhenRefreshTokenIsInvalid_ReturnsBadRequest()
        {
            var email = "invalidrefreshtoken@test.com";
            var password = "123456";

            var registerDto = new RegisterRequestDto { Email = email, Password = password };
            await _client.PostAsJsonAsync("api/auth/register", registerDto);
            var loginDto = new LoginRequestDto { Email = email, Password = password };
            var loginResult = await _client.PostAsJsonAsync("api/auth/login", loginDto);
            var tokens = await loginResult.Content.ReadFromJsonAsync<AuthTokenDto>();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            var logoutDto = new LogOutRequestDto { RefreshToken = "wrong-refresh-token" };

            var result = await _client.PostAsJsonAsync("api/auth/logout", logoutDto);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
