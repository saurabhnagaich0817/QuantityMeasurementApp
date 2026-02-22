// ===================================================
// File: Feet.cs
// Project: QuantityMeasurementApp.Models
// Description: Feet measurement class - UC1 Implementation
// Author: Development Team
// Version: 1.0 (UC1 - Basic Feet Equality)
// ===================================================

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents a measurement in Feet.
    /// Implements Value Object pattern for immutable measurement.
    /// UC1: Basic feet equality comparison.
    /// </summary>
    /// <remarks>
    /// This class is kept for backward compatibility with UC1.
    /// New code should use Quantity class instead.
    /// </remarks>
    public sealed class Feet
    {
        /// <summary>
        /// The numeric value of this feet measurement.
        /// </summary>
        private readonly double _value;

        /// <summary>
        /// Initializes a new instance of the Feet class.
        /// </summary>
        /// <param name="value">The length in feet.</param>
        /// <exception cref="ArgumentException">Thrown when value is NaN.</exception>
        public Feet(double value)
        {
            // Validate input value
            if (double.IsNaN(value))
                throw new ArgumentException("Invalid Feet value. Value cannot be NaN.");

            _value = value;
        }

        /// <summary>
        /// Gets the feet value.
        /// </summary>
        public double Value => _value;

        /// <summary>
        /// Determines whether the specified object is equal to the current Feet.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>true if objects have same value; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            // Check if same reference (reflexive property)
            if (ReferenceEquals(this, obj))
                return true;

            // Check for null and type mismatch
            if (obj is not Feet other)
                return false;

            // Value-based equality check
            return _value.Equals(other._value);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>Hash code based on the feet value.</returns>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the Feet object.
        /// </summary>
        /// <returns>Formatted string with value and unit.</returns>
        public override string ToString()
        {
            return $"{_value} Feet";
        }
    }
}