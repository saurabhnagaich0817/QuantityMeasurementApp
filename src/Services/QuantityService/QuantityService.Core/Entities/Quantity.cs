using System;

namespace QuantityService.Core.Entities
{
    public class Quantity
    {
        public Guid Id { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string UnitType { get; set; } = string.Empty; // Length, Weight, Volume, Temperature
        public double BaseValue { get; set; } // Always store in base unit (meters, grams, liters, celsius)
        public DateTime CreatedAt { get; set; }
    }
}