using HistoryService.Core.DTOs;

namespace HistoryService.Core.Interfaces
{
    public interface IHistoryBusinessService
    {
        Task<ApiResponse<HistoryResponse>> SaveHistoryAsync(Guid userId, SaveHistoryRequest request);
        Task<ApiResponse<IEnumerable<HistoryResponse>>> GetUserHistoryAsync(Guid userId);
        Task<ApiResponse<DeleteResponse>> DeleteHistoryAsync(Guid id, Guid userId);
        Task<ApiResponse<DeleteResponse>> ClearUserHistoryAsync(Guid userId);
    }
}