namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents measurement in Inches.
    /// Value-based equality implementation.
    /// </summary>
    public sealed class Inches
    {
        private readonly double _value;

        public Inches(double value)
        {
            if (double.IsNaN(value))
                throw new ArgumentException("Invalid Inches value.");

            _value = value;
        }

        public double Value => _value;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not Inches other)
                return false;

            return _value.Equals(other._value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}