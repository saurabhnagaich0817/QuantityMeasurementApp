using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services;
using ModelLayer.DTOs.Auth;

namespace QuantityMeasurementApp.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] // Require authentication by default; allow anonymous on login/register
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAuthService authService,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Register a new user account
        /// </summary>
        /// <remarks>
        /// Creates a new user account with the provided registration details.
        /// The password is securely hashed using BCrypt before storage.
        /// 
        /// **Registration Process:**
        /// 1. Validate input data (email format, password strength)
        /// 2. Check if email is already registered
        /// 3. Hash the password securely
        /// 4. Create user record in database
        /// 5. Return authentication tokens
        /// 
        /// **Password Requirements:**
        /// - Minimum 8 characters
        /// - Should contain mix of letters, numbers, and special characters
        /// 
        /// **Example Request:**
        /// ```
        /// POST /api/v1/Auth/register
        /// {
        ///   "email": "user@example.com",
        ///   "password": "SecurePass123!",
        ///   "firstName": "John",
        ///   "lastName": "Doe"
        /// }
        /// ```
        /// </remarks>
        /// <param name="request">User registration details including email, password, and name</param>
        /// <response code="200">User registered successfully, returns authentication tokens</response>
        /// <response code="400">Registration failed (invalid data, email already exists, weak password)</response>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterRequestDto request)
        {
            try
            {
                var result = await _authService.RegisterAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Authenticate user and get access tokens
        /// </summary>
        /// <remarks>
        /// Authenticates a user with email and password credentials.
        /// Returns JWT access and refresh tokens upon successful authentication.
        /// 
        /// **Authentication Process:**
        /// 1. Validate email format and password presence
        /// 2. Retrieve user by email from database
        /// 3. Verify password using BCrypt comparison
        /// 4. Generate JWT access token with user claims
        /// 5. Generate refresh token for token renewal
        /// 6. Return tokens in response
        /// 
        /// **Token Details:**
        /// - Access Token: Short-lived (15-60 minutes), used for API access
        /// - Refresh Token: Long-lived, used to obtain new access tokens
        /// - Include access token in Authorization header: "Bearer {token}"
        /// 
        /// **Example Request:**
        /// ```
        /// POST /api/v1/Auth/login
        /// {
        ///   "email": "user@example.com",
        ///   "password": "SecurePass123!"
        /// }
        /// ```
        /// 
        /// **Example Response:**
        /// ```json
        /// {
        ///   "accessToken": "eyJhbGciOiJIUzI1NiIs...",
        ///   "refreshToken": "refresh_token_here",
        ///   "tokenType": "Bearer",
        ///   "expiresIn": 900
        /// }
        /// ```
        /// </remarks>
        /// <param name="request">User login credentials (email and password)</param>
        /// <response code="200">Login successful, returns JWT tokens</response>
        /// <response code="401">Login failed - invalid credentials</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var result = await _authService.LoginAsync(request);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }
    }
}