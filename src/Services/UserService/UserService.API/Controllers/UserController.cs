using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Core.DTOs;
using UserService.Core.Interfaces;
using UserService.Core.Helpers;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusinessService _userService;

        public UserController(IUserBusinessService userService)
        {
            _userService = userService;
        }

        private Guid GetCurrentUserId()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader))
                return Guid.Empty;
                
            return JwtHelper.GetUserIdFromToken(authHeader);
        }

        // GET: api/user/profile
        [HttpGet("profile")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UserResponse>>> GetProfile()
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new ApiResponse<UserResponse>
                {
                    Success = false,
                    Message = "Invalid token",
                    ErrorCode = "INVALID_TOKEN"
                });
            }

            var result = await _userService.GetUserByIdAsync(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UserResponse>>> GetUserById(Guid id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        // PUT: api/user/profile
        [HttpPut("profile")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<UserResponse>>> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new ApiResponse<UserResponse>
                {
                    Success = false,
                    Message = "Invalid token",
                    ErrorCode = "INVALID_TOKEN"
                });
            }

            var result = await _userService.UpdateProfileAsync(userId, request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // PUT: api/user/password
        [HttpPut("password")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<bool>>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Invalid token",
                    ErrorCode = "INVALID_TOKEN"
                });
            }

            var result = await _userService.ChangePasswordAsync(userId, request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // DELETE: api/user
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteUser()
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Invalid token",
                    ErrorCode = "INVALID_TOKEN"
                });
            }

            var result = await _userService.DeleteUserAsync(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // POST: api/user/register (Internal endpoint for Auth Service)
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<UserResponse>>> RegisterUser([FromBody] RegisterUserRequest request)
        {
            var createRequest = new CreateUserRequest
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            };
            
            var result = await _userService.CreateUserAsync(createRequest);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // POST: api/user/validate (Internal endpoint for Auth Service)
        [HttpPost("validate")]
        [AllowAnonymous]
        public async Task<ActionResult<ValidateCredentialsResponse>> ValidateCredentials([FromBody] ValidateCredentialsRequest request)
        {
            var result = await _userService.ValidateUserCredentialsAsync(request.Email, request.Password);
            return Ok(result);
        }
    }
}