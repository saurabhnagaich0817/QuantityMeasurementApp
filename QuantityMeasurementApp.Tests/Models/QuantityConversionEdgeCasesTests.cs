using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Test class for Quantity conversion edge cases and special scenarios.
    /// Tests boundary conditions, extreme values, and error handling.
    /// </summary>
    [TestClass]
    public class QuantityConversionEdgeCasesTests
    {
        private const double NumericTolerance = 0.000001;

        #region Extreme Value Tests

        /// <summary>
        /// Tests conversion of extremely large values.
        /// Verifies that conversion handles double.MaxValue gracefully.
        /// </summary>
        [TestMethod]
        public void Convert_ExtremelyLargeValue_HandlesCorrectly()
        {
            double largeInputValue = double.MaxValue / 1000; // Avoid overflow

            double conversionResult = Quantity.ConvertValue(largeInputValue, LengthUnit.FEET, LengthUnit.INCH);
            double expectedResult = largeInputValue * 12.0;

            // Check if result is finite (not overflow)
            Assert.IsFalse(double.IsInfinity(conversionResult), "Result should not be infinite");
            Assert.IsFalse(double.IsNaN(conversionResult), "Result should not be NaN");
        }

        /// <summary>
        /// Tests conversion of extremely small values.
        /// Verifies that conversion handles double.Epsilon gracefully.
        /// </summary>
        [TestMethod]
        public void Convert_ExtremelySmallValue_HandlesCorrectly()
        {
            double smallInputValue = double.Epsilon;

            double conversionResult = Quantity.ConvertValue(smallInputValue, LengthUnit.FEET, LengthUnit.INCH);

            Assert.IsFalse(double.IsInfinity(conversionResult), "Result should not be infinite");
            Assert.IsFalse(double.IsNaN(conversionResult), "Result should not be NaN");
        }

        #endregion

        #region Unit Boundary Tests

        /// <summary>
        /// Tests conversion between all possible unit combinations.
        /// Verifies that no combination throws unexpected exceptions.
        /// </summary>
        [TestMethod]
        public void Convert_AllUnitCombinations_NoExceptions()
        {
            LengthUnit[] availableUnits =
            {
                LengthUnit.FEET,
                LengthUnit.INCH,
                LengthUnit.YARD,
                LengthUnit.CENTIMETER,
            };
            double inputValue = 1.0;

            foreach (LengthUnit sourceUnit in availableUnits)
            {
                foreach (LengthUnit targetUnit in availableUnits)
                {
                    try
                    {
                        double conversionResult = Quantity.ConvertValue(inputValue, sourceUnit, targetUnit);

                        // Basic sanity check
                        Assert.IsFalse(
                            double.IsNaN(conversionResult),
                            $"Conversion from {sourceUnit} to {targetUnit} produced NaN"
                        );
                        Assert.IsFalse(
                            double.IsInfinity(conversionResult),
                            $"Conversion from {sourceUnit} to {targetUnit} produced infinity"
                        );
                    }
                    catch (Exception ex)
                    {
                        Assert.Fail(
                            $"Conversion from {sourceUnit} to {targetUnit} threw exception: {ex.Message}"
                        );
                    }
                }
            }
        }

        #endregion

        #region Consistency Tests

        /// <summary>
        /// Tests that conversion is consistent across different instances.
        /// Verifies that multiple conversions of same value yield same result.
        /// </summary>
        [TestMethod]
        public void Convert_ConsistentResults_MultipleCalls()
        {
            double inputValue = 2.5;

            double firstResult = Quantity.ConvertValue(inputValue, LengthUnit.YARD, LengthUnit.INCH);
            double secondResult = Quantity.ConvertValue(inputValue, LengthUnit.YARD, LengthUnit.INCH);
            double thirdResult = Quantity.ConvertValue(inputValue, LengthUnit.YARD, LengthUnit.INCH);

            Assert.AreEqual(
                firstResult,
                secondResult,
                NumericTolerance,
                "First and second conversions should match"
            );
            Assert.AreEqual(
                secondResult,
                thirdResult,
                NumericTolerance,
                "Second and third conversions should match"
            );
        }

        /// <summary>
        /// Tests that conversion through base unit matches direct conversion.
        /// Verifies that multi-step conversion is equivalent to direct conversion.
        /// </summary>
        [TestMethod]
        public void Convert_ThroughBaseUnit_MatchesDirect()
        {
            double inputValue = 3.0;

            // Direct: Yards to Inches
            double directConversion = Quantity.ConvertValue(inputValue, LengthUnit.YARD, LengthUnit.INCH);

            // Through base (feet)
            double toFeetConversion = Quantity.ConvertValue(inputValue, LengthUnit.YARD, LengthUnit.FEET);
            double throughBaseConversion = Quantity.ConvertValue(toFeetConversion, LengthUnit.FEET, LengthUnit.INCH);

            Assert.AreEqual(
                directConversion,
                throughBaseConversion,
                NumericTolerance,
                "Direct conversion should match conversion through base unit"
            );
        }

        #endregion

        #region Validation Tests

        /// <summary>
        /// Tests that undefined enum values are properly rejected.
        /// Verifies that casting invalid integers to enum is caught.
        /// </summary>
        [TestMethod]
        public void Convert_UndefinedEnumValue_ThrowsArgumentException()
        {
            // Test with various invalid enum values
            int[] invalidEnumValues = { -1, 4, 5, 10, 100 };

            foreach (int invalidValue in invalidEnumValues)
            {
                LengthUnit invalidUnit = (LengthUnit)invalidValue;

                // FIXED: Without using Assert.ThrowsException
                try
                {
                    Quantity.ConvertValue(1.0, invalidUnit, LengthUnit.FEET);
                    Assert.Fail($"Expected ArgumentException for source unit {invalidValue} but no exception was thrown");
                }
                catch (ArgumentException)
                {
                    // Expected exception - test passes for this case
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Expected ArgumentException for source unit {invalidValue} but got {ex.GetType().Name}");
                }

                try
                {
                    Quantity.ConvertValue(1.0, LengthUnit.FEET, invalidUnit);
                    Assert.Fail($"Expected ArgumentException for target unit {invalidValue} but no exception was thrown");
                }
                catch (ArgumentException)
                {
                    // Expected exception - test passes for this case
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Expected ArgumentException for target unit {invalidValue} but got {ex.GetType().Name}");
                }
            }
        }

        #endregion

        #region Mathematical Property Tests

        /// <summary>
        /// Tests that conversion is linear (proportional).
        /// Verifies that convert(k*x) = k*convert(x).
        /// </summary>
        [TestMethod]
        public void Convert_IsLinear_Proportional()
        {
            double baseValue = 2.0;
            double scalingFactor = 3.0;

            // convert(k*x)
            double scaledConversion = Quantity.ConvertValue(scalingFactor * baseValue, LengthUnit.FEET, LengthUnit.INCH);

            // k * convert(x)
            double factorTimesConverted = scalingFactor * Quantity.ConvertValue(baseValue, LengthUnit.FEET, LengthUnit.INCH);

            Assert.AreEqual(
                scaledConversion,
                factorTimesConverted,
                NumericTolerance,
                "Conversion should be linear: convert(k*x) = k*convert(x)"
            );
        }

        /// <summary>
        /// Tests that conversion is additive.
        /// Verifies that convert(a+b) = convert(a) + convert(b).
        /// </summary>
        [TestMethod]
        public void Convert_IsAdditive_SumPreserved()
        {
            double firstValue = 1.5;
            double secondValue = 2.5;

            // convert(a+b)
            double sumConverted = Quantity.ConvertValue(firstValue + secondValue, LengthUnit.FEET, LengthUnit.INCH);

            // convert(a) + convert(b)
            double individualSum =
                Quantity.ConvertValue(firstValue, LengthUnit.FEET, LengthUnit.INCH)
                + Quantity.ConvertValue(secondValue, LengthUnit.FEET, LengthUnit.INCH);

            Assert.AreEqual(
                sumConverted,
                individualSum,
                NumericTolerance,
                "Conversion should be additive: convert(a+b) = convert(a) + convert(b)"
            );
        }

        #endregion

        #region Zero and Sign Tests

        /// <summary>
        /// Tests conversion of positive zero and negative zero.
        /// Verifies that -0.0 converts to -0.0 in target unit.
        /// </summary>
        [TestMethod]
        public void Convert_NegativeZero_ReturnsNegativeZero()
        {
            double negativeZeroValue = -0.0;

            double conversionResult = Quantity.ConvertValue(negativeZeroValue, LengthUnit.FEET, LengthUnit.INCH);

            // Check that result is negative zero
            Assert.IsTrue(
                double.IsNegativeInfinity(1.0 / conversionResult),
                "Negative zero should remain negative zero after conversion"
            );
        }

        #endregion

        #region Rounding Tests

        /// <summary>
        /// Tests conversion with values that might cause rounding issues.
        /// Verifies that rounding is handled consistently.
        /// </summary>
        [TestMethod]
        public void Convert_RoundingBehavior_Consistent()
        {
            // Test values that might cause binary floating-point rounding issues
            double[] testValues = { 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0 / 3.0 };

            foreach (double testValue in testValues)
            {
                double conversionResult = Quantity.ConvertValue(testValue, LengthUnit.FEET, LengthUnit.INCH);
                double expectedResult = testValue * 12.0;

                // Should be within tolerance, even with rounding issues
                Assert.AreEqual(
                    expectedResult,
                    conversionResult,
                    NumericTolerance,
                    $"Conversion of {testValue} feet to inches should be accurate"
                );
            }
        }

        #endregion
    }
}