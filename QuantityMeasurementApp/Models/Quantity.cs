using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents a quantity with a value and unit of measurement.
    /// This class provides functionality for equality comparison and unit conversion.
    /// </summary>
    public class Quantity
    {
        // Private fields for value and unit
        private readonly double _numericValue;
        private readonly LengthUnit _measurementUnit;

        /// <summary>
        /// Initializes a new instance of the Quantity class.
        /// </summary>
        /// <param name="numericValue">The numeric value of the measurement.</param>
        /// <param name="measurementUnit">The unit of measurement.</param>
        public Quantity(double numericValue, LengthUnit measurementUnit)
        {
            _numericValue = numericValue;
            _measurementUnit = measurementUnit;
        }

        /// <summary>
        /// Gets the measurement value.
        /// </summary>
        public double NumericValue => _numericValue;

        /// <summary>
        /// Gets the measurement unit.
        /// </summary>
        public LengthUnit MeasurementUnit => _measurementUnit;

        /// <summary>
        /// Converts the current quantity to a target unit.
        /// </summary>
        /// <param name="targetUnit">The unit to convert to.</param>
        /// <returns>A new Quantity object with the converted value and target unit.</returns>
        /// <exception cref="ArgumentException">Thrown when target unit is invalid.</exception>
        public Quantity ConvertTo(LengthUnit targetUnit)
        {
            ValidateUnit(targetUnit);

            double valueInBaseUnit = _numericValue * _measurementUnit.GetFeetConversionFactor();
            double convertedNumericValue = valueInBaseUnit / targetUnit.GetFeetConversionFactor();

            return new Quantity(convertedNumericValue, targetUnit);
        }

        /// <summary>
        /// Static method to convert a value from one unit to another.
        /// </summary>
        /// <param name="inputValue">The value to convert.</param>
        /// <param name="sourceUnit">The source unit.</param>
        /// <param name="targetUnit">The target unit.</param>
        /// <returns>The converted value as a double.</returns>
        /// <exception cref="ArgumentException">Thrown when value is invalid or units are invalid.</exception>
        public static double ConvertValue(double inputValue, LengthUnit sourceUnit, LengthUnit targetUnit)
        {
            ValidateNumericValue(inputValue);
            ValidateUnit(sourceUnit);
            ValidateUnit(targetUnit);

            Quantity sourceQuantity = new Quantity(inputValue, sourceUnit);
            Quantity convertedQuantity = sourceQuantity.ConvertTo(targetUnit);

            return convertedQuantity.NumericValue;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current Quantity object.
        /// </summary>
        /// <param name="targetObject">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? targetObject)
        {
            if (ReferenceEquals(this, targetObject))
                return true;

            if (targetObject is null || GetType() != targetObject.GetType())
                return false;

            Quantity otherQuantity = (Quantity)targetObject;

            double thisInFeet = this.ConvertTo(LengthUnit.FEET).NumericValue;
            double otherInFeet = otherQuantity.ConvertTo(LengthUnit.FEET).NumericValue;

            return LengthUnitExtensions.AreApproximatelyEqual(thisInFeet, otherInFeet);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            double valueInFeet = ConvertTo(LengthUnit.FEET).NumericValue;
            return Math.Round(valueInFeet, 6).GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the Quantity object.
        /// </summary>
        /// <returns>A string in the format "{value} {unitSymbol}".</returns>
        public override string ToString()
        {
            return $"{_numericValue} {_measurementUnit.GetSymbol()}";
        }

        /// <summary>
        /// Validates that a unit is valid.
        /// </summary>
        /// <param name="unit">The unit to validate.</param>
        /// <exception cref="ArgumentException">Thrown when unit is invalid.</exception>
        private static void ValidateUnit(LengthUnit unit)
        {
            if (!Enum.IsDefined(typeof(LengthUnit), unit))
            {
                throw new ArgumentException($"Invalid unit: {unit}");
            }
        }

        /// <summary>
        /// Validates that a value is finite.
        /// </summary>
        /// <param name="inputValue">The value to validate.</param>
        /// <exception cref="ArgumentException">Thrown when value is NaN or Infinity.</exception>
        private static void ValidateNumericValue(double inputValue)
        {
            if (double.IsNaN(inputValue) || double.IsInfinity(inputValue))
            {
                throw new ArgumentException(
                    $"Invalid value: {inputValue}. Value must be a finite number."
                );
            }
        }
    }
}