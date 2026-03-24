using System;  // 👈 Add this
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer.Entities;

namespace RepoLayer.Interfaces
{
    public interface IQuantityRepository
    {
        // Sync methods (for backward compatibility)
        QuantityMeasurementEntity SaveToDatabase(QuantityMeasurementEntity entity);
        List<QuantityMeasurementEntity> GetAllFromDatabase();
        int GetTotalCount();
        List<QuantityMeasurementEntity> GetByOperationType(string operationType);

        // Async methods (for EF Core)
        Task<QuantityMeasurementEntity> SaveToDatabaseAsync(QuantityMeasurementEntity entity);
        Task<List<QuantityMeasurementEntity>> GetAllFromDatabaseAsync();
        Task<int> GetTotalCountAsync();
        Task<List<QuantityMeasurementEntity>> GetByOperationTypeAsync(string operationType);
        Task<List<QuantityMeasurementEntity>> GetByUserIdAsync(int userId);
    }
}
