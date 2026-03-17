using RepoLayer.Interfaces;
using ModelLayer.Models;
using System.Collections.Generic;
using System;

namespace RepoLayer.Repositories
{
    /// <summary>
    /// Provides a repository implementation for quantity storage operations.
    /// Updated for UC16 to support both cache and database
    /// </summary>
    public class QuantityRepository : IQuantityRepository
    {
        // In-memory cache for UC16 compatibility
        private static List<QuantityMeasurementEntity> _memoryCache = new List<QuantityMeasurementEntity>();
        
        /// <summary>
        /// Saves the given quantity instance.
        /// </summary>
        public Quantity<T> Save<T>(Quantity<T> quantity) where T : struct, Enum
        {
            return quantity;
        }
        
        // UC16: Save to memory cache
        public QuantityMeasurementEntity SaveToDatabase(QuantityMeasurementEntity entity)
        {
            entity.Id = _memoryCache.Count + 1;
            entity.CreatedAt = DateTime.Now;
            _memoryCache.Add(entity);
            return entity;
        }
        
        // UC16: Get from memory cache
        public List<QuantityMeasurementEntity> GetAllFromDatabase()
        {
            return new List<QuantityMeasurementEntity>(_memoryCache);
        }
        
        // UC16: Get by operation from cache
        public List<QuantityMeasurementEntity> GetByOperationType(string operationType)
        {
            return _memoryCache.FindAll(e => e.OperationType == operationType);
        }
        
        // UC16: Get count from cache
        public int GetTotalCount()
        {
            return _memoryCache.Count;
        }
    }
}