using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer.DTOs;
using ModelLayer.Enums;

namespace BusinessLayer.Interfaces
{
    /// <summary>
    /// Service interface for quantity measurement operations including conversions, comparisons, and arithmetic.
    /// </summary>
    public interface IQuantityMeasurementService
    {
        /// <summary>Compares two quantities and determines if they are equal after unit normalization.</summary>
        /// <param name="first">The first quantity to compare.</param>
        /// <param name="second">The second quantity to compare.</param>
        /// <returns>A DTO containing the comparison result.</returns>
        Task<QuantityMeasurementDTO> CompareQuantities(QuantityInputDTO first, QuantityInputDTO second);
        Task<QuantityMeasurementDTO> ConvertQuantity(QuantityInputDTO source, QuantityInputDTO target);
        Task<QuantityMeasurementDTO> AddQuantities(QuantityInputDTO first, QuantityInputDTO second, string? resultUnit);
        Task<QuantityMeasurementDTO> SubtractQuantities(QuantityInputDTO first, QuantityInputDTO second, string? resultUnit);
        Task<QuantityMeasurementDTO> DivideQuantities(QuantityInputDTO first, QuantityInputDTO second);

        // User-specific history methods (service extracts user id from HttpContext)
        Task<List<QuantityMeasurementDTO>> GetMyOperationsAsync();
        Task<List<QuantityMeasurementDTO>> GetOperationHistory(OperationType operation);
        Task<List<QuantityMeasurementDTO>> GetMeasurementTypeHistory(string measurementType);
        Task<int> GetOperationCount(OperationType operation);
        Task<List<QuantityMeasurementDTO>> GetErrorHistory();
        
        // (No duplicate default overloads)
    }
}
