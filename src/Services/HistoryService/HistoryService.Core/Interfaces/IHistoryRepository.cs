using HistoryService.Core.Entities;

namespace HistoryService.Core.Interfaces
{
    public interface IHistoryRepository
    {
        Task<HistoryEntry> AddAsync(HistoryEntry entry);
        Task<IEnumerable<HistoryEntry>> GetByUserIdAsync(Guid userId);
        Task<HistoryEntry?> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<int> DeleteByUserIdAsync(Guid userId);
        Task<bool> ExistsAsync(Guid id);
    }
}