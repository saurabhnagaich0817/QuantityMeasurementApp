using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Service class for quantity measurement operations
    /// Updated to use the generic Quantity class instead of separate Feet and Inch classes
    /// </summary>
    public class QuantityMeasurementService
    {
        // Compares two quantity measurements for equality
        // Parameter: firstQuantity - First quantity measurement (can be null)
        // Parameter: secondQuantity - Second quantity measurement (can be null)
        // Returns: True if both measurements are equal and non-null; otherwise, false
        public bool CompareQuantities(Quantity? firstQuantity, Quantity? secondQuantity)
        {
            // Handle null cases - if either measurement is null, they cannot be equal
            if (firstQuantity is null || secondQuantity is null)
                return false;

            // Delegate the equality check to the Quantity class's Equals method
            return firstQuantity.Equals(secondQuantity);
        }

        // Creates a Quantity object from a string input and unit
        // Parameter: userInput - String input to parse (can be null or whitespace)
        // Parameter: targetUnit - The unit of measurement
        // Returns: Quantity object if parsing successful; otherwise, null
        public Quantity? CreateQuantityFromInput(string? userInput, LengthUnit targetUnit)
        {
            // Check for null or whitespace input
            if (string.IsNullOrWhiteSpace(userInput))
                return null;

            // Try to parse the string as a double
            if (double.TryParse(userInput, out double parsedValue))
            {
                // Successfully parsed, create and return a new Quantity object
                return new Quantity(parsedValue, targetUnit);
            }

            // Parsing failed (non-numeric input), return null
            return null;
        }

        // Static method for quantity equality check - reduces dependency on main method
        // Parameter: firstValue - First value
        // Parameter: firstUnit - Unit of first value
        // Parameter: secondValue - Second value
        // Parameter: secondUnit - Unit of second value
        // Returns: True if both quantities are equal
        public static bool CheckQuantityEquality(
            double firstValue,
            LengthUnit firstUnit,
            double secondValue,
            LengthUnit secondUnit
        )
        {
            Quantity quantityOne = new Quantity(firstValue, firstUnit);
            Quantity quantityTwo = new Quantity(secondValue, secondUnit);
            return quantityOne.Equals(quantityTwo);
        }

        // For backward compatibility - uses the new Quantity class
        public bool CompareFeetMeasurements(Feet? firstFeet, Feet? secondFeet)
        {
            if (firstFeet is null || secondFeet is null)
                return false;

            Quantity q1 = new Quantity(firstFeet.Measurement, LengthUnit.FEET);
            Quantity q2 = new Quantity(secondFeet.Measurement, LengthUnit.FEET);
            return q1.Equals(q2);
        }

        // For backward compatibility - uses the new Quantity class
        public Feet? CreateFeetFromString(string? userInput)
        {
            Quantity? quantity = CreateQuantityFromInput(userInput, LengthUnit.FEET);
            return quantity != null ? new Feet(quantity.NumericValue) : null;
        }

        // For backward compatibility - uses the new Quantity class
        public bool CompareInchMeasurements(Inch? firstInch, Inch? secondInch)
        {
            if (firstInch is null || secondInch is null)
                return false;

            Quantity q1 = new Quantity(firstInch.Measurement, LengthUnit.INCH);
            Quantity q2 = new Quantity(secondInch.Measurement, LengthUnit.INCH);
            return q1.Equals(q2);
        }

        // For backward compatibility - uses the new Quantity class
        public Inch? CreateInchFromString(string? userInput)
        {
            Quantity? quantity = CreateQuantityFromInput(userInput, LengthUnit.INCH);
            return quantity != null ? new Inch(quantity.NumericValue) : null;
        }
    }
}