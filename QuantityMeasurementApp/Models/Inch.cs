// ===================================================
// File: Inch.cs
// Project: QuantityMeasurementApp.Models
// Description: Inch measurement class - UC2 Implementation
// Author: Development Team
// Version: 2.0 (UC2 - Basic Inch Equality)
// ===================================================

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents a measurement in Inches.
    /// Implements Value Object pattern for immutable measurement.
    /// UC2: Basic inch equality comparison.
    /// </summary>
    /// <remarks>
    /// This class is kept for backward compatibility with UC2.
    /// New code should use Quantity class instead.
    /// </remarks>
    public sealed class Inch
    {
        /// <summary>
        /// The numeric value of this inch measurement.
        /// </summary>
        private readonly double _value;

        /// <summary>
        /// Initializes a new instance of the Inch class.
        /// </summary>
        /// <param name="value">The length in inches.</param>
        /// <exception cref="ArgumentException">Thrown when value is NaN.</exception>
        public Inch(double value)
        {
            // Validate input value
            if (double.IsNaN(value))
                throw new ArgumentException("Invalid Inch value. Value cannot be NaN.");

            _value = value;
        }

        /// <summary>
        /// Gets the inch value.
        /// </summary>
        public double Value => _value;

        /// <summary>
        /// Determines whether the specified object is equal to the current Inch.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>true if objects have same value; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            // Check if same reference (reflexive property)
            if (ReferenceEquals(this, obj))
                return true;

            // Check for null and type mismatch
            if (obj is not Inch other)
                return false;

            // Value-based equality check
            return _value.Equals(other._value);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>Hash code based on the inch value.</returns>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the Inch object.
        /// </summary>
        /// <returns>Formatted string with value and unit.</returns>
        public override string ToString()
        {
            return $"{_value} Inches";
        }
    }
}