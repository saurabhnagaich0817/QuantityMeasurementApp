using System;

namespace ModelLayer.Exceptions
{
    public class QuantityMeasurementException : Exception
    {
        public QuantityMeasurementException() { }

        public QuantityMeasurementException(string message) : base(message) { }

        public QuantityMeasurementException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}