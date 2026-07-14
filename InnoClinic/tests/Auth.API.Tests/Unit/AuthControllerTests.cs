using BLL.DTOs;
using BLL.Interfaces;
using InnoClinic.Auth.API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;

namespace Auth.API.Tests.Unit
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authService;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _authService = new Mock<IAuthService>();
            _authController = new AuthController(_authService.Object);
        }

        public void SetUserClaims(int? userId)
        {
            var claims = userId.HasValue ? new[] { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) } : null;

            var identity = new ClaimsIdentity(claims);
            var principal = new ClaimsPrincipal(identity);

            _authController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };
        }

        [Fact]
        public async Task Logout_WhenUserIsAuthorized_ReturnsOk()
        {
            SetUserClaims(1);
            var dto = new LogOutRequestDto { RefreshToken = "some-token" };

            var result = await _authController.Logout(dto);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Logout_WhenUserIsUnauthorized_ReturnsUnauthorized()
        {
            SetUserClaims(null);
            var dto = new LogOutRequestDto { RefreshToken = "some-token" };

            var result = await _authController.Logout(dto);

            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }
    }
}
