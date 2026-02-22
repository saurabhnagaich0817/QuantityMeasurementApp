// ===================================================
// File: QuantityTests.cs
// Project: QuantityMeasurementApp.Tests.Models
// Description: Unit tests for Quantity class - FIXED for .NET 10.0
// Author: Development Team
// Version: 4.0 (UC4 - Fixed tolerance parameter issues)
// ===================================================

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using System;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Test class for Quantity measurement functionality.
    /// UC3: Basic unit tests
    /// UC4: Added Yard and Centimeter test cases
    /// </summary>
    [TestClass]
    public class QuantityTests
    {
        // ==================== UC3 TESTS (Keep all existing) ====================

        [TestMethod]
        public void GivenSameFeetValue_ShouldReturnTrue()
        {
            // Arrange
            var q1 = new Quantity(5.0, LengthUnit.FEET);
            var q2 = new Quantity(5.0, LengthUnit.FEET);

            // Act & Assert
            Assert.AreEqual(q1, q2);
        }

        [TestMethod]
        public void GivenDifferentFeetValue_ShouldReturnFalse()
        {
            // Arrange
            var q1 = new Quantity(5.0, LengthUnit.FEET);
            var q2 = new Quantity(6.0, LengthUnit.FEET);

            // Act & Assert
            Assert.AreNotEqual(q1, q2);
        }

        [TestMethod]
        public void GivenSameInchValue_ShouldReturnTrue()
        {
            // Arrange
            var q1 = new Quantity(10.0, LengthUnit.INCH);
            var q2 = new Quantity(10.0, LengthUnit.INCH);

            // Act & Assert
            Assert.AreEqual(q1, q2);
        }

        [TestMethod]
        public void GivenDifferentInchValue_ShouldReturnFalse()
        {
            // Arrange
            var q1 = new Quantity(10.0, LengthUnit.INCH);
            var q2 = new Quantity(12.0, LengthUnit.INCH);

            // Act & Assert
            Assert.AreNotEqual(q1, q2);
        }

        [TestMethod]
        public void GivenOneFeetAndTwelveInches_ShouldReturnTrue()
        {
            // Arrange
            var feet = new Quantity(1.0, LengthUnit.FEET);
            var inches = new Quantity(12.0, LengthUnit.INCH);

            // Act & Assert
            Assert.AreEqual(feet, inches);
        }

        [TestMethod]
        public void GivenTwelveInchesAndOneFeet_ShouldReturnTrue()
        {
            // Arrange
            var inches = new Quantity(12.0, LengthUnit.INCH);
            var feet = new Quantity(1.0, LengthUnit.FEET);

            // Act & Assert
            Assert.AreEqual(inches, feet);
        }

        [TestMethod]
        public void GivenTwoFeetAndTwelveInches_ShouldReturnFalse()
        {
            // Arrange
            var feet = new Quantity(2.0, LengthUnit.FEET);
            var inches = new Quantity(12.0, LengthUnit.INCH);

            // Act & Assert
            Assert.AreNotEqual(feet, inches);
        }

        // ==================== UC4 YARD TESTS ====================

        [TestMethod]
        public void GivenSameYardValue_ShouldReturnTrue()
        {
            // Arrange
            var yard1 = new Quantity(5.0, LengthUnit.YARD);
            var yard2 = new Quantity(5.0, LengthUnit.YARD);

            // Act & Assert
            Assert.AreEqual(yard1, yard2);
        }

        [TestMethod]
        public void GivenDifferentYardValue_ShouldReturnFalse()
        {
            // Arrange
            var yard1 = new Quantity(5.0, LengthUnit.YARD);
            var yard2 = new Quantity(6.0, LengthUnit.YARD);

            // Act & Assert
            Assert.AreNotEqual(yard1, yard2);
        }

        [TestMethod]
        public void GivenOneYardAndThreeFeet_ShouldReturnTrue()
        {
            // Arrange
            var yard = new Quantity(1.0, LengthUnit.YARD);
            var feet = new Quantity(3.0, LengthUnit.FEET);

            // Act & Assert
            Assert.AreEqual(yard, feet);
        }

        [TestMethod]
        public void GivenThreeFeetAndOneYard_ShouldReturnTrue()
        {
            // Arrange
            var feet = new Quantity(3.0, LengthUnit.FEET);
            var yard = new Quantity(1.0, LengthUnit.YARD);

            // Act & Assert
            Assert.AreEqual(feet, yard);
        }

        [TestMethod]
        public void GivenOneYardAndThirtySixInches_ShouldReturnTrue()
        {
            // Arrange
            var yard = new Quantity(1.0, LengthUnit.YARD);
            var inches = new Quantity(36.0, LengthUnit.INCH);

            // Act & Assert
            Assert.AreEqual(yard, inches);
        }

        [TestMethod]
        public void GivenThirtySixInchesAndOneYard_ShouldReturnTrue()
        {
            // Arrange
            var inches = new Quantity(36.0, LengthUnit.INCH);
            var yard = new Quantity(1.0, LengthUnit.YARD);

            // Act & Assert
            Assert.AreEqual(inches, yard);
        }

        [TestMethod]
        public void GivenOneYardAndTwoFeet_ShouldReturnFalse()
        {
            // Arrange
            var yard = new Quantity(1.0, LengthUnit.YARD);
            var feet = new Quantity(2.0, LengthUnit.FEET);

            // Act & Assert
            Assert.AreNotEqual(yard, feet);
        }

        [TestMethod]
        public void GivenTwoYardsSixFeetSeventyTwoInches_AllShouldBeEqual()
        {
            // Arrange
            var yards = new Quantity(2.0, LengthUnit.YARD);
            var feet = new Quantity(6.0, LengthUnit.FEET);
            var inches = new Quantity(72.0, LengthUnit.INCH);

            // Act & Assert
            Assert.AreEqual(yards, feet);
            Assert.AreEqual(feet, inches);
            Assert.AreEqual(yards, inches);
        }

        // ==================== UC4 CENTIMETER TESTS ====================

        [TestMethod]
        public void GivenSameCentimeterValue_ShouldReturnTrue()
        {
            // Arrange
            var cm1 = new Quantity(100.0, LengthUnit.CENTIMETER);
            var cm2 = new Quantity(100.0, LengthUnit.CENTIMETER);

            // Act & Assert
            Assert.AreEqual(cm1, cm2);
        }

        [TestMethod]
        public void GivenOneCentimeterAndZeroPointThreeNineThreeSevenInches_ShouldReturnTrue()
        {
            // Arrange
            var cm = new Quantity(1.0, LengthUnit.CENTIMETER);
            var inches = new Quantity(0.393701, LengthUnit.INCH);

            // Act & Assert
            // Use tolerance by comparing converted values directly
            Assert.IsTrue(Math.Abs(cm.ValueInInches - inches.ValueInInches) < 0.000001);
        }

        [TestMethod]
        public void GivenZeroPointThreeNineThreeSevenInchesAndOneCentimeter_ShouldReturnTrue()
        {
            // Arrange
            var inches = new Quantity(0.393701, LengthUnit.INCH);
            var cm = new Quantity(1.0, LengthUnit.CENTIMETER);

            // Act & Assert
            Assert.IsTrue(Math.Abs(inches.ValueInCentimeters - cm.ValueInCentimeters) < 0.000001);
        }

        [TestMethod]
        public void GivenThirtyPointFourEightCentimetersAndOneFeet_ShouldReturnTrue()
        {
            // Arrange
            var cm = new Quantity(30.48, LengthUnit.CENTIMETER);
            var feet = new Quantity(1.0, LengthUnit.FEET);

            // Act & Assert
            // Use tolerance by comparing converted values
            Assert.IsTrue(Math.Abs(cm.ValueInFeet - feet.ValueInFeet) < 0.000001);
        }

        [TestMethod]
public void GivenNinetyOnePointFourFourCentimetersAndOneYard_ShouldReturnTrue()
{
    // Arrange
    var cm = new Quantity(91.44, LengthUnit.CENTIMETER);
    var yard = new Quantity(1.0, LengthUnit.YARD);

    // Act & Assert - Simple one line fix
    Assert.AreEqual(cm, yard);  // Quantity.Equals already has tolerance
}

        [TestMethod]
        public void GivenOneCentimeterAndOneInch_ShouldReturnFalse()
        {
            // Arrange
            var cm = new Quantity(1.0, LengthUnit.CENTIMETER);
            var inch = new Quantity(1.0, LengthUnit.INCH);

            // Act & Assert
            Assert.AreNotEqual(cm, inch);
        }

        // ==================== UC4 CROSS-UNIT COMBINATION TESTS ====================

        [TestMethod]
        public void GivenComplexCrossUnitValues_ShouldBeApproximatelyEqual()
        {
            // Arrange - All represent approximately 1 meter (100 cm)
            var cm = new Quantity(100.0, LengthUnit.CENTIMETER);           // 100 cm
            var inches = new Quantity(39.3701, LengthUnit.INCH);           // ~100 cm
            var feet = new Quantity(3.28084, LengthUnit.FEET);             // ~100 cm
            var yards = new Quantity(1.09361, LengthUnit.YARD);            // ~100 cm

            // Act & Assert - Compare using converted values
            Assert.IsTrue(Math.Abs(cm.ValueInCentimeters - inches.ValueInCentimeters) < 0.1);
            Assert.IsTrue(Math.Abs(cm.ValueInCentimeters - feet.ValueInCentimeters) < 0.1);
            Assert.IsTrue(Math.Abs(cm.ValueInCentimeters - yards.ValueInCentimeters) < 0.1);
            Assert.IsTrue(Math.Abs(inches.ValueInCentimeters - feet.ValueInCentimeters) < 0.1);
            Assert.IsTrue(Math.Abs(feet.ValueInCentimeters - yards.ValueInCentimeters) < 0.1);
        }

        [TestMethod]
        public void GivenTransitiveChain_AllShouldBeEqual()
        {
            // Arrange - Chain of equalities
            var yards = new Quantity(2.0, LengthUnit.YARD);                 // 2 yards
            var feet = new Quantity(6.0, LengthUnit.FEET);                  // 6 feet = 2 yards
            var inches = new Quantity(72.0, LengthUnit.INCH);               // 72 inches = 6 feet
            var cm = new Quantity(182.88, LengthUnit.CENTIMETER);          // 182.88 cm = 72 inches

            // Act & Assert - Test transitive property using object equality
            Assert.AreEqual(yards, feet);
            Assert.AreEqual(feet, inches);
            Assert.AreEqual(inches, cm);
            Assert.AreEqual(yards, cm);
        }

        // ==================== REFERENCE AND NULL TESTS ====================

        [TestMethod]
        public void GivenSameReference_ShouldReturnTrue()
        {
            // Arrange
            var q = new Quantity(5.0, LengthUnit.FEET);

            // Act & Assert
            Assert.IsTrue(q.Equals(q));
        }

        [TestMethod]
        public void GivenNull_ShouldReturnFalse()
        {
            // Arrange
            var q = new Quantity(5.0, LengthUnit.FEET);

            // Act & Assert
            Assert.IsFalse(q.Equals(null));
        }

        // ==================== EXCEPTION TESTS ====================

        [TestMethod]
        public void GivenNaNValue_ShouldThrowArgumentException()
        {
            // Act & Assert
            try
            {
                var q = new Quantity(double.NaN, LengthUnit.FEET);
                Assert.Fail("Expected ArgumentException was not thrown");
            }
            catch (ArgumentException)
            {
                // Test passes
            }
        }

        [TestMethod]
        public void GivenInfinityValue_ShouldThrowArgumentException()
        {
            // Act & Assert
            try
            {
                var q = new Quantity(double.PositiveInfinity, LengthUnit.FEET);
                Assert.Fail("Expected ArgumentException was not thrown");
            }
            catch (ArgumentException)
            {
                // Test passes
            }
        }

        [TestMethod]
        public void GivenInvalidUnit_ShouldThrowArgumentException()
        {
            // Act & Assert
            try
            {
                var q = new Quantity(5.0, (LengthUnit)99);
                Assert.Fail("Expected ArgumentException was not thrown");
            }
            catch (ArgumentException)
            {
                // Test passes
            }
        }

        // ==================== HASH CODE TESTS ====================

        [TestMethod]
        public void EqualObjects_ShouldHaveSameHashCode()
        {
            // Arrange
            var q1 = new Quantity(1.0, LengthUnit.FEET);
            var q2 = new Quantity(12.0, LengthUnit.INCH);

            // Act & Assert
            Assert.AreEqual(q1.GetHashCode(), q2.GetHashCode());
        }

        [TestMethod]
        public void DifferentObjects_ShouldHaveDifferentHashCode()
        {
            // Arrange
            var q1 = new Quantity(1.0, LengthUnit.FEET);
            var q2 = new Quantity(2.0, LengthUnit.FEET);

            // Act & Assert
            Assert.AreNotEqual(q1.GetHashCode(), q2.GetHashCode());
        }

        // ==================== PROPERTY TESTS ====================

        [TestMethod]
        public void ValueInFeet_ShouldReturnCorrectConversion()
        {
            // Arrange
            var feet = new Quantity(2.5, LengthUnit.FEET);
            var inches = new Quantity(30.0, LengthUnit.INCH);
            var yard = new Quantity(1.0, LengthUnit.YARD);
            var cm = new Quantity(91.44, LengthUnit.CENTIMETER);

            // Act & Assert
            Assert.AreEqual(2.5, feet.ValueInFeet, 0.001);
            Assert.AreEqual(2.5, inches.ValueInFeet, 0.001);
            Assert.AreEqual(3.0, yard.ValueInFeet, 0.001);
            Assert.AreEqual(3.0, cm.ValueInFeet, 0.001);
        }

        [TestMethod]
        public void ValueInInches_ShouldReturnCorrectConversion()
        {
            // Arrange
            var feet = new Quantity(2.5, LengthUnit.FEET);
            var inches = new Quantity(30.0, LengthUnit.INCH);
            var yard = new Quantity(1.0, LengthUnit.YARD);
            var cm = new Quantity(91.44, LengthUnit.CENTIMETER);

            // Act & Assert
            Assert.AreEqual(30.0, feet.ValueInInches, 0.001);
            Assert.AreEqual(30.0, inches.ValueInInches, 0.001);
            Assert.AreEqual(36.0, yard.ValueInInches, 0.001);
            Assert.AreEqual(36.0, cm.ValueInInches, 0.001);
        }

        [TestMethod]
        public void ValueInYards_ShouldReturnCorrectConversion()
        {
            // Arrange
            var yard = new Quantity(2.0, LengthUnit.YARD);
            var feet = new Quantity(6.0, LengthUnit.FEET);
            var inches = new Quantity(72.0, LengthUnit.INCH);
            var cm = new Quantity(182.88, LengthUnit.CENTIMETER);

            // Act & Assert
            Assert.AreEqual(2.0, yard.ValueInYards, 0.001);
            Assert.AreEqual(2.0, feet.ValueInYards, 0.001);
            Assert.AreEqual(2.0, inches.ValueInYards, 0.001);
            Assert.AreEqual(2.0, cm.ValueInYards, 0.001);
        }

        [TestMethod]
        public void ValueInCentimeters_ShouldReturnCorrectConversion()
        {
            // Arrange
            var cm = new Quantity(100.0, LengthUnit.CENTIMETER);
            var yard = new Quantity(1.09361, LengthUnit.YARD);
            var feet = new Quantity(3.28084, LengthUnit.FEET);
            var inches = new Quantity(39.3701, LengthUnit.INCH);

            // Act & Assert
            Assert.AreEqual(100.0, cm.ValueInCentimeters, 0.1);
            Assert.AreEqual(100.0, yard.ValueInCentimeters, 0.1);
            Assert.AreEqual(100.0, feet.ValueInCentimeters, 0.1);
            Assert.AreEqual(100.0, inches.ValueInCentimeters, 0.1);
        }

        // ==================== TOSTRING TESTS ====================

        [TestMethod]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var q = new Quantity(5.5, LengthUnit.YARD);

            // Act
            string result = q.ToString();

            // Assert
            Assert.IsTrue(result.Contains("5.5"));
            Assert.IsTrue(result.Contains("yd") || result.Contains("YARD"));
        }

        [TestMethod]
        public void ToDetailedString_ShouldReturnDetailedInfo()
        {
            // Arrange
            var q = new Quantity(1.0, LengthUnit.YARD);

            // Act
            string result = q.ToDetailedString();

            // Assert
            Assert.IsTrue(result.Contains("1"));
            Assert.IsTrue(result.Contains("yd"));
            Assert.IsTrue(result.Contains("ft"));
            Assert.IsTrue(result.Contains("in"));
            Assert.IsTrue(result.Contains("cm"));
        }
    }
}