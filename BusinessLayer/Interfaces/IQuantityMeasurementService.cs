using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer.DTOs.Quantity;  // 👈 Add this
using ModelLayer.DTOs;
using ModelLayer.Enums;

namespace BusinessLayer.Interfaces
{
    public interface IQuantityMeasurementService
    {
        // Methods with userId parameter
        Task<QuantityMeasurementDTO> CompareQuantities(QuantityInputDTO first, QuantityInputDTO second, int userId);
        Task<QuantityMeasurementDTO> ConvertQuantity(QuantityInputDTO source, QuantityInputDTO target, int userId);
        Task<QuantityMeasurementDTO> AddQuantities(QuantityInputDTO first, QuantityInputDTO second, string? resultUnit, int userId);
        Task<QuantityMeasurementDTO> SubtractQuantities(QuantityInputDTO first, QuantityInputDTO second, string? resultUnit, int userId);
        Task<QuantityMeasurementDTO> DivideQuantities(QuantityInputDTO first, QuantityInputDTO second, int userId);

        // User-specific history methods
        Task<List<QuantityMeasurementDTO>> GetUserOperationsAsync(int userId);
        Task<List<QuantityMeasurementDTO>> GetOperationHistory(OperationType operation);
        Task<List<QuantityMeasurementDTO>> GetMeasurementTypeHistory(string measurementType);
        Task<int> GetOperationCount(OperationType operation);
        Task<List<QuantityMeasurementDTO>> GetErrorHistory();
        
        // Default methods
        Task<QuantityMeasurementDTO> CompareQuantities(QuantityInputDTO first, QuantityInputDTO second);
        Task<QuantityMeasurementDTO> ConvertQuantity(QuantityInputDTO source, QuantityInputDTO target);
        Task<QuantityMeasurementDTO> AddQuantities(QuantityInputDTO first, QuantityInputDTO second, string? resultUnit);
        Task<QuantityMeasurementDTO> SubtractQuantities(QuantityInputDTO first, QuantityInputDTO second, string? resultUnit);
        Task<QuantityMeasurementDTO> DivideQuantities(QuantityInputDTO first, QuantityInputDTO second);
    }
}