using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HistoryService.Core.DTOs;
using HistoryService.Core.Interfaces;

namespace HistoryService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryBusinessService _historyService;

        public HistoryController(IHistoryBusinessService historyService)
        {
            _historyService = historyService;
        }

        // Get current user ID from JWT token
        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Guid.Empty;
            
            return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
        }

        // POST: api/history
        [HttpPost]
        public async Task<ActionResult<ApiResponse<HistoryResponse>>> SaveHistory([FromBody] SaveHistoryRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new ApiResponse<HistoryResponse>
                {
                    Success = false,
                    Message = "Invalid user",
                    ErrorCode = "INVALID_USER"
                });
            }

            var result = await _historyService.SaveHistoryAsync(userId, request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // GET: api/history
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<HistoryResponse>>>> GetMyHistory()
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new ApiResponse<IEnumerable<HistoryResponse>>
                {
                    Success = false,
                    Message = "Invalid user",
                    ErrorCode = "INVALID_USER"
                });
            }

            var result = await _historyService.GetUserHistoryAsync(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // GET: api/history/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<HistoryResponse>>>> GetUserHistory(Guid userId)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == Guid.Empty)
            {
                return Unauthorized(new ApiResponse<IEnumerable<HistoryResponse>>
                {
                    Success = false,
                    Message = "Invalid user",
                    ErrorCode = "INVALID_USER"
                });
            }

            // Users can only see their own history
            if (currentUserId != userId)
            {
                return Forbid();
            }

            var result = await _historyService.GetUserHistoryAsync(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // DELETE: api/history/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<DeleteResponse>>> DeleteHistory(Guid id)
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new ApiResponse<DeleteResponse>
                {
                    Success = false,
                    Message = "Invalid user",
                    ErrorCode = "INVALID_USER"
                });
            }

            var result = await _historyService.DeleteHistoryAsync(id, userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // DELETE: api/history/user/clear
        [HttpDelete("user/clear")]
        public async Task<ActionResult<ApiResponse<DeleteResponse>>> ClearMyHistory()
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new ApiResponse<DeleteResponse>
                {
                    Success = false,
                    Message = "Invalid user",
                    ErrorCode = "INVALID_USER"
                });
            }

            var result = await _historyService.ClearUserHistoryAsync(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}