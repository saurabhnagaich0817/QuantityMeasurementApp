using ModelLayer.Interfaces;
using ModelLayer.Enums;

namespace BusinessLayer.Services
{
    /// <summary>
    /// Converts between different volume units (liters, milliliters, gallons).
    /// Uses liters as the base unit for all conversions.
    /// </summary>
    public class VolumeUnitConverter : IMeasurable<VolumeUnit>
    {
        /// <summary>Conversion factors from each unit to the base unit (liters).</summary>
        private static readonly double[] ConversionFactors =
        {
            1.0,      // Liters (base unit)
            0.001,    // Milliliters to liters
            3.78541   // Gallons to liters
        };

        public double GetConversionFactor(VolumeUnit unit)
        {
            return ConversionFactors[(int)unit];
        }

        public double ConvertToBase(VolumeUnit unit, double amount)
        {
            return amount * GetConversionFactor(unit);
        }

        public double ConvertFromBase(VolumeUnit unit, double baseValue)
        {
            return baseValue / GetConversionFactor(unit);
        }

        /// <summary>Gets the standard symbol/abbreviation for a volume unit.</summary>
        public string GetSymbol(VolumeUnit unit)
        {
            return unit switch
            {
                VolumeUnit.Litre => "L",
                VolumeUnit.MilliLiter => "mL",
                VolumeUnit.Gallon => "gal",
                _ => unit.ToString().ToLower()
            };
        }
    }
}
