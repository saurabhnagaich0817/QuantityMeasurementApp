using Microsoft.AspNetCore.Mvc;
using AuthService.Core.DTOs;
using AuthService.Core.Interfaces;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserServiceClient _userServiceClient;
        private readonly IJwtService _jwtService;

        public AuthController(
            IUserServiceClient userServiceClient,
            IJwtService jwtService)
        {
            _userServiceClient = userServiceClient;
            _jwtService = jwtService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
        {
            // Call User Service to create user
            var registrationResult = await _userServiceClient.RegisterUserAsync(
                request.Username,
                request.Email,
                request.Password);

            if (!registrationResult.Success || !registrationResult.UserId.HasValue)
            {
                return BadRequest(new AuthResponse
                {
                    Success = false,
                    Message = registrationResult.ErrorMessage ?? "Registration failed"
                });
            }

            // Generate JWT token with userId from User Service
            var token = _jwtService.GenerateToken(
                registrationResult.UserId.Value,
                registrationResult.Email ?? request.Email,
                registrationResult.Username ?? request.Username);

            return Ok(new AuthResponse
            {
                Success = true,
                Message = "Registration successful",
                Token = token,
                Email = registrationResult.Email ?? request.Email,
                Username = registrationResult.Username ?? request.Username,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            });
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            // Call User Service to validate credentials
            var validationResult = await _userServiceClient.ValidateUserAsync(
                request.Email,
                request.Password);

            if (!validationResult.IsValid || !validationResult.UserId.HasValue)
            {
                return Unauthorized(new AuthResponse
                {
                    Success = false,
                    Message = validationResult.ErrorMessage ?? "Invalid email or password"
                });
            }

            // Generate JWT token with userId from User Service
            var token = _jwtService.GenerateToken(
                validationResult.UserId.Value,
                validationResult.Email ?? request.Email,
                validationResult.Username ?? "");

            return Ok(new AuthResponse
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                Email = validationResult.Email ?? request.Email,
                Username = validationResult.Username ?? "",
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            });
        }

        // POST: api/auth/google-login (keep as is for now)
        [HttpPost("google-login")]
        public async Task<ActionResult<AuthResponse>> GoogleLogin(GoogleLoginRequest request)
        {
            // Google login implementation - will also need to call User Service
            // For now, return not implemented
            return BadRequest(new AuthResponse
            {
                Success = false,
                Message = "Google login needs to be integrated with User Service"
            });
        }

        // GET: api/auth/verify
        [HttpGet("verify")]
        public IActionResult VerifyToken()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { valid = false, message = "No token provided" });
            }

            var isValid = _jwtService.ValidateToken(token);
            
            return Ok(new { valid = isValid });
        }
    }
}