using System.ComponentModel.DataAnnotations;
using ModelLayer.Enums;

namespace ModelLayer.DTOs
{
    public class QuantityInputDTO
    {
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be positive")]
        public double Value { get; set; }

        [Required]
        public string Unit { get; set; } = string.Empty;

        [Required]
        public string MeasurementType { get; set; } = string.Empty; // Length, Weight, Volume, Temperature
    }
}
