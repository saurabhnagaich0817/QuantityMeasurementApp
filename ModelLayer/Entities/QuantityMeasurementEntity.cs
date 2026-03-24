#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer.Entities
{
    [Table("QuantityMeasurements")]
    public class QuantityMeasurementEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string OperationType { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string MeasurementType { get; set; } = string.Empty;

        [Required]
        public double FromValue { get; set; }

        [Required]
        [MaxLength(20)]
        public string FromUnit { get; set; } = string.Empty;

        [Required]
        public double ToValue { get; set; }

        [Required]
        [MaxLength(20)]
        public string ToUnit { get; set; } = string.Empty;

        [Required]
        public double Result { get; set; }

        [MaxLength(20)]
        public string? ResultUnit { get; set; }

        public bool IsError { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid SessionId { get; set; }

        // Navigation property
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
