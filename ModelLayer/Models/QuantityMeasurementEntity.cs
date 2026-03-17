using System;

namespace ModelLayer.Models
{
    public class QuantityMeasurementEntity
    {
        public int Id { get; set; }
        public string? OperationType { get; set; }      
        public string? MeasurementType { get; set; }     
        public double FromValue { get; set; }
        public string? FromUnit { get; set; }            
        public double ToValue { get; set; }
        public string? ToUnit { get; set; }              
        public double Result { get; set; }
        public string? ResultUnit { get; set; }          
        public DateTime CreatedAt { get; set; }
        public string? UserId { get; set; }              
        public Guid SessionId { get; set; }
    }
}