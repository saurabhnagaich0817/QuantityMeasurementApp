using System;
using QuantityService.Core.DTOs;

namespace QuantityService.Core.Helpers
{
    public static class UnitConverter
    {
        // Convert any quantity to base unit (meters, grams, liters, celsius)
        public static double ToBaseUnit(double value, string unit, string unitType)
        {
            return unitType switch
            {
                "Length" => ConvertLengthToBase(value, unit),
                "Weight" => ConvertWeightToBase(value, unit),
                "Volume" => ConvertVolumeToBase(value, unit),
                "Temperature" => ConvertTemperatureToBase(value, unit),
                _ => throw new ArgumentException($"Unknown unit type: {unitType}")
            };
        }

        // Convert from base unit to target unit
        public static double FromBaseUnit(double baseValue, string targetUnit, string unitType)
        {
            return unitType switch
            {
                "Length" => ConvertLengthFromBase(baseValue, targetUnit),
                "Weight" => ConvertWeightFromBase(baseValue, targetUnit),
                "Volume" => ConvertVolumeFromBase(baseValue, targetUnit),
                "Temperature" => ConvertTemperatureFromBase(baseValue, targetUnit),
                _ => throw new ArgumentException($"Unknown unit type: {unitType}")
            };
        }

        // Detect unit type from unit string
        public static string DetectUnitType(string unit)
        {
            return unit.ToLower() switch
            {
                "m" or "meter" or "meters" or "km" or "kilometer" or "kilometers" or "cm" or "centimeter" or "centimeters" or "mm" or "millimeter" or "millimeters" or "ft" or "feet" or "in" or "inch" or "inches" => "Length",
                
                "g" or "gram" or "grams" or "kg" or "kilogram" or "kilograms" or "mg" or "milligram" or "milligrams" or "lb" or "pound" or "pounds" or "oz" or "ounce" or "ounces" => "Weight",
                
                "l" or "liter" or "liters" or "ml" or "milliliter" or "milliliters" or "gal" or "gallon" or "gallons" => "Volume",
                
                "c" or "celsius" or "f" or "fahrenheit" or "k" or "kelvin" => "Temperature",
                
                _ => throw new ArgumentException($"Unknown unit: {unit}")
            };
        }

        #region Length Conversions (Base: Meters)
        private static double ConvertLengthToBase(double value, string unit)
        {
            return unit.ToLower() switch
            {
                "m" or "meter" or "meters" => value,
                "km" or "kilometer" or "kilometers" => value * 1000,
                "cm" or "centimeter" or "centimeters" => value / 100,
                "mm" or "millimeter" or "millimeters" => value / 1000,
                "ft" or "feet" => value * 0.3048,
                "in" or "inch" or "inches" => value * 0.0254,
                _ => throw new ArgumentException($"Unknown length unit: {unit}")
            };
        }

        private static double ConvertLengthFromBase(double baseValue, string targetUnit)
        {
            return targetUnit.ToLower() switch
            {
                "m" or "meter" or "meters" => baseValue,
                "km" or "kilometer" or "kilometers" => baseValue / 1000,
                "cm" or "centimeter" or "centimeters" => baseValue * 100,
                "mm" or "millimeter" or "millimeters" => baseValue * 1000,
                "ft" or "feet" => baseValue / 0.3048,
                "in" or "inch" or "inches" => baseValue / 0.0254,
                _ => throw new ArgumentException($"Unknown length unit: {targetUnit}")
            };
        }
        #endregion

        #region Weight Conversions (Base: Grams)
        private static double ConvertWeightToBase(double value, string unit)
        {
            return unit.ToLower() switch
            {
                "g" or "gram" or "grams" => value,
                "kg" or "kilogram" or "kilograms" => value * 1000,
                "mg" or "milligram" or "milligrams" => value / 1000,
                "lb" or "pound" or "pounds" => value * 453.592,
                "oz" or "ounce" or "ounces" => value * 28.3495,
                _ => throw new ArgumentException($"Unknown weight unit: {unit}")
            };
        }

        private static double ConvertWeightFromBase(double baseValue, string targetUnit)
        {
            return targetUnit.ToLower() switch
            {
                "g" or "gram" or "grams" => baseValue,
                "kg" or "kilogram" or "kilograms" => baseValue / 1000,
                "mg" or "milligram" or "milligrams" => baseValue * 1000,
                "lb" or "pound" or "pounds" => baseValue / 453.592,
                "oz" or "ounce" or "ounces" => baseValue / 28.3495,
                _ => throw new ArgumentException($"Unknown weight unit: {targetUnit}")
            };
        }
        #endregion

        #region Volume Conversions (Base: Liters)
        private static double ConvertVolumeToBase(double value, string unit)
        {
            return unit.ToLower() switch
            {
                "l" or "liter" or "liters" => value,
                "ml" or "milliliter" or "milliliters" => value / 1000,
                "gal" or "gallon" or "gallons" => value * 3.78541,
                _ => throw new ArgumentException($"Unknown volume unit: {unit}")
            };
        }

        private static double ConvertVolumeFromBase(double baseValue, string targetUnit)
        {
            return targetUnit.ToLower() switch
            {
                "l" or "liter" or "liters" => baseValue,
                "ml" or "milliliter" or "milliliters" => baseValue * 1000,
                "gal" or "gallon" or "gallons" => baseValue / 3.78541,
                _ => throw new ArgumentException($"Unknown volume unit: {targetUnit}")
            };
        }
        #endregion

        #region Temperature Conversions (Base: Celsius)
        private static double ConvertTemperatureToBase(double value, string unit)
        {
            return unit.ToLower() switch
            {
                "c" or "celsius" => value,
                "f" or "fahrenheit" => (value - 32) * 5 / 9,
                "k" or "kelvin" => value - 273.15,
                _ => throw new ArgumentException($"Unknown temperature unit: {unit}")
            };
        }

        private static double ConvertTemperatureFromBase(double baseValue, string targetUnit)
        {
            return targetUnit.ToLower() switch
            {
                "c" or "celsius" => baseValue,
                "f" or "fahrenheit" => (baseValue * 9 / 5) + 32,
                "k" or "kelvin" => baseValue + 273.15,
                _ => throw new ArgumentException($"Unknown temperature unit: {targetUnit}")
            };
        }
        #endregion
    }
}