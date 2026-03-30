using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer.Entities;

namespace RepoLayer.Interfaces
{
    /// <summary>
    /// Repository interface for persisting and retrieving quantity measurement operations.
    /// Supports both synchronous and asynchronous data access patterns.
    /// </summary>
    public interface IQuantityRepository
    {
        /// <summary>Saves a measurement operation to the database synchronously. (Deprecated - use async version)</summary>
        QuantityMeasurementEntity SaveToDatabase(QuantityMeasurementEntity entity);

        /// <summary>Retrieves all measurement operations synchronously. (Deprecated - use async version)</summary>
        List<QuantityMeasurementEntity> GetAllFromDatabase();

        /// <summary>Gets the total count of all measurement operations synchronously. (Deprecated - use async version)</summary>
        int GetTotalCount();

        /// <summary>Retrieves operations filtered by type synchronously. (Deprecated - use async version)</summary>
        List<QuantityMeasurementEntity> GetByOperationType(string operationType);

        /// <summary>Saves a measurement operation asynchronously.</summary>
        Task<QuantityMeasurementEntity> SaveToDatabaseAsync(QuantityMeasurementEntity entity);

        /// <summary>Retrieves all measurement operations asynchronously.</summary>
        Task<List<QuantityMeasurementEntity>> GetAllFromDatabaseAsync();

        /// <summary>Gets the total count of measurement operations asynchronously.</summary>
        Task<int> GetTotalCountAsync();

        /// <summary>Retrieves operations filtered by operation type asynchronously.</summary>
        Task<List<QuantityMeasurementEntity>> GetByOperationTypeAsync(string operationType);

        /// <summary>Retrieves all operations performed by a specific user asynchronously.</summary>
        Task<List<QuantityMeasurementEntity>> GetByUserIdAsync(int userId);
    }
}
