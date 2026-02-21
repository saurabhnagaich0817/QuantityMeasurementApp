using System;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    
    /// Service layer responsible for handling
    /// quantity measurement business logic.
 
    public class QuantityMeasurementService
    {
       
        /// Compares two Feet objects safely.
      
        public bool CompareFeetEquality(Feet? firstMeasurement, Feet? secondMeasurement)
        {
            if (firstMeasurement is null || secondMeasurement is null)
                return false;

            return firstMeasurement.Equals(secondMeasurement);
        }

       
        /// Parses string input into Feet object.
        /// Returns null for invalid input.
      
        public Feet? ConvertToFeet(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;

            bool isValid = double.TryParse(input, out double value);

            if (!isValid)
                return null;

            return new Feet(value);
        }
    }
}