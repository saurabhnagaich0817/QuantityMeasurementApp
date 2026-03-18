using System;
using System.Collections.Generic;
using System.Threading.Tasks;  // IMPORTANT: Add this line
using ModelLayer.DTOs;
using ModelLayer.Enums;

namespace BusinessLayer.Interfaces
{
    public interface IQuantityMeasurementService
    {
        Task<QuantityMeasurementDTO> CompareQuantities(QuantityInputDTO first, QuantityInputDTO second);
        Task<QuantityMeasurementDTO> ConvertQuantity(QuantityInputDTO source, QuantityInputDTO target);
        Task<QuantityMeasurementDTO> AddQuantities(QuantityInputDTO first, QuantityInputDTO second, string? resultUnit = null);
        Task<QuantityMeasurementDTO> SubtractQuantities(QuantityInputDTO first, QuantityInputDTO second, string? resultUnit = null);
        Task<QuantityMeasurementDTO> DivideQuantities(QuantityInputDTO first, QuantityInputDTO second);
        
        Task<List<QuantityMeasurementDTO>> GetOperationHistory(OperationType operation);
        Task<List<QuantityMeasurementDTO>> GetMeasurementTypeHistory(string measurementType);
        Task<int> GetOperationCount(OperationType operation);
        Task<List<QuantityMeasurementDTO>> GetErrorHistory();
    }
}