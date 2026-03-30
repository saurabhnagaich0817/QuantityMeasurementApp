using System.ComponentModel.DataAnnotations;
using ModelLayer.Enums;

namespace ModelLayer.DTOs
{
    /// <summary>
    /// Request DTO for quantity input with validation rules for API operations.
    /// </summary>
    public class QuantityInputDTO
    {
        /// <summary>Gets or sets the numeric value of the quantity. Must be a positive number.</summary>
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be a non-negative number")]
        public double Value { get; set; }

        [Required]
        public string Unit { get; set; } = string.Empty;

        [Required]
        public string MeasurementType { get; set; } = string.Empty; // Length, Weight, Volume, Temperature
    }
}
