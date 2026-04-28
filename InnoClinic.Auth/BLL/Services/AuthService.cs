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
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        public async Task RegisterAsync(RegisterRequestDto dto)
        {
            if (await _userRepository.ExistsByEmailAsync(dto.Email))
            {
                throw new EmailAlreadyExistsException();
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = passwordHash;

            await _userRepository.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<AuthTokenDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
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

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new AuthTokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task LogoutAsync(LogOutRequestDto dto, int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null) { 
                throw new UserNotFoundException();
            }

            if (user.RefreshTokenExpiry < DateTime.UtcNow ||
                !BCrypt.Net.BCrypt.Verify(dto.RefreshToken, user.RefreshToken))
            {
                throw new InvalidTokenException();
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiry = null;

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
