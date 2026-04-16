using Microsoft.EntityFrameworkCore;
using HistoryService.Core.Entities;
using HistoryService.Core.Interfaces;
using HistoryService.Infrastructure.Data;

namespace HistoryService.Infrastructure.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly HistoryDbContext _context;

        public HistoryRepository(HistoryDbContext context)
        {
            _context = context;
        }

        public async Task<HistoryEntry> AddAsync(HistoryEntry entry)
        {
            entry.Id = Guid.NewGuid();
            entry.CreatedAt = DateTime.UtcNow;
            
            _context.HistoryEntries.Add(entry);
            await _context.SaveChangesAsync();
            
            return entry;
        }

        public async Task<IEnumerable<HistoryEntry>> GetByUserIdAsync(Guid userId)
        {
            return await _context.HistoryEntries
                .Where(h => h.UserId == userId)
                .OrderByDescending(h => h.CreatedAt)
                .ToListAsync();
        }

        public async Task<HistoryEntry?> GetByIdAsync(Guid id)
        {
            return await _context.HistoryEntries.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entry = await _context.HistoryEntries.FindAsync(id);
            if (entry == null)
                return false;
                
            _context.HistoryEntries.Remove(entry);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> DeleteByUserIdAsync(Guid userId)
        {
            var entries = await _context.HistoryEntries
                .Where(h => h.UserId == userId)
                .ToListAsync();
                
            var count = entries.Count;
            _context.HistoryEntries.RemoveRange(entries);
            await _context.SaveChangesAsync();
            
            return count;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.HistoryEntries.AnyAsync(h => h.Id == id);
        }
    }
}