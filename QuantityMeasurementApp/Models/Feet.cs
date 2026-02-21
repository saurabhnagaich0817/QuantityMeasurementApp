using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents a measurement in Feet.
    /// This is an immutable value object.
    /// </summary>
    public sealed class Feet
    {
        private readonly double _value;

        /// <summary>
        /// Initializes a new Feet measurement.
        /// </summary>
        /// <param name="value">Measurement value in feet.</param>
        public Feet(double value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the measurement value.
        /// </summary>
        public double Value => _value;

        /// <summary>
        /// Determines value-based equality.
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is null || obj.GetType() != typeof(Feet))
                return false;

            var other = (Feet)obj;

            // Exact comparison as per UC1 requirement
            return _value == other._value;
        }

        /// <summary>
        /// Returns hash code based on value.
        /// Equal objects must have equal hash codes.
        /// </summary>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        /// <summary>
        /// Returns formatted string representation.
        /// </summary>
        public override string ToString()
        {
            return $"{_value} ft";
        }
    }
}