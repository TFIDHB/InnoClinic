using AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class AuthService(
        ITokenService tokenService,
        IAuthUnitOfWork unitOfWork,
        IMapper mapper,
        IProfilesClient profilesClient) : IAuthService
    {
        public async Task RegisterAsync(RegisterRequestDto dto, CancellationToken ct = default)
        {
            if (await unitOfWork.UserRepository.ExistsByEmailAsync(dto.Email, ct))
            {
                throw new EmailAlreadyExistsException();
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = mapper.Map<User>(dto);
            user.PasswordHash = passwordHash;

            await unitOfWork.UserRepository.CreateAsync(user, ct);
            await unitOfWork.SaveChangesAsync(ct);
        }
        public async Task<AuthTokenDto> LoginAsync(LoginRequestDto dto, CancellationToken ct = default)
        {
            var user = await unitOfWork.UserRepository.GetByEmailAsync(dto.Email, ct);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new InvalidPasswordException();
            }

            var profileInfo = await profilesClient.GetProfileInfoByAccountIdAsync(user.Id, ct);
            if (profileInfo == null)
            {
                throw new UserNotFoundException();
            }

            if (profileInfo.Role == "Doctor" && profileInfo.Status == "Inactive")
            {
                throw new UserNotFoundException();
            }

            var accessToken = tokenService.GenerateAccessToken(user, profileInfo.Role);
            var refreshToken = tokenService.GenerateRefreshToken();

            var refreshTokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken);

            user.RefreshToken = refreshTokenHash;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            await unitOfWork.UserRepository.UpdateAsync(user, ct);
            await unitOfWork.SaveChangesAsync(ct);

            return new AuthTokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
        public async Task LogoutAsync(LogOutRequestDto dto, Guid userId, CancellationToken ct = default)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(userId, ct);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (user.RefreshTokenExpiry < DateTime.UtcNow ||
                !BCrypt.Net.BCrypt.Verify(dto.RefreshToken, user.RefreshToken))
            {
                throw new InvalidTokenException();
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiry = null;

            await unitOfWork.SaveChangesAsync(ct);
        }
    }
}