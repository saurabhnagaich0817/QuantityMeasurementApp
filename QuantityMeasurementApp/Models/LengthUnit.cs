// ===================================================
// File: LengthUnit.cs
// Project: QuantityMeasurementApp.Models
// Description: Length unit enumeration with conversion factors - UC3
// Author: Development Team
// Version: 3.0 (UC3 - Generic Length Implementation)
// ===================================================

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Enumeration of supported length measurement units.
    /// UC3: Provides type-safe unit representation instead of magic strings.
    /// </summary>
    /// <remarks>
    /// Using FEET as base unit (conversion factor 1.0)
    /// All units are defined relative to FEET for consistent conversion.
    /// </remarks>
    public enum LengthUnit
    {
        /// <summary>
        /// Feet unit - base unit for all conversions.
        /// 1 FEET = 1 FEET (base)
        /// </summary>
        FEET,

        /// <summary>
        /// Inch unit - sub-unit of feet.
        /// 1 INCH = 1/12 FEET
        /// </summary>
        INCH
    }

    /// <summary>
    /// Extension methods for LengthUnit enum providing conversion factors.
    /// UC3: Centralizes conversion logic in one place following DRY principle.
    /// </summary>
    public static class LengthUnitExtensions
    {
        /// <summary>
        /// Converts any length unit to its equivalent value in FEET.
        /// </summary>
        /// <param name="unit">The unit to convert from.</param>
        /// <returns>Conversion factor to convert to FEET.</returns>
        /// <exception cref="ArgumentException">Thrown when unit is invalid.</exception>
        /// <example>
        /// LengthUnit.FEET.ToFeet() returns 1.0
        /// LengthUnit.INCH.ToFeet() returns 1.0/12.0
        /// </example>
        public static double ToFeet(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => 1.0,                    // 1 foot = 1 foot
                LengthUnit.INCH => 1.0 / 12.0,             // 1 inch = 1/12 foot
                _ => throw new ArgumentException($"Invalid unit: {unit}")
            };
        }

        /// <summary>
        /// Converts any length unit to its equivalent value in INCHES.
        /// </summary>
        /// <param name="unit">The unit to convert from.</param>
        /// <returns>Conversion factor to convert to INCHES.</returns>
        /// <exception cref="ArgumentException">Thrown when unit is invalid.</exception>
        /// <example>
        /// LengthUnit.FEET.ToInches() returns 12.0
        /// LengthUnit.INCH.ToInches() returns 1.0
        /// </example>
        public static double ToInches(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => 12.0,                   // 1 foot = 12 inches
                LengthUnit.INCH => 1.0,                     // 1 inch = 1 inch
                _ => throw new ArgumentException($"Invalid unit: {unit}")
            };
        }

        /// <summary>
        /// Gets the display name of the unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns>Display name for the unit.</returns>
        public static string GetDisplayName(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => "Feet",
                LengthUnit.INCH => "Inches",
                _ => unit.ToString()
            };
        }
    }
}