using QuantityService.Core.Entities;

namespace QuantityService.Core.Interfaces
{
    public interface IQuantityRepository
    {
        Task<Quantity?> GetByIdAsync(Guid id);
        Task<Quantity> CreateAsync(Quantity quantity);
        Task<IEnumerable<Quantity>> GetAllAsync();
    }
}