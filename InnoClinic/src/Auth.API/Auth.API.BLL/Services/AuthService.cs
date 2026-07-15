using AutoMapper;
using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthService(
            IAuthUnitOfWork unitOfWork,
            IMapper mapper,
            ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        public async Task RegisterAsync(RegisterRequestDto dto, CancellationToken ct = default)
        {
            if (await _unitOfWork.UserRepository.ExistsByEmailAsync(dto.Email, ct))
            {
                throw new EmailAlreadyExistsException();
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = passwordHash;

            await _unitOfWork.UserRepository.CreateAsync(user, ct);
            await _unitOfWork.SaveChangesAsync(ct);
        }
        public async Task<AuthTokenDto> LoginAsync(LoginRequestDto dto, CancellationToken ct = default)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email, ct);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new InvalidPasswordException();
            }

            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken);

            user.RefreshToken = refreshTokenHash;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            await _unitOfWork.UserRepository.UpdateAsync(user, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return new AuthTokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
        public async Task LogoutAsync(LogOutRequestDto dto, Guid userId, CancellationToken ct = default)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId, ct);

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

            await _unitOfWork.SaveChangesAsync(ct);
        }
    }
}