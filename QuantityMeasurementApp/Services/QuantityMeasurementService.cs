using System;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Service layer responsible for handling
    /// quantity measurement business logic.
    /// </summary>
    public class QuantityMeasurementService
    {
        /// <summary>
        /// Compares two Feet objects safely.
        /// </summary>
        public bool CompareFeetEquality(Feet? firstMeasurement, Feet? secondMeasurement)
        {
            if (firstMeasurement is null || secondMeasurement is null)
                return false;

            return firstMeasurement.Equals(secondMeasurement);
        }

        /// <summary>
        /// Parses string input into Feet object.
        /// Returns null for invalid input.
        /// </summary>
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