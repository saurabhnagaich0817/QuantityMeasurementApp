namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents measurement in Feet.
    /// Implements Value Object pattern.
    /// </summary>
    public sealed class Feet
    {
        private readonly double _value;

        public Feet(double value)
        {
            if (double.IsNaN(value))
                throw new ArgumentException("Invalid Feet value.");

            _value = value;
        }

        public double Value => _value;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not Feet other)
                return false;

            return _value.Equals(other._value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}