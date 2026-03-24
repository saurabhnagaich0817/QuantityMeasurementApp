#nullable enable
using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.DTOs.Quantity
{
    public class QuantityInputDto
    {
        [Required]
        public double Value { get; set; }

        [Required]
        public string Unit { get; set; } = string.Empty;

        [Required]
        public string MeasurementType { get; set; } = string.Empty;
    }

    public class QuantityMeasurementDto
    {
        public int Id { get; set; }
        public string Operation { get; set; } = string.Empty;
        public string MeasurementType { get; set; } = string.Empty;
        public double FromValue { get; set; }
        public string FromUnit { get; set; } = string.Empty;
        public double ToValue { get; set; }
        public string ToUnit { get; set; } = string.Empty;
        public double Result { get; set; }
        public string? ResultUnit { get; set; }
        public bool IsError { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // ... rest remains same
}