using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Test class for Quantity conversion functionality.
    /// Tests the Convert method and conversion operations between all supported units.
    /// </summary>
    [TestClass]
    public class QuantityConversionTests
    {
        private const double NumericTolerance = 0.000001;

        #region Basic Unit Conversion Tests

        // Tests conversion from Feet to Inches.
        // Verifies that 1 foot converts to 12 inches.
        [TestMethod]
        public void Convert_FeetToInches_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(1.0, LengthUnit.FEET, LengthUnit.INCH);
            Assert.AreEqual(12.0, conversionResult, NumericTolerance, "1 foot should equal 12 inches");
        }

        // Tests conversion from Inches to Feet.
        // Verifies that 24 inches convert to 2 feet.
        [TestMethod]
        public void Convert_InchesToFeet_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(24.0, LengthUnit.INCH, LengthUnit.FEET);
            Assert.AreEqual(2.0, conversionResult, NumericTolerance, "24 inches should equal 2 feet");
        }

        // Tests conversion from Yards to Feet.
        // Verifies that 1 yard converts to 3 feet.
        [TestMethod]
        public void Convert_YardsToFeet_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(1.0, LengthUnit.YARD, LengthUnit.FEET);
            Assert.AreEqual(3.0, conversionResult, NumericTolerance, "1 yard should equal 3 feet");
        }

        // Tests conversion from Feet to Yards.
        // Verifies that 6 feet convert to 2 yards.
        [TestMethod]
        public void Convert_FeetToYards_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(6.0, LengthUnit.FEET, LengthUnit.YARD);
            Assert.AreEqual(2.0, conversionResult, NumericTolerance, "6 feet should equal 2 yards");
        }

        // Tests conversion from Yards to Inches.
        // Verifies that 1 yard converts to 36 inches.
        [TestMethod]
        public void Convert_YardsToInches_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(1.0, LengthUnit.YARD, LengthUnit.INCH);
            Assert.AreEqual(36.0, conversionResult, NumericTolerance, "1 yard should equal 36 inches");
        }

        // Tests conversion from Inches to Yards.
        // Verifies that 72 inches convert to 2 yards.
        [TestMethod]
        public void Convert_InchesToYards_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(72.0, LengthUnit.INCH, LengthUnit.YARD);
            Assert.AreEqual(2.0, conversionResult, NumericTolerance, "72 inches should equal 2 yards");
        }

        // Tests conversion from Centimeters to Inches.
        // Verifies that 2.54 cm converts to 1 inch.
        [TestMethod]
        public void Convert_CentimetersToInches_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(2.54, LengthUnit.CENTIMETER, LengthUnit.INCH);
            Assert.AreEqual(1.0, conversionResult, NumericTolerance, "2.54 cm should equal 1 inch");
        }

        // Tests conversion from Inches to Centimeters.
        // Verifies that 1 inch converts to 2.54 cm.
        [TestMethod]
        public void Convert_InchesToCentimeters_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(1.0, LengthUnit.INCH, LengthUnit.CENTIMETER);
            Assert.AreEqual(2.54, conversionResult, NumericTolerance, "1 inch should equal 2.54 cm");
        }

        // Tests conversion from Centimeters to Feet.
        // Verifies that 30.48 cm converts to 1 foot.
        [TestMethod]
        public void Convert_CentimetersToFeet_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(30.48, LengthUnit.CENTIMETER, LengthUnit.FEET);
            Assert.AreEqual(1.0, conversionResult, NumericTolerance, "30.48 cm should equal 1 foot");
        }

        // Tests conversion from Feet to Centimeters.
        // Verifies that 1 foot converts to 30.48 cm.
        [TestMethod]
        public void Convert_FeetToCentimeters_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(1.0, LengthUnit.FEET, LengthUnit.CENTIMETER);
            Assert.AreEqual(30.48, conversionResult, NumericTolerance, "1 foot should equal 30.48 cm");
        }

        // Tests conversion from Centimeters to Yards.
        // Verifies that 91.44 cm converts to 1 yard.
        [TestMethod]
        public void Convert_CentimetersToYards_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(91.44, LengthUnit.CENTIMETER, LengthUnit.YARD);
            Assert.AreEqual(1.0, conversionResult, NumericTolerance, "91.44 cm should equal 1 yard");
        }

        // Tests conversion from Yards to Centimeters.
        // Verifies that 1 yard converts to 91.44 cm.
        [TestMethod]
        public void Convert_YardsToCentimeters_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(1.0, LengthUnit.YARD, LengthUnit.CENTIMETER);
            Assert.AreEqual(91.44, conversionResult, NumericTolerance, "1 yard should equal 91.44 cm");
        }

        #endregion

        #region Cross-Unit Conversion Tests

        // Tests conversion from Yards directly to Centimeters with decimal value.
        // Verifies the multi-step conversion through base unit is accurate.
        [TestMethod]
        public void Convert_YardsToCentimeters_DecimalValue_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(2.5, LengthUnit.YARD, LengthUnit.CENTIMETER);
            double expectedResult = 2.5 * 91.44; // 2.5 yards * 91.44 cm per yard
            Assert.AreEqual(
                expectedResult,
                conversionResult,
                NumericTolerance,
                "2.5 yards should equal the expected centimeters"
            );
        }

        // Tests conversion from Inches to Centimeters with fractional value.
        // Verifies conversion between non-adjacent units.
        [TestMethod]
        public void Convert_InchesToCentimeters_FractionalValue_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(12.5, LengthUnit.INCH, LengthUnit.CENTIMETER);
            double expectedResult = 12.5 * 2.54; // 12.5 inches * 2.54 cm per inch
            Assert.AreEqual(
                expectedResult,
                conversionResult,
                NumericTolerance,
                "12.5 inches should equal correct centimeters"
            );
        }

        // Tests conversion from Feet to Centimeters with decimal value.
        // Verifies conversion between non-adjacent units.
        [TestMethod]
        public void Convert_FeetToCentimeters_DecimalValue_ReturnsCorrectValue()
        {
            double conversionResult = Quantity.ConvertValue(3.5, LengthUnit.FEET, LengthUnit.CENTIMETER);
            double expectedResult = 3.5 * 30.48; // 3.5 feet * 30.48 cm per foot
            Assert.AreEqual(
                expectedResult,
                conversionResult,
                NumericTolerance,
                "3.5 feet should equal correct centimeters"
            );
        }

        #endregion

        #region Zero Value Tests

        // Tests conversion of zero value.
        // Verifies that zero in any unit converts to zero.
        [TestMethod]
        public void Convert_ZeroValue_ReturnsZero()
        {
            double conversionResult = Quantity.ConvertValue(0.0, LengthUnit.FEET, LengthUnit.INCH);
            Assert.AreEqual(0.0, conversionResult, NumericTolerance, "Zero feet should convert to zero inches");

            conversionResult = Quantity.ConvertValue(0.0, LengthUnit.YARD, LengthUnit.CENTIMETER);
            Assert.AreEqual(0.0, conversionResult, NumericTolerance, "Zero yards should convert to zero cm");

            conversionResult = Quantity.ConvertValue(0.0, LengthUnit.INCH, LengthUnit.FEET);
            Assert.AreEqual(0.0, conversionResult, NumericTolerance, "Zero inches should convert to zero feet");
        }

        #endregion

        #region Negative Value Tests

        // Tests conversion of negative values.
        // Verifies that sign is preserved during conversion.
        [TestMethod]
        public void Convert_NegativeValue_PreservesSign()
        {
            double conversionResult = Quantity.ConvertValue(-1.0, LengthUnit.FEET, LengthUnit.INCH);
            Assert.AreEqual(-12.0, conversionResult, NumericTolerance, "-1 foot should convert to -12 inches");

            conversionResult = Quantity.ConvertValue(-2.5, LengthUnit.YARD, LengthUnit.FEET);
            Assert.AreEqual(-7.5, conversionResult, NumericTolerance, "-2.5 yards should convert to -7.5 feet");

            conversionResult = Quantity.ConvertValue(-30.48, LengthUnit.CENTIMETER, LengthUnit.FEET);
            Assert.AreEqual(-1.0, conversionResult, NumericTolerance, "-30.48 cm should convert to -1 foot");
        }

        #endregion

        #region Large Value Tests

        // Tests conversion of very large values.
        // Verifies that conversion maintains precision for large numbers.
        [TestMethod]
        public void Convert_LargeValues_MaintainsPrecision()
        {
            double conversionResult = Quantity.ConvertValue(1000000.0, LengthUnit.FEET, LengthUnit.INCH);
            Assert.AreEqual(
                12000000.0,
                conversionResult,
                NumericTolerance * 1000000,
                "1,000,000 feet should equal 12,000,000 inches"
            );
        }

        #endregion

        #region Small Value Tests

        // Tests conversion of very small values.
        // Verifies that conversion maintains precision for small numbers.
        [TestMethod]
        public void Convert_SmallValues_MaintainsPrecision()
        {
            double conversionResult = Quantity.ConvertValue(0.000001, LengthUnit.FEET, LengthUnit.INCH);
            Assert.AreEqual(
                0.000012,
                conversionResult,
                NumericTolerance,
                "0.000001 feet should equal 0.000012 inches"
            );

            conversionResult = Quantity.ConvertValue(0.000001, LengthUnit.CENTIMETER, LengthUnit.INCH);
            double expectedResult = 0.000001 * 0.393700787;
            Assert.AreEqual(expectedResult, conversionResult, NumericTolerance, "0.000001 cm should convert correctly");
        }

        #endregion

        #region Same-Unit Tests

        // Tests conversion where source and target units are the same.
        // Verifies that value remains unchanged.
        [TestMethod]
        public void Convert_SameUnit_ReturnsOriginalValue()
        {
            double conversionResult = Quantity.ConvertValue(5.0, LengthUnit.FEET, LengthUnit.FEET);
            Assert.AreEqual(
                5.0,
                conversionResult,
                NumericTolerance,
                "Converting feet to feet should return original value"
            );

            conversionResult = Quantity.ConvertValue(7.5, LengthUnit.INCH, LengthUnit.INCH);
            Assert.AreEqual(
                7.5,
                conversionResult,
                NumericTolerance,
                "Converting inches to inches should return original value"
            );

            conversionResult = Quantity.ConvertValue(3.2, LengthUnit.YARD, LengthUnit.YARD);
            Assert.AreEqual(
                3.2,
                conversionResult,
                NumericTolerance,
                "Converting yards to yards should return original value"
            );

            conversionResult = Quantity.ConvertValue(10.0, LengthUnit.CENTIMETER, LengthUnit.CENTIMETER);
            Assert.AreEqual(
                10.0,
                conversionResult,
                NumericTolerance,
                "Converting cm to cm should return original value"
            );
        }

        #endregion

        #region Round-Trip Tests

        // Tests round-trip conversion (A → B → A).
        // Verifies that conversion is reversible within tolerance.
        [TestMethod]
        public void Convert_RoundTrip_ReturnsOriginalValue()
        {
            double originalInputValue = 3.5;

            double toInchesResult = Quantity.ConvertValue(originalInputValue, LengthUnit.FEET, LengthUnit.INCH);
            double backToFeetResult = Quantity.ConvertValue(toInchesResult, LengthUnit.INCH, LengthUnit.FEET);

            Assert.AreEqual(
                originalInputValue,
                backToFeetResult,
                NumericTolerance,
                "Round-trip feet→inches→feet should return original"
            );

            originalInputValue = 2.0;
            double toCmResult = Quantity.ConvertValue(originalInputValue, LengthUnit.YARD, LengthUnit.CENTIMETER);
            double backToYardsResult = Quantity.ConvertValue(toCmResult, LengthUnit.CENTIMETER, LengthUnit.YARD);

            Assert.AreEqual(
                originalInputValue,
                backToYardsResult,
                NumericTolerance,
                "Round-trip yards→cm→yards should return original"
            );
        }

        // Tests multiple round-trip conversions through all units.
        // Verifies that value is preserved through multiple conversions.
        [TestMethod]
        public void Convert_MultiStepRoundTrip_ReturnsOriginalValue()
        {
            double originalInputValue = 1.0;

            // Feet → Inches → Centimeters → Yards → Feet
            double toInchesResult = Quantity.ConvertValue(originalInputValue, LengthUnit.FEET, LengthUnit.INCH);
            double toCmResult = Quantity.ConvertValue(toInchesResult, LengthUnit.INCH, LengthUnit.CENTIMETER);
            double toYardsResult = Quantity.ConvertValue(toCmResult, LengthUnit.CENTIMETER, LengthUnit.YARD);
            double backToFeetResult = Quantity.ConvertValue(toYardsResult, LengthUnit.YARD, LengthUnit.FEET);

            Assert.AreEqual(
                originalInputValue,
                backToFeetResult,
                NumericTolerance,
                "Multi-step conversion should return original value"
            );
        }

        #endregion

               #region Invalid Input Tests - FIXED (Without ThrowsException)

        // Tests conversion with invalid source unit.
        // Verifies that ArgumentException is thrown.
        [TestMethod]
        public void Convert_InvalidSourceUnit_ThrowsArgumentException()
        {
            LengthUnit invalidUnit = (LengthUnit)99;
            
            try
            {
                Quantity.ConvertValue(1.0, invalidUnit, LengthUnit.FEET);
                Assert.Fail("Expected ArgumentException but no exception was thrown");
            }
            catch (ArgumentException)
            {
                // Expected exception - test passes
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected ArgumentException but got {ex.GetType().Name}");
            }
        }

        // Tests conversion with invalid target unit.
        // Verifies that ArgumentException is thrown.
        [TestMethod]
        public void Convert_InvalidTargetUnit_ThrowsArgumentException()
        {
            LengthUnit invalidUnit = (LengthUnit)99;
            
            try
            {
                Quantity.ConvertValue(1.0, LengthUnit.FEET, invalidUnit);
                Assert.Fail("Expected ArgumentException but no exception was thrown");
            }
            catch (ArgumentException)
            {
                // Expected exception - test passes
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected ArgumentException but got {ex.GetType().Name}");
            }
        }

        // Tests conversion with NaN value.
        // Verifies that ArgumentException is thrown.
        [TestMethod]
        public void Convert_NaNValue_ThrowsArgumentException()
        {
            try
            {
                Quantity.ConvertValue(double.NaN, LengthUnit.FEET, LengthUnit.INCH);
                Assert.Fail("Expected ArgumentException but no exception was thrown");
            }
            catch (ArgumentException)
            {
                // Expected exception - test passes
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected ArgumentException but got {ex.GetType().Name}");
            }
        }

        // Tests conversion with Positive Infinity value.
        // Verifies that ArgumentException is thrown.
        [TestMethod]
        public void Convert_PositiveInfinity_ThrowsArgumentException()
        {
            try
            {
                Quantity.ConvertValue(double.PositiveInfinity, LengthUnit.FEET, LengthUnit.INCH);
                Assert.Fail("Expected ArgumentException but no exception was thrown");
            }
            catch (ArgumentException)
            {
                // Expected exception - test passes
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected ArgumentException but got {ex.GetType().Name}");
            }
        }

        // Tests conversion with Negative Infinity value.
        // Verifies that ArgumentException is thrown.
        [TestMethod]
        public void Convert_NegativeInfinity_ThrowsArgumentException()
        {
            try
            {
                Quantity.ConvertValue(double.NegativeInfinity, LengthUnit.FEET, LengthUnit.INCH);
                Assert.Fail("Expected ArgumentException but no exception was thrown");
            }
            catch (ArgumentException)
            {
                // Expected exception - test passes
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected ArgumentException but got {ex.GetType().Name}");
            }
        }

        #endregion
   
      
        #region Instance Method Tests

        // Tests instance ConvertTo method.
        // Verifies that instance method returns correct converted value.
        [TestMethod]
        public void ConvertTo_InstanceMethod_ReturnsCorrectQuantity()
        {
            var sourceQuantity = new Quantity(1.0, LengthUnit.FEET);
            var convertedQuantity = sourceQuantity.ConvertTo(LengthUnit.INCH);

            Assert.AreEqual(
                12.0,
                convertedQuantity.NumericValue,
                NumericTolerance,
                "Instance ConvertTo should return correct value"
            );
            Assert.AreEqual(
                LengthUnit.INCH,
                convertedQuantity.MeasurementUnit,
                "Instance ConvertTo should set correct target unit"
            );
        }

        // Tests instance ConvertTo method with zero value.
        // Verifies that zero converts correctly.
        [TestMethod]
        public void ConvertTo_InstanceMethod_ZeroValue_ReturnsZero()
        {
            var sourceQuantity = new Quantity(0.0, LengthUnit.FEET);
            var convertedQuantity = sourceQuantity.ConvertTo(LengthUnit.INCH);

            Assert.AreEqual(0.0, convertedQuantity.NumericValue, NumericTolerance, "Zero should convert to zero");
            Assert.AreEqual(LengthUnit.INCH, convertedQuantity.MeasurementUnit);
        }

        // Tests instance ConvertTo method with negative value.
        // Verifies that negative values preserve sign.
        [TestMethod]
        public void ConvertTo_InstanceMethod_NegativeValue_PreservesSign()
        {
            var sourceQuantity = new Quantity(-2.5, LengthUnit.YARD);
            var convertedQuantity = sourceQuantity.ConvertTo(LengthUnit.FEET);

            Assert.AreEqual(
                -7.5,
                convertedQuantity.NumericValue,
                NumericTolerance,
                "Negative values should preserve sign"
            );
            Assert.AreEqual(LengthUnit.FEET, convertedQuantity.MeasurementUnit);
        }

        // Tests instance ConvertTo method with same unit.
        // Verifies that value remains unchanged.
        [TestMethod]
        public void ConvertTo_InstanceMethod_SameUnit_ReturnsSameValue()
        {
            var sourceQuantity = new Quantity(5.0, LengthUnit.FEET);
            var convertedQuantity = sourceQuantity.ConvertTo(LengthUnit.FEET);

            Assert.AreEqual(
                5.0,
                convertedQuantity.NumericValue,
                NumericTolerance,
                "Same unit conversion should return same value"
            );
            Assert.AreEqual(LengthUnit.FEET, convertedQuantity.MeasurementUnit);
        }

        #endregion

        #region Precision Tests

        // Tests conversion precision with many decimal places.
        // Verifies that conversion maintains mathematical accuracy.
        [TestMethod]
        public void Convert_PrecisionTest_MaintainsAccuracy()
        {
            // Test with values that have many decimal places
            double conversionResult = Quantity.ConvertValue(1.23456789, LengthUnit.FEET, LengthUnit.INCH);
            double expectedResult = 1.23456789 * 12.0;
            Assert.AreEqual(expectedResult, conversionResult, 0.000001, "Conversion should maintain precision");

            conversionResult = Quantity.ConvertValue(2.3456789, LengthUnit.YARD, LengthUnit.CENTIMETER);
            expectedResult = 2.3456789 * 91.44;
            Assert.AreEqual(
                expectedResult,
                conversionResult,
                0.0001,
                "Yard to cm conversion should maintain precision"
            );

            conversionResult = Quantity.ConvertValue(3.456789, LengthUnit.CENTIMETER, LengthUnit.INCH);
            expectedResult = 3.456789 * 0.393700787;
            Assert.AreEqual(
                expectedResult,
                conversionResult,
                0.000001,
                "Cm to inch conversion should maintain precision"
            );
        }

        #endregion

        #region Bidirectional Tests

        // Tests bidirectional conversion (A→B and B→A are inverses).
        // Verifies that conversion factors are mathematically consistent.
        [TestMethod]
        public void Convert_Bidirectional_AreInverses()
        {
            double inputValue = 5.0;

            double feetToInchesResult = Quantity.ConvertValue(inputValue, LengthUnit.FEET, LengthUnit.INCH);
            double inchesToFeetResult = Quantity.ConvertValue(feetToInchesResult, LengthUnit.INCH, LengthUnit.FEET);
            Assert.AreEqual(
                inputValue,
                inchesToFeetResult,
                NumericTolerance,
                "Feet↔Inches conversions should be inverses"
            );

            double yardsToFeetResult = Quantity.ConvertValue(inputValue, LengthUnit.YARD, LengthUnit.FEET);
            double feetToYardsResult = Quantity.ConvertValue(yardsToFeetResult, LengthUnit.FEET, LengthUnit.YARD);
            Assert.AreEqual(
                inputValue,
                feetToYardsResult,
                NumericTolerance,
                "Yards↔Feet conversions should be inverses"
            );

            double cmToInchesResult = Quantity.ConvertValue(inputValue, LengthUnit.CENTIMETER, LengthUnit.INCH);
            double inchesToCmResult = Quantity.ConvertValue(
                cmToInchesResult,
                LengthUnit.INCH,
                LengthUnit.CENTIMETER
            );
            Assert.AreEqual(
                inputValue,
                inchesToCmResult,
                NumericTolerance,
                "Cm↔Inches conversions should be inverses"
            );
        }

        #endregion
    }
}