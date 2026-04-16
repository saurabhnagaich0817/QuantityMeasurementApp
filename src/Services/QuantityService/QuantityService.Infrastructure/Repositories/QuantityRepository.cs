using Microsoft.EntityFrameworkCore;
using QuantityService.Core.Entities;
using QuantityService.Core.Interfaces;
using QuantityService.Infrastructure.Data;

namespace QuantityService.Infrastructure.Repositories
{
    public class QuantityRepository : IQuantityRepository
    {
        private readonly QuantityDbContext _context;

        public QuantityRepository(QuantityDbContext context)
        {
            _context = context;
        }

        public async Task<Quantity?> GetByIdAsync(Guid id)
        {
            return await _context.Quantities.FindAsync(id);
        }

        public async Task<Quantity> CreateAsync(Quantity quantity)
        {
            quantity.Id = Guid.NewGuid();
            quantity.CreatedAt = DateTime.UtcNow;
            
            _context.Quantities.Add(quantity);
            await _context.SaveChangesAsync();
            
            return quantity;
        }

        public async Task<IEnumerable<Quantity>> GetAllAsync()
        {
            return await _context.Quantities.ToListAsync();
        }
    }
}