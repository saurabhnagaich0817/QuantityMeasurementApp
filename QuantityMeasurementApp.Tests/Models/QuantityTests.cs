// File: QuantityMeasurementApp.Tests/Models/QuantityTests.cs

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using System;

namespace QuantityMeasurementApp.Tests.Models
{
    [TestClass]
    public class QuantityTests
    {
        // ==================== SAME UNIT TESTS ====================

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

        // ==================== CROSS UNIT TESTS ====================

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

        [TestMethod]
        public void GivenMultipleEquivalentValues_ShouldAllBeEqual()
        {
            // Arrange
            var q1 = new Quantity(2.0, LengthUnit.FEET);
            var q2 = new Quantity(24.0, LengthUnit.INCH);
            var q3 = new Quantity(2.0, LengthUnit.FEET);

            // Act & Assert
            Assert.IsTrue(q1.Equals(q2));
            Assert.IsTrue(q2.Equals(q3));
            Assert.IsTrue(q1.Equals(q3));
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

        [TestMethod]
        public void GivenDifferentType_ShouldReturnFalse()
        {
            // Arrange
            var q = new Quantity(5.0, LengthUnit.FEET);
            var obj = new object();

            // Act & Assert
            Assert.IsFalse(q.Equals(obj));
        }

        // ==================== EXCEPTION TESTS ====================

        [TestMethod]
        public void GivenNaNValue_ShouldThrowArgumentException()
        {
            // Act & Assert - .NET 10.0 syntax
            try
            {
                var q = new Quantity(double.NaN, LengthUnit.FEET);
                Assert.Fail("Expected ArgumentException was not thrown");
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("NaN") || ex.Message.Contains("invalid"));
            }
        }

        [TestMethod]
        public void GivenInfinityValue_ShouldThrowArgumentException()
        {
            // Act & Assert - .NET 10.0 syntax
            try
            {
                var q = new Quantity(double.PositiveInfinity, LengthUnit.FEET);
                Assert.Fail("Expected ArgumentException was not thrown");
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("infinite") || ex.Message.Contains("invalid"));
            }
        }

        [TestMethod]
        public void GivenInvalidUnit_ShouldThrowArgumentException()
        {
            // Act & Assert - .NET 10.0 syntax
            try
            {
                var q = new Quantity(5.0, (LengthUnit)99);
                Assert.Fail("Expected ArgumentException was not thrown");
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Invalid unit") || ex.Message.Contains("unit"));
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

            // Act & Assert
            Assert.AreEqual(2.5, feet.ValueInFeet, 0.001);
            Assert.AreEqual(2.5, inches.ValueInFeet, 0.001);
        }

        [TestMethod]
        public void ValueInInches_ShouldReturnCorrectConversion()
        {
            // Arrange
            var feet = new Quantity(2.5, LengthUnit.FEET);
            var inches = new Quantity(30.0, LengthUnit.INCH);

            // Act & Assert
            Assert.AreEqual(30.0, feet.ValueInInches, 0.001);
            Assert.AreEqual(30.0, inches.ValueInInches, 0.001);
        }

        // ==================== TOSTRING TESTS ====================

        [TestMethod]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var q = new Quantity(5.5, LengthUnit.FEET);

            // Act
            string result = q.ToString();

            // Assert
            Assert.AreEqual("5.5 FEET", result);
        }

        [TestMethod]
        public void ToDetailedString_ShouldReturnDetailedInfo()
        {
            // Arrange
            var q = new Quantity(1.0, LengthUnit.FEET);

            // Act
            string result = q.ToDetailedString();

            // Assert
            Assert.IsTrue(result.Contains("1 FEET"));
            Assert.IsTrue(result.Contains("feet"));
            Assert.IsTrue(result.Contains("inches"));
        }
    }
}