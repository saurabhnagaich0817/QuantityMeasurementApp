using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Contains business logic for quantity comparison.
    /// </summary>
    public class QuantityMeasurementService
    {
        public bool AreEqual(Feet first, Feet second)
        {
            if (first is null || second is null)
                throw new ArgumentNullException("Feet cannot be null.");

            return first.Equals(second);
        }

        public bool AreEqual(Inches first, Inches second)
        {
            if (first is null || second is null)
                throw new ArgumentNullException("Inches cannot be null.");

            return first.Equals(second);
        }
    }
}