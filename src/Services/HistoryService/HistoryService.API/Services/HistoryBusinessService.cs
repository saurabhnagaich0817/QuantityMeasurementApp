using HistoryService.Core.DTOs;
using HistoryService.Core.Entities;
using HistoryService.Core.Interfaces;

namespace HistoryService.API.Services
{
    public class HistoryBusinessService : IHistoryBusinessService
    {
        private readonly IHistoryRepository _repository;

        public HistoryBusinessService(IHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResponse<HistoryResponse>> SaveHistoryAsync(Guid userId, SaveHistoryRequest request)
        {
            try
            {
                var entry = new HistoryEntry
                {
                    UserId = userId,
                    OperationType = request.OperationType,
                    InputValues = request.InputValues,
                    Result = request.Result
                };

                var saved = await _repository.AddAsync(entry);

                return new ApiResponse<HistoryResponse>
                {
                    Success = true,
                    Message = "History saved successfully",
                    Data = new HistoryResponse
                    {
                        Id = saved.Id,
                        UserId = saved.UserId,
                        OperationType = saved.OperationType,
                        InputValues = saved.InputValues,
                        Result = saved.Result,
                        CreatedAt = saved.CreatedAt
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<HistoryResponse>
                {
                    Success = false,
                    Message = $"Failed to save history: {ex.Message}",
                    ErrorCode = "SAVE_ERROR"
                };
            }
        }

        public async Task<ApiResponse<IEnumerable<HistoryResponse>>> GetUserHistoryAsync(Guid userId)
        {
            try
            {
                var history = await _repository.GetByUserIdAsync(userId);
                
                var response = history.Select(h => new HistoryResponse
                {
                    Id = h.Id,
                    UserId = h.UserId,
                    OperationType = h.OperationType,
                    InputValues = h.InputValues,
                    Result = h.Result,
                    CreatedAt = h.CreatedAt
                });

                return new ApiResponse<IEnumerable<HistoryResponse>>
                {
                    Success = true,
                    Message = "History retrieved successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<HistoryResponse>>
                {
                    Success = false,
                    Message = $"Failed to retrieve history: {ex.Message}",
                    ErrorCode = "RETRIEVE_ERROR"
                };
            }
        }

        public async Task<ApiResponse<DeleteResponse>> DeleteHistoryAsync(Guid id, Guid userId)
        {
            try
            {
                var entry = await _repository.GetByIdAsync(id);
                if (entry == null)
                {
                    return new ApiResponse<DeleteResponse>
                    {
                        Success = false,
                        Message = "History entry not found",
                        ErrorCode = "NOT_FOUND"
                    };
                }

                // Verify ownership
                if (entry.UserId != userId)
                {
                    return new ApiResponse<DeleteResponse>
                    {
                        Success = false,
                        Message = "You don't have permission to delete this entry",
                        ErrorCode = "UNAUTHORIZED"
                    };
                }

                var deleted = await _repository.DeleteAsync(id);
                
                return new ApiResponse<DeleteResponse>
                {
                    Success = deleted,
                    Message = deleted ? "History entry deleted successfully" : "Failed to delete",
                    Data = new DeleteResponse
                    {
                        Success = deleted,
                        Message = deleted ? "Deleted successfully" : "Delete failed",
                        DeletedCount = deleted ? 1 : 0
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<DeleteResponse>
                {
                    Success = false,
                    Message = $"Failed to delete: {ex.Message}",
                    ErrorCode = "DELETE_ERROR"
                };
            }
        }

        public async Task<ApiResponse<DeleteResponse>> ClearUserHistoryAsync(Guid userId)
        {
            try
            {
                var deletedCount = await _repository.DeleteByUserIdAsync(userId);
                
                return new ApiResponse<DeleteResponse>
                {
                    Success = true,
                    Message = $"Cleared {deletedCount} history entries",
                    Data = new DeleteResponse
                    {
                        Success = true,
                        Message = $"Successfully cleared {deletedCount} entries",
                        DeletedCount = deletedCount
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<DeleteResponse>
                {
                    Success = false,
                    Message = $"Failed to clear history: {ex.Message}",
                    ErrorCode = "CLEAR_ERROR"
                };
            }
        }
    }
}