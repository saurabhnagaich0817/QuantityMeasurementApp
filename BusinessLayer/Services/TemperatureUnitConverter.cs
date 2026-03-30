using ModelLayer.Interfaces;
using ModelLayer.Enums;

namespace BusinessLayer.Services
{
    /// <summary>
    /// Converts between different temperature units (Celsius, Fahrenheit, Kelvin).
    /// Uses Celsius as the base unit for all conversions.
    /// </summary>
    public class TemperatureUnitConverter : IUnitConverter<TemperatureUnit>
    {
        /// <summary>Converts a temperature to Celsius (base unit).</summary>
        /// <param name="unitType">The unit of the input temperature.</param>
        /// <param name="inputTemp">The temperature value to convert.</param>
        /// <returns>The temperature in Celsius.</returns>
        public double ConvertToBase(TemperatureUnit unitType, double inputTemp)
        {
            return unitType switch
            {
                TemperatureUnit.Celsius => inputTemp,
                TemperatureUnit.Fahrenheit => (inputTemp - 32.0) * 5.0 / 9.0,
                TemperatureUnit.Kelvin => inputTemp - 273.15,
                _ => throw new ArgumentException($"Unsupported temperature unit: {unitType}")
            };
        }

        /// <summary>Converts a temperature from Celsius (base unit) to the specified unit.</summary>
        /// <param name="unitType">The target unit.</param>
        /// <param name="celsiusValue">The temperature in Celsius.</param>
        /// <returns>The temperature in the specified unit.</returns>
        public double ConvertFromBase(TemperatureUnit unitType, double celsiusValue)
        {
            return unitType switch
            {
                TemperatureUnit.Celsius => celsiusValue,
                TemperatureUnit.Fahrenheit => (celsiusValue * 9.0 / 5.0) + 32.0,
                TemperatureUnit.Kelvin => celsiusValue + 273.15,
                _ => throw new ArgumentException($"Unsupported temperature unit: {unitType}")
            };
        }

        /// <summary>Gets the standard symbol for a temperature unit.</summary>
        public string GetSymbol(TemperatureUnit unitType)
        {
            return unitType switch
            {
                TemperatureUnit.Celsius => "°C",
                TemperatureUnit.Fahrenheit => "°F",
                TemperatureUnit.Kelvin => "K",
                _ => unitType.ToString()
            };
        }
    }
}
