using BusinessLayer.Interfaces;
using ModelLayer.DTOs.Auth;
using ModelLayer.Entities;
using RepoLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            if (await _userRepository.EmailExistsAsync(request.Email))
                throw new Exception("Email already registered");

            if (await _userRepository.UsernameExistsAsync(request.Username))
                throw new Exception("Username already taken");

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = "User",  //  STRING
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var createdUser = await _userRepository.CreateAsync(user);
            var token = _jwtService.GenerateToken(createdUser);

            return new AuthResponseDto
            {
                Id = createdUser.Id,
                Username = createdUser.Username,
                Email = createdUser.Email,
                Role = createdUser.Role,  //  STRING from user
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password");

            if (!user.IsActive)
                throw new UnauthorizedAccessException("Account is deactivated");

            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,  // STRING from user
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }
    }
}
