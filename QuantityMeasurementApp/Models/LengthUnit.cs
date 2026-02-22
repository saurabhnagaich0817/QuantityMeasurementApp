// ===================================================
// File: LengthUnit.cs
// Project: QuantityMeasurementApp.Models
// Description: Length unit enumeration with conversion factors - UC4
// Author: Development Team
// Version: 4.0 (UC4 - Extended Unit Support: Yards & Centimeters)
// ===================================================

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Enumeration of supported length measurement units.
    /// UC3: Basic units (FEET, INCH)
    /// UC4: Extended units (YARD, CENTIMETER)
    /// </summary>
    /// <remarks>
    /// Using FEET as base unit (conversion factor 1.0)
    /// All units are defined relative to FEET for consistent conversion.
    /// 
    /// Conversion Factors (to FEET):
    /// - FEET: 1.0 (base unit)
    /// - INCH: 1/12 = 0.0833333
    /// - YARD: 3.0 (1 yard = 3 feet)
    /// - CENTIMETER: 0.0328084 (1 cm = 0.0328084 feet)
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
        /// 1 INCH = 1/12 FEET = 0.0833333 FEET
        /// </summary>
        INCH,

        /// <summary>
        /// Yard unit - larger unit of length.
        /// UC4: New unit added
        /// 1 YARD = 3 FEET
        /// </summary>
        YARD,

        /// <summary>
        /// Centimeter unit - metric unit of length.
        /// UC4: New unit added
        /// 1 CENTIMETER = 0.0328084 FEET
        /// </summary>
        CENTIMETER
    }

    /// <summary>
    /// Extension methods for LengthUnit enum providing conversion factors.
    /// UC3: Basic conversion logic
    /// UC4: Added YARD and CENTIMETER conversions
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
        /// LengthUnit.INCH.ToFeet() returns 0.0833333
        /// LengthUnit.YARD.ToFeet() returns 3.0
        /// LengthUnit.CENTIMETER.ToFeet() returns 0.0328084
        /// </example>
        public static double ToFeet(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => 1.0,                                // 1 foot = 1 foot
                LengthUnit.INCH => 1.0 / 12.0,                         // 1 inch = 1/12 foot
                LengthUnit.YARD => 3.0,                                 // 1 yard = 3 feet
                LengthUnit.CENTIMETER => 0.0328084,                    // 1 cm = 0.0328084 feet
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
        /// LengthUnit.YARD.ToInches() returns 36.0
        /// LengthUnit.CENTIMETER.ToInches() returns 0.393701
        /// </example>
        public static double ToInches(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => 12.0,                               // 1 foot = 12 inches
                LengthUnit.INCH => 1.0,                                 // 1 inch = 1 inch
                LengthUnit.YARD => 36.0,                                // 1 yard = 36 inches
                LengthUnit.CENTIMETER => 0.393701,                     // 1 cm = 0.393701 inches
                _ => throw new ArgumentException($"Invalid unit: {unit}")
            };
        }

        /// <summary>
        /// Converts any length unit to its equivalent value in YARDS.
        /// UC4: New conversion method for yard-based calculations.
        /// </summary>
        /// <param name="unit">The unit to convert from.</param>
        /// <returns>Conversion factor to convert to YARDS.</returns>
        public static double ToYards(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => 1.0 / 3.0,                          // 1 foot = 1/3 yard
                LengthUnit.INCH => 1.0 / 36.0,                         // 1 inch = 1/36 yard
                LengthUnit.YARD => 1.0,                                 // 1 yard = 1 yard
                LengthUnit.CENTIMETER => 0.0109361,                    // 1 cm = 0.0109361 yards
                _ => throw new ArgumentException($"Invalid unit: {unit}")
            };
        }

        /// <summary>
        /// Converts any length unit to its equivalent value in CENTIMETERS.
        /// UC4: New conversion method for metric-based calculations.
        /// </summary>
        /// <param name="unit">The unit to convert from.</param>
        /// <returns>Conversion factor to convert to CENTIMETERS.</returns>
        public static double ToCentimeters(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => 30.48,                              // 1 foot = 30.48 cm
                LengthUnit.INCH => 2.54,                                // 1 inch = 2.54 cm
                LengthUnit.YARD => 91.44,                               // 1 yard = 91.44 cm
                LengthUnit.CENTIMETER => 1.0,                          // 1 cm = 1 cm
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
                LengthUnit.YARD => "Yards",
                LengthUnit.CENTIMETER => "Centimeters",
                _ => unit.ToString()
            };
        }

        /// <summary>
        /// Gets the abbreviation of the unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns>Standard abbreviation for the unit.</returns>
        public static string GetAbbreviation(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => "ft",
                LengthUnit.INCH => "in",
                LengthUnit.YARD => "yd",
                LengthUnit.CENTIMETER => "cm",
                _ => unit.ToString()
            };
        }

        /// <summary>
        /// Gets the conversion factor description.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <returns>Description of conversion to base unit.</returns>
        public static string GetConversionDescription(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.FEET => "1 ft = 1 ft (base unit)",
                LengthUnit.INCH => "1 in = 0.0833333 ft",
                LengthUnit.YARD => "1 yd = 3 ft",
                LengthUnit.CENTIMETER => "1 cm = 0.0328084 ft",
                _ => unit.ToString()
            };
        }
    }
}