using System;  // IMPORTANT: Add this line
using ModelLayer.Models;
using System.Collections.Generic;

namespace RepoLayer.Interfaces
{
    public interface IQuantityRepository
    {
        // Original method - with proper constraint
        Quantity<T> Save<T>(Quantity<T> quantity) where T : struct, Enum;
        
        // UC16 Database Methods
        QuantityMeasurementEntity SaveToDatabase(QuantityMeasurementEntity entity);
        List<QuantityMeasurementEntity> GetAllFromDatabase();
        int GetTotalCount();
        List<QuantityMeasurementEntity> GetByOperationType(string operationType);
    }
}