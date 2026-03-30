using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Entities;
using RepoLayer.Context;
using RepoLayer.Interfaces;

namespace RepoLayer.Repositories
{
    /// <summary>
    /// Provides an implementation of IQuantityRepository backed by a real database via EF Core.
    /// This is used by the console app when configured to use a database connection string.
    /// </summary>
    public class QuantityDatabaseRepository : IQuantityRepository
    {
        private readonly QuantityRepository _innerRepository;
        private readonly AppDbContext _dbContext;

        public QuantityDatabaseRepository(string connectionString)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            _dbContext = new AppDbContext(options);

            // Ensure database is created and migrations are applied (if any)
            _dbContext.Database.Migrate();

            _innerRepository = new QuantityRepository(_dbContext);
        }

        // ===== Async Methods (EF Core) =====
        public Task<QuantityMeasurementEntity> SaveToDatabaseAsync(QuantityMeasurementEntity entity)
            => _innerRepository.SaveToDatabaseAsync(entity);

        public Task<List<QuantityMeasurementEntity>> GetAllFromDatabaseAsync()
            => _innerRepository.GetAllFromDatabaseAsync();

        public Task<int> GetTotalCountAsync()
            => _innerRepository.GetTotalCountAsync();

        public Task<List<QuantityMeasurementEntity>> GetByOperationTypeAsync(string operationType)
            => _innerRepository.GetByOperationTypeAsync(operationType);

        public Task<List<QuantityMeasurementEntity>> GetByUserIdAsync(int userId)
            => _innerRepository.GetByUserIdAsync(userId);

        // ===== Sync Methods (for backward compatibility) =====
        public QuantityMeasurementEntity SaveToDatabase(QuantityMeasurementEntity entity)
            => _innerRepository.SaveToDatabase(entity);

        public List<QuantityMeasurementEntity> GetAllFromDatabase()
            => _innerRepository.GetAllFromDatabase();

        public int GetTotalCount()
            => _innerRepository.GetTotalCount();

        public List<QuantityMeasurementEntity> GetByOperationType(string operationType)
            => _innerRepository.GetByOperationType(operationType);
    }
}
