#nullable enable
using System;
using ModelLayer.Enums;
using ModelLayer.Entities;

namespace ModelLayer.DTOs
{
    /// <summary>
    /// Data Transfer Object representing the result of a quantity measurement operation.
    /// Used for API responses and client communication.
    /// </summary>
    public class QuantityMeasurementDTO
    {
        /// <summary>Gets or sets the unique identifier for this measurement record.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets the type of operation performed (Compare, Convert, Add, etc.).</summary>
        public OperationType Operation { get; set; }

        /// <summary>Gets or sets the measurement type (Length, Weight, Volume, Temperature).</summary>
        public string MeasurementType { get; set; } = string.Empty;
        
        public double FromValue { get; set; }
        public string FromUnit { get; set; } = string.Empty;
        
        public double ToValue { get; set; }
        public string ToUnit { get; set; } = string.Empty;
        
        public double Result { get; set; }
        public string ResultUnit { get; set; } = string.Empty;
        
        public bool IsError { get; set; }
        public string? ErrorMessage { get; set; }
        
        public DateTime CreatedAt { get; set; }

        // Static factory methods for conversion
        public static QuantityMeasurementDTO FromEntity(QuantityMeasurementEntity entity)
        {
            return new QuantityMeasurementDTO
            {
                Id = entity.Id,
                Operation = Enum.Parse<OperationType>(entity.OperationType ?? "Compare"),
                MeasurementType = entity.MeasurementType ?? "",
                FromValue = entity.FromValue,
                FromUnit = entity.FromUnit ?? "",
                ToValue = entity.ToValue,
                ToUnit = entity.ToUnit ?? "",
                Result = entity.Result,
                ResultUnit = entity.ResultUnit ?? "",
                IsError = false,
                CreatedAt = entity.CreatedAt
            };
        }

        public QuantityMeasurementEntity ToEntity()
        {
            return new QuantityMeasurementEntity
            {
                Id = this.Id,
                OperationType = this.Operation.ToString(),
                MeasurementType = this.MeasurementType,
                FromValue = this.FromValue,
                FromUnit = this.FromUnit,
                ToValue = this.ToValue,
                ToUnit = this.ToUnit,
                Result = this.Result,
                ResultUnit = this.ResultUnit,
                CreatedAt = this.CreatedAt == DateTime.MinValue ? DateTime.Now : this.CreatedAt,
                SessionId = Guid.NewGuid()
            };
        }
    }
}
