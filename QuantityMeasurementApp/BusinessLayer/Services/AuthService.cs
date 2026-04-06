using BusinessLayer.Interfaces;
using ModelLayer.DTOs.Auth;
using ModelLayer.Entities;
using RepoLayer.Interfaces;
using Google.Apis.Auth;

namespace BusinessLayer.Services
{
    /// <summary>
    /// Service for user authentication including registration and login.
    /// Handles password hashing, validation, and token generation.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        /// <summary>
        /// Registers a new user account with the provided credentials.
        /// </summary>
        /// <param name="request">Registration request containing username, email, and password.</param>
        /// <returns>Authentication response with JWT token.</returns>
        /// <exception cref="InvalidOperationException">Thrown if email or username already exists.</exception>
        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (await _userRepository.EmailExistsAsync(request.Email))
                throw new InvalidOperationException($"Email '{request.Email}' is already registered");

            if (await _userRepository.UsernameExistsAsync(request.Username))
                throw new InvalidOperationException($"Username '{request.Username}' is already taken");

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = "User",
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
                Role = createdUser.Role,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }

        /// <summary>
        /// Authenticates a user with email and password.
        /// </summary>
        /// <param name="request">Login request containing email and password.</param>
        /// <returns>Authentication response with JWT token.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if credentials are invalid or account is inactive.</exception>
        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

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
                Role = user.Role,
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }

        /// <summary>
        /// Authenticates a user using Google OAuth ID token.
        /// </summary>
        /// <param name="request">Google login request containing ID token.</param>
        /// <returns>Authentication response with JWT token.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown if token is invalid or account is inactive.</exception>
        public async Task<AuthResponseDto> GoogleLoginAsync(GoogleLoginRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.IdToken))
                throw new ArgumentException("ID token is required", nameof(request.IdToken));

            try
            {
                // Validate the Google ID token
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
                
                if (payload == null)
                    throw new UnauthorizedAccessException("Invalid Google token");

                // Check if user already exists by email
                var existingUser = await _userRepository.GetByEmailAsync(payload.Email);
                
                User user;
                if (existingUser != null)
                {
                    // User exists, check if active
                    if (!existingUser.IsActive)
                        throw new UnauthorizedAccessException("Account is deactivated");
                    
                    user = existingUser;
                }
                else
                {
                    // Create new user from Google payload
                    user = new User
                    {
                        Username = payload.Email.Split('@')[0], // Use email prefix as username
                        Email = payload.Email,
                        PasswordHash = "", // No password for Google users
                        Role = "User",
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };
                    
                    user = await _userRepository.CreateAsync(user);
                }

                var token = _jwtService.GenerateToken(user);

                return new AuthResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role,
                    Token = token,
                    ExpiresAt = DateTime.UtcNow.AddHours(24)
                };
            }
            catch (InvalidJwtException)
            {
                throw new UnauthorizedAccessException("Invalid Google token");
            }
            catch (Exception ex) when (ex is not UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException("Google authentication failed");
            }
        }
    }
}
