using System;  
using System.Collections.Generic;
using System.Linq;  
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Entities;
using RepoLayer.Context;
using RepoLayer.Interfaces;
namespace RepoLayer.Persistence
{
    public class QuantityRepository : IQuantityRepository
    {
        private readonly AppDbContext _context;

        public QuantityRepository(AppDbContext context)
        {
            _context = context;
        }

        // ===== Async Methods (EF Core) =====
        public async Task<QuantityMeasurementEntity> SaveToDatabaseAsync(QuantityMeasurementEntity entity)
        {
            await _context.QuantityMeasurements.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<QuantityMeasurementEntity>> GetAllFromDatabaseAsync()
        {
            return await _context.QuantityMeasurements
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.QuantityMeasurements.CountAsync();
        }

        public async Task<List<QuantityMeasurementEntity>> GetByOperationTypeAsync(string operationType)
        {
            return await _context.QuantityMeasurements
                .Where(q => q.OperationType == operationType)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<QuantityMeasurementEntity>> GetByUserIdAsync(int userId)
        {
            return await _context.QuantityMeasurements
                .Where(q => q.UserId == userId)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        // ===== Sync Methods (for backward compatibility) =====
        public QuantityMeasurementEntity SaveToDatabase(QuantityMeasurementEntity entity)
        {
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
            return _context.QuantityMeasurements
                .Where(q => q.OperationType == operationType)
                .OrderByDescending(q => q.CreatedAt)
                .ToList();
        }
    }
}
