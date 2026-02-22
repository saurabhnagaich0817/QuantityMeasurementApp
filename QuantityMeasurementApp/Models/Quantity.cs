// ===================================================
// File: Quantity.cs
// Project: QuantityMeasurementApp.Models
// Description: Generic quantity class - Add UC4 helper properties
// Author: Development Team
// Version: 4.0 (UC4 - Added Yard and Centimeter properties)
// ===================================================

using System;

namespace QuantityMeasurementApp.Models
{
    public sealed class Quantity
    {
        private readonly double _value;
        private readonly LengthUnit _unit;
        private const double EPSILON = 0.000001;

        public Quantity(double value, LengthUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid quantity value");

            if (!Enum.IsDefined(typeof(LengthUnit), unit))
                throw new ArgumentException($"Invalid unit: {unit}");

            _value = value;
            _unit = unit;
        }

        public double Value => _value;
        public LengthUnit Unit => _unit;
        
        // UC3 Properties
        public double ValueInFeet => _value * _unit.ToFeet();
        public double ValueInInches => _value * _unit.ToInches();
        
        // UC4 New Properties
        public double ValueInYards => _value * _unit.ToYards();
        public double ValueInCentimeters => _value * _unit.ToCentimeters();

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not Quantity other)
                return false;

            return Math.Abs(this.ValueInFeet - other.ValueInFeet) < EPSILON;
        }

        public override int GetHashCode()
        {
            return ValueInFeet.GetHashCode();
        }

        public override string ToString()
        {
            string unitAbbr = _unit.GetAbbreviation();
            return $"{_value} {unitAbbr} ({_unit})";
        }

        public string ToDetailedString()
        {
            return $"{_value} {_unit.GetAbbreviation()} = {ValueInFeet:F4} ft = {ValueInInches:F4} in = {ValueInYards:F4} yd = {ValueInCentimeters:F4} cm";
        }
    }
}