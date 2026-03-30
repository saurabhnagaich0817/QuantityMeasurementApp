using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Entities;
using RepoLayer.Context;
using RepoLayer.Interfaces;

namespace RepoLayer.Repositories
{
    /// <summary>
    /// Repository implementation for quantity measurement operations.
    /// Provides database access using Entity Framework Core.
    /// </summary>
    public class QuantityRepository : IQuantityRepository
    {
        private readonly AppDbContext _context;

        public QuantityRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>Saves a measurement operation to the database asynchronously.</summary>
        public async Task<QuantityMeasurementEntity> SaveToDatabaseAsync(QuantityMeasurementEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.QuantityMeasurements.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>Retrieves all measurement operations from the database, ordered by most recent first.</summary>
        public async Task<List<QuantityMeasurementEntity>> GetAllFromDatabaseAsync()
        {
            return await _context.QuantityMeasurements
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        /// <summary>Gets the total count of measurement operations in the database.</summary>
        public async Task<int> GetTotalCountAsync()
        {
            return await _context.QuantityMeasurements.CountAsync();
        }

        /// <summary>Retrieves measurement operations filtered by operation type.</summary>
        public async Task<List<QuantityMeasurementEntity>> GetByOperationTypeAsync(string operationType)
        {
            if (string.IsNullOrWhiteSpace(operationType))
                throw new ArgumentException("Operation type cannot be empty", nameof(operationType));

            return await _context.QuantityMeasurements
                .Where(q => q.OperationType == operationType)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        /// <summary>Retrieves all measurement operations performed by a specific user.</summary>
        public async Task<List<QuantityMeasurementEntity>> GetByUserIdAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than zero", nameof(userId));

            return await _context.QuantityMeasurements
                .Where(q => q.UserId == userId)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        // Synchronous methods for backward compatibility
        public QuantityMeasurementEntity SaveToDatabase(QuantityMeasurementEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.QuantityMeasurements.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public List<QuantityMeasurementEntity> GetAllFromDatabase()
        {
            return _context.QuantityMeasurements
                .OrderByDescending(q => q.CreatedAt)
                .ToList();
        }

        public int GetTotalCount()
        {
            return _context.QuantityMeasurements.Count();
        }

        public List<QuantityMeasurementEntity> GetByOperationType(string operationType)
        {
            if (string.IsNullOrWhiteSpace(operationType))
                throw new ArgumentException("Operation type cannot be empty", nameof(operationType));

            return _context.QuantityMeasurements
                .Where(q => q.OperationType == operationType)
                .OrderByDescending(q => q.CreatedAt)
                .ToList();
        }
    }
}