using ModelLayer.Interfaces;
using ModelLayer.Enums;

namespace BusinessLayer.Services
{
    public class VolumeUnitConverter : IMeasurable<VolumeUnit>
    {
        // Base unit: ml (milliliter)
        private readonly double[] _conversionFactors =
        {
            1.0,        // ml
            1000.0,     // l (1 L = 1000 ml)
            3785.41,    // gallon (1 US gallon = 3785.41 ml)
            946.353,    // quart (1 US quart = 946.353 ml)
            473.176,    // pint (1 US pint = 473.176 ml)
            236.588,    // cup (1 US cup = 236.588 ml)
            14.7868,    // tbsp (1 tbsp = 14.7868 ml)
            4.92892     // tsp (1 tsp = 4.92892 ml)
        };

        public double GetConversionFactor(VolumeUnit unit)
        {
            int index = (int)unit;
            if (index >= 0 && index < _conversionFactors.Length)
                return _conversionFactors[index];
            return 1.0;
        }

        public double ConvertToBase(VolumeUnit unit, double amount)
        {
            return amount * GetConversionFactor(unit);
        }

        public double ConvertFromBase(VolumeUnit unit, double baseValue)
        {
            double factor = GetConversionFactor(unit);
            if (factor == 0) return baseValue;
            return baseValue / factor;
        }

        public string GetSymbol(VolumeUnit unit)
        {
            switch (unit)
            {
                case VolumeUnit.ml: return "ml";
                case VolumeUnit.l: return "L";
                case VolumeUnit.gallon: return "gal";
                case VolumeUnit.quart: return "qt";
                case VolumeUnit.pint: return "pt";
                case VolumeUnit.cup: return "cup";
                case VolumeUnit.tbsp: return "tbsp";
                case VolumeUnit.tsp: return "tsp";
                default: return unit.ToString().ToLower();
            }
        }
    }
}