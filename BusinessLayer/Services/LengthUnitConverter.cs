using ModelLayer.Interfaces;
using ModelLayer.Enums;

namespace BusinessLayer.Services
{
    public class LengthUnitConverter : IMeasurable<LengthUnit>
    {
        // Base unit: mm (millimeter)
        private readonly double[] _conversionFactors =
        {
            1.0,        // mm
            10.0,       // cm (1 cm = 10 mm)
            1000.0,     // m (1 m = 1000 mm)
            1000000.0,  // km (1 km = 1,000,000 mm)
            25.4,       // inch (1 inch = 25.4 mm)
            304.8,      // ft (1 ft = 304.8 mm)
            914.4,      // yd (1 yd = 914.4 mm)
            1609344.0   // mile (1 mile = 1,609,344 mm)
        };

        public double GetConversionFactor(LengthUnit unit)
        {
            int index = (int)unit;
            if (index >= 0 && index < _conversionFactors.Length)
                return _conversionFactors[index];
            return 1.0;
        }

        public double ConvertToBase(LengthUnit unit, double amount)
        {
            return amount * GetConversionFactor(unit);
        }

        public double ConvertFromBase(LengthUnit unit, double baseValue)
        {
            double factor = GetConversionFactor(unit);
            if (factor == 0) return baseValue;
            return baseValue / factor;
        }

        public string GetSymbol(LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.mm => "mm",
                LengthUnit.cm => "cm",
                LengthUnit.m => "m",
                LengthUnit.km => "km",
                LengthUnit.inch => "in",
                LengthUnit.ft => "ft",
                LengthUnit.yd => "yd",
                LengthUnit.mile => "mi",
                _ => unit.ToString().ToLower()
            };
        }
    }
}