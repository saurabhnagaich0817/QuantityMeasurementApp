using ModelLayer.Interfaces;
using ModelLayer.Enums;

namespace BusinessLayer.Services
{
    public class WeightUnitConverter : IMeasurable<WeightUnit>
    {
        // Base unit: mg (milligram)
        private readonly double[] _conversionFactors =
        {
            1.0,        // mg
            1000.0,     // g (1 g = 1000 mg)
            1000000.0,  // kg (1 kg = 1,000,000 mg)
            1000000000.0, // tonne (1 tonne = 1,000,000,000 mg)
            28349.5,    // oz (1 oz = 28,349.5 mg)
            453592.0,   // lb (1 lb = 453,592 mg)
            6350290.0   // stone (1 stone = 6,350,290 mg)
        };

        public double GetConversionFactor(WeightUnit unit)
        {
            int index = (int)unit;
            if (index >= 0 && index < _conversionFactors.Length)
                return _conversionFactors[index];
            return 1.0;
        }

        public double ConvertToBase(WeightUnit unit, double amount)
        {
            return amount * GetConversionFactor(unit);
        }

        public double ConvertFromBase(WeightUnit unit, double baseValue)
        {
            double factor = GetConversionFactor(unit);
            if (factor == 0) return baseValue;
            return baseValue / factor;
        }

        public string GetSymbol(WeightUnit unit)
        {
            switch (unit)
            {
                case WeightUnit.mg: return "mg";
                case WeightUnit.g: return "g";
                case WeightUnit.kg: return "kg";
                case WeightUnit.tonne: return "t";
                case WeightUnit.oz: return "oz";
                case WeightUnit.lb: return "lb";
                case WeightUnit.stone: return "st";
                default: return unit.ToString().ToLower();
            }
        }
    }
}