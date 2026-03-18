using System;  // Add this line
using RepoLayer.Interfaces;
using ModelLayer.Models;
using System.Collections.Generic;

namespace RepoLayer.Repositories
{
    public class QuantityRepository : IQuantityRepository
    {
        private static List<QuantityMeasurementEntity> _memoryCache = new List<QuantityMeasurementEntity>();
        
        // Fix: Add proper constraint
        public Quantity<T> Save<T>(Quantity<T> quantity) where T : struct, Enum
        {
            return quantity;
        }
        
        public QuantityMeasurementEntity SaveToDatabase(QuantityMeasurementEntity entity)
        {
            entity.Id = _memoryCache.Count + 1;
            entity.CreatedAt = DateTime.Now;
            _memoryCache.Add(entity);
            return entity;
        }
        
        public List<QuantityMeasurementEntity> GetAllFromDatabase()
        {
            return new List<QuantityMeasurementEntity>(_memoryCache);
        }
        
        public List<QuantityMeasurementEntity> GetByOperationType(string operationType)
        {
            return _memoryCache.FindAll(e => e.OperationType == operationType);
        }
        
        public int GetTotalCount()
        {
            return _memoryCache.Count;
        }
    }
}