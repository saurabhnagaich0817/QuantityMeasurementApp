using ModelLayer.Interfaces;
using ModelLayer.Enums;

namespace BusinessLayer.Services
{
    public class TemperatureUnitConverter : IMeasurable<TemperatureUnit>
    {
        // Base unit: Celsius
        public double ConvertToBase(TemperatureUnit unit, double amount)
        {
            switch (unit)
            {
                case TemperatureUnit.Celsius:
                    return amount;
                case TemperatureUnit.Fahrenheit:
                    return (amount - 32) * 5 / 9;
                case TemperatureUnit.Kelvin:
                    return amount - 273.15;
                default:
                    return amount;
            }
        }

        public double ConvertFromBase(TemperatureUnit unit, double baseValue)
        {
            switch (unit)
            {
                case TemperatureUnit.Celsius:
                    return baseValue;
                case TemperatureUnit.Fahrenheit:
                    return (baseValue * 9 / 5) + 32;
                case TemperatureUnit.Kelvin:
                    return baseValue + 273.15;
                default:
                    return baseValue;
            }
        }

        public double GetConversionFactor(TemperatureUnit unit)
        {
            return 1.0;
        }

        public string GetSymbol(TemperatureUnit unit)
        {
            switch (unit)
            {
                case TemperatureUnit.Celsius: return "°C";
                case TemperatureUnit.Fahrenheit: return "°F";
                case TemperatureUnit.Kelvin: return "K";
                default: return unit.ToString();
            }
        }
    }
}