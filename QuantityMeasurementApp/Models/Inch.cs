using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents an inch measurement with value-based equality
    /// This class is immutable - once created, its value cannot be changed
    /// </summary>
    public class Inch
    {
        // Private field to store the measurement value
        // Made readonly to ensure immutability
        private readonly double _measurementValue;

        // Constructor to initialize a new Inch object with a given value
        // Parameter: measurement - The measurement value in inches
        public Inch(double measurement)
        {
            _measurementValue = measurement;
        }

        // Public property to access the measurement value
        // Since the field is readonly, this provides read-only access
        public double Measurement => _measurementValue;

        // Determines whether the specified object is equal to the current Inch object
        // Implements value-based equality with proper null checking and type safety
        // Parameter: target - The object to compare with the current object
        // Returns: true if the specified object is equal to the current object; otherwise, false
        public override bool Equals(object? target)
        {
            // Check if the object is the same reference (reflexive property)
            // If both references point to the same object, they are equal
            if (ReferenceEquals(this, target))
                return true;

            // Check if the object is null
            // By contract, no object should equal null
            if (target is null)
                return false;

            // Check if the object is of different type (type safety)
            // We only want to compare Inch objects with other Inch objects
            if (GetType() != target.GetType())
                return false;

            // Safe cast after type check - this will never fail due to above check
            Inch otherInch = (Inch)target;

            // Compare double values using exact equality
            // This ensures that 1.000001 and 1.000002 are considered different
            return _measurementValue == otherInch._measurementValue;
        }

        // Serves as the default hash function
        // Returns a hash code for the current object
        // Important: Equal objects must have equal hash codes
        public override int GetHashCode()
        {
            // Use the built-in hash code of the double value
            // This ensures that equal values produce the same hash code
            return _measurementValue.GetHashCode();
        }

        // Returns a string representation of the Inch object
        // Format: "{value} in" (e.g., "1.5 in")
        public override string ToString()
        {
            return $"{_measurementValue} in";
        }
    }
}