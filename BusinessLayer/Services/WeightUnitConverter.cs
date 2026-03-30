using ModelLayer.Interfaces;
using ModelLayer.Enums;

namespace BusinessLayer.Services
{
    /// <summary>
    /// Converts between different weight units (grams, kilograms, pounds).
    /// Uses kilograms as the base unit for all conversions.
    /// </summary>
    public class WeightUnitConverter : IMeasurable<WeightUnit>
    {
        /// <summary>Conversion factors from each unit to the base unit (kilograms).</summary>
        private static readonly double[] ConversionFactors =
        {
            0.001,    // Grams to kilograms
            1.0,      // Kilograms (base unit)
            0.453592  // Pounds to kilograms
        };

        public double GetConversionFactor(WeightUnit unit)
        {
            return ConversionFactors[(int)unit];
        }

        public double ConvertToBase(WeightUnit unit, double amount)
        {
            return amount * GetConversionFactor(unit);
        }

        public double ConvertFromBase(WeightUnit unit, double baseValue)
        {
            return baseValue / GetConversionFactor(unit);
        }

        /// <summary>Gets the standard symbol/abbreviation for a weight unit.</summary>
        public string GetSymbol(WeightUnit unit)
        {
            return unit switch
            {
                WeightUnit.Grams => "g",
                WeightUnit.Kilograms => "kg",
                WeightUnit.Pound => "lb",
                _ => unit.ToString().ToLower()
            };
        }
    }
}
