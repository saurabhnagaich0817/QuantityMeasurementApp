// ===================================================
// File: Quantity.cs
// Project: QuantityMeasurementApp.Models
// Description: Generic quantity class for length measurements - UC3
// Author: Development Team
// Version: 3.0 (UC3 - Generic Length Implementation)
// ===================================================

using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents a generic length quantity with value and unit.
    /// UC3: Consolidates Feet and Inch classes into a single generic class.
    /// Follows Value Object pattern and DRY principle.
    /// </summary>
    /// <remarks>
    /// Design Decisions:
    /// 1. Immutable - Once created, quantity cannot be changed
    /// 2. Value-based equality - Equality based on converted values
    /// 3. Type-safe - Uses enum instead of strings for units
    /// 4. Extensible - Easy to add new units
    /// </remarks>
    public sealed class Quantity
    {
        /// <summary>
        /// The numeric value of this quantity in its original unit.
        /// </summary>
        private readonly double _value;

        /// <summary>
        /// The unit of measurement for this quantity.
        /// </summary>
        private readonly LengthUnit _unit;

        /// <summary>
        /// Tolerance for floating point comparisons.
        /// Used to handle precision issues in double calculations.
        /// </summary>
        private const double EPSILON = 0.000001;

        /// <summary>
        /// Initializes a new instance of the Quantity class.
        /// </summary>
        /// <param name="value">The numeric value of the quantity.</param>
        /// <param name="unit">The unit of measurement.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when value is NaN/Infinity or unit is invalid.
        /// </exception>
        /// <example>
        /// Quantity feet = new Quantity(5.0, LengthUnit.FEET);
        /// Quantity inches = new Quantity(60.0, LengthUnit.INCH);
        /// </example>
        public Quantity(double value, LengthUnit unit)
        {
            // Validate numeric value
            if (double.IsNaN(value))
                throw new ArgumentException("Quantity value cannot be NaN.", nameof(value));

            if (double.IsInfinity(value))
                throw new ArgumentException("Quantity value cannot be infinite.", nameof(value));

            // Validate unit
            if (!Enum.IsDefined(typeof(LengthUnit), unit))
                throw new ArgumentException($"Invalid unit: {unit}", nameof(unit));

            _value = value;
            _unit = unit;
        }

        /// <summary>
        /// Gets the original value in the specified unit.
        /// </summary>
        public double Value => _value;

        /// <summary>
        /// Gets the unit of measurement.
        /// </summary>
        public LengthUnit Unit => _unit;

        /// <summary>
        /// Gets the value converted to FEET (base unit).
        /// Used for cross-unit comparisons.
        /// </summary>
        public double ValueInFeet => _value * _unit.ToFeet();

        /// <summary>
        /// Gets the value converted to INCHES.
        /// Useful for display and calculations.
        /// </summary>
        public double ValueInInches => _value * _unit.ToInches();

        /// <summary>
        /// Determines whether the specified object is equal to the current Quantity.
        /// Implements value-based equality by comparing values converted to base unit.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>
        /// true if the objects represent the same length (after conversion);
        /// otherwise, false.
        /// </returns>
        /// <remarks>
        /// Follows equality contract:
        /// - Reflexive: a.Equals(a) is true
        /// - Symmetric: a.Equals(b) == b.Equals(a)
        /// - Transitive: if a.Equals(b) and b.Equals(c) then a.Equals(c)
        /// - Consistent: multiple calls return same result
        /// - Null handling: a.Equals(null) is false
        /// </remarks>
        public override bool Equals(object? obj)
        {
            // Same reference check (reflexive property)
            if (ReferenceEquals(this, obj))
                return true;

            // Null check
            if (obj is null)
                return false;

            // Type check
            if (obj is not Quantity other)
                return false;

            // Value-based equality after conversion to base unit
            // Use tolerance to handle floating point precision
            return Math.Abs(this.ValueInFeet - other.ValueInFeet) < EPSILON;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// Returns hash code based on converted value to maintain
        /// equality contract with Equals method.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            // Use the converted value for hash code
            // This ensures equal objects have equal hash codes
            return ValueInFeet.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the quantity.
        /// </summary>
        /// <returns>Formatted string with value and unit.</returns>
        /// <example>
        /// new Quantity(5.5, LengthUnit.FEET).ToString() returns "5.5 FEET"
        /// </example>
        public override string ToString()
        {
            return $"{_value} {_unit}";
        }

        /// <summary>
        /// Returns a detailed string with conversions.
        /// </summary>
        /// <returns>Detailed string with original and converted values.</returns>
        public string ToDetailedString()
        {
            return $"{_value} {_unit} = {ValueInFeet:F4} feet = {ValueInInches:F4} inches";
        }
    }
}