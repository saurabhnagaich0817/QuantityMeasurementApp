using ModelLayer.Interfaces;
using ModelLayer.Enums;

namespace BusinessLayer.Services
{
    /// <summary>
    /// Converts between different length units (inches, feet, yards, centimeters).
    /// Uses inches as the base unit for all conversions.
    /// </summary>
    public class LengthUnitConverter : IMeasurable<LengthUnit>
    {
        /// <summary>Conversion factors from each unit to the base unit (inches).</summary>
        private readonly double[] _conversionFactors =
        {
            1.0,           // Inches (base unit)
            12.0,          // Feet to inches
            36.0,          // Yards to inches
            0.393701       // Centimeters to inches
        };

        public double GetConversionFactor(LengthUnit unit)
        {
            return _conversionFactors[(int)unit];
        }

        public double ConvertToBase(LengthUnit unit, double amount)
        {
            return amount * GetConversionFactor(unit);
        }

        public double ConvertFromBase(LengthUnit unit, double baseValue)
        {
            return baseValue / GetConversionFactor(unit);
        }

        /// <summary>Gets the standard symbol/abbreviation for a length unit.</summary>
        public string GetSymbol(LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Inches => "in",
                LengthUnit.Feet => "ft",
                LengthUnit.Yards => "yd",
                LengthUnit.Centimeters => "cm",
                _ => unit.ToString().ToLower()
            };
        }
    }
}
