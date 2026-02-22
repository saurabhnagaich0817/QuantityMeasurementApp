// ===================================================
// File: QuantityMeasurementService.cs
// Project: QuantityMeasurementApp.Services
// Description: Business logic for quantity measurement comparisons
// Author: Development Team
// Version: 3.0 (UC1, UC2, UC3)
// ===================================================

using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// Service class handling all measurement comparison logic.
    /// UC3: Enhanced to support generic Quantity comparisons while
    /// maintaining backward compatibility with UC1 and UC2.
    /// </summary>
    /// <remarks>
    /// This service acts as a facade between UI layer and models.
    /// It centralizes comparison logic and provides consistent API.
    /// </remarks>
    public class QuantityMeasurementService
    {
        // ==================== UC1 Methods (Feet) ====================

        /// <summary>
        /// Compares two Feet measurements for equality.
        /// UC1: Legacy method kept for backward compatibility.
        /// </summary>
        /// <param name="first">First Feet measurement.</param>
        /// <param name="second">Second Feet measurement.</param>
        /// <returns>True if both measurements are equal.</returns>
        /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
        public bool AreEqual(Feet first, Feet second)
        {
            // Validate inputs
            if (first is null)
                throw new ArgumentNullException(nameof(first), "Feet cannot be null");

            if (second is null)
                throw new ArgumentNullException(nameof(second), "Feet cannot be null");

            // Delegate equality check to Feet class
            return first.Equals(second);
        }

        // ==================== UC2 Methods (Inch) ====================

        /// <summary>
        /// Compares two Inch measurements for equality.
        /// UC2: Legacy method kept for backward compatibility.
        /// </summary>
        /// <param name="first">First Inch measurement.</param>
        /// <param name="second">Second Inch measurement.</param>
        /// <returns>True if both measurements are equal.</returns>
        /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
        public bool AreEqual(Inch first, Inch second)
        {
            // Validate inputs
            if (first is null)
                throw new ArgumentNullException(nameof(first), "Inch cannot be null");

            if (second is null)
                throw new ArgumentNullException(nameof(second), "Inch cannot be null");

            // Delegate equality check to Inch class
            return first.Equals(second);
        }

        // ==================== UC3 Methods (Quantity) ====================

        /// <summary>
        /// Compares two Quantity measurements for equality.
        /// UC3: Generic method that handles all unit types.
        /// Supports cross-unit comparison (e.g., 1 FEET vs 12 INCHES).
        /// </summary>
        /// <param name="first">First Quantity measurement.</param>
        /// <param name="second">Second Quantity measurement.</param>
        /// <returns>
        /// True if measurements represent same length after conversion;
        /// false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if any parameter is null.</exception>
        /// <example>
        /// service.AreEqual(
        ///     new Quantity(1, LengthUnit.FEET),
        ///     new Quantity(12, LengthUnit.INCH)
        /// ) returns true
        /// </example>
        public bool AreEqual(Quantity first, Quantity second)
        {
            // Validate inputs
            if (first is null)
                throw new ArgumentNullException(nameof(first), "Quantity cannot be null");

            if (second is null)
                throw new ArgumentNullException(nameof(second), "Quantity cannot be null");

            // Delegate equality check to Quantity class
            // Quantity.Equals handles unit conversion internally
            return first.Equals(second);
        }

        /// <summary>
        /// Compares multiple Quantity measurements for equality.
        /// UC3: Extension method to check if all provided quantities are equal.
        /// </summary>
        /// <param name="quantities">Array of quantities to compare.</param>
        /// <returns>True if all quantities are equal; false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if array is null.</exception>
        public bool AreAllEqual(params Quantity[] quantities)
        {
            // Validate input
            if (quantities is null)
                throw new ArgumentNullException(nameof(quantities));

            // If less than 2 items, trivially true
            if (quantities.Length < 2)
                return true;

            // Compare each with first
            for (int i = 1; i < quantities.Length; i++)
            {
                if (!AreEqual(quantities[0], quantities[i]))
                    return false;
            }

            return true;
        }
    }
}