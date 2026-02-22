// File: QuantityMeasurementApp.Tests/Services/QuantityMeasurementServiceTests.cs

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;
using System;

namespace QuantityMeasurementApp.Tests.Services
{
    [TestClass]
    public class QuantityMeasurementServiceTests
    {
        private QuantityMeasurementService _service = null!;

        [TestInitialize]
        public void Setup()
        {
            _service = new QuantityMeasurementService();
        }

        // ==================== UC1 TESTS (Feet) ====================

        [TestMethod]
        public void FeetService_EqualValues_ReturnsTrue()
        {
            // Arrange
            var feet1 = new Feet(5);
            var feet2 = new Feet(5);

            // Act
            var result = _service.AreEqual(feet1, feet2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void FeetService_DifferentValues_ReturnsFalse()
        {
            // Arrange
            var feet1 = new Feet(5);
            var feet2 = new Feet(7);

            // Act
            var result = _service.AreEqual(feet1, feet2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FeetService_NullFirst_ThrowsArgumentNullException()
        {
            // Arrange
            var feet = new Feet(5);

            // Act & Assert - .NET 10.0 syntax
            try
            {
                _service.AreEqual(null!, feet);
                Assert.Fail("Expected ArgumentNullException was not thrown");
            }
            catch (ArgumentNullException)
            {
                // Test passes - exception was thrown
            }
        }

        // ==================== UC2 TESTS (Inch) ====================

        [TestMethod]
        public void InchService_EqualValues_ReturnsTrue()
        {
            // Arrange
            var inch1 = new Inch(10);
            var inch2 = new Inch(10);

            // Act
            var result = _service.AreEqual(inch1, inch2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InchService_DifferentValues_ReturnsFalse()
        {
            // Arrange
            var inch1 = new Inch(10);
            var inch2 = new Inch(15);

            // Act
            var result = _service.AreEqual(inch1, inch2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InchService_NullFirst_ThrowsArgumentNullException()
        {
            // Arrange
            var inch = new Inch(10);

            // Act & Assert - .NET 10.0 syntax
            try
            {
                _service.AreEqual(null!, inch);
                Assert.Fail("Expected ArgumentNullException was not thrown");
            }
            catch (ArgumentNullException)
            {
                // Test passes - exception was thrown
            }
        }

        // ==================== UC3 TESTS (Quantity) ====================

        [TestMethod]
        public void QuantityService_SameFeet_ReturnsTrue()
        {
            // Arrange
            var q1 = new Quantity(5, LengthUnit.FEET);
            var q2 = new Quantity(5, LengthUnit.FEET);

            // Act
            var result = _service.AreEqual(q1, q2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void QuantityService_FeetAndInches_ReturnsTrue()
        {
            // Arrange
            var feet = new Quantity(1, LengthUnit.FEET);
            var inches = new Quantity(12, LengthUnit.INCH);

            // Act
            var result = _service.AreEqual(feet, inches);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void QuantityService_DifferentUnitsDifferentValues_ReturnsFalse()
        {
            // Arrange
            var feet = new Quantity(2, LengthUnit.FEET);
            var inches = new Quantity(12, LengthUnit.INCH);

            // Act
            var result = _service.AreEqual(feet, inches);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void QuantityService_NullFirst_ThrowsArgumentNullException()
        {
            // Arrange
            var q = new Quantity(5, LengthUnit.FEET);

            // Act & Assert - .NET 10.0 syntax
            try
            {
                _service.AreEqual(null!, q);
                Assert.Fail("Expected ArgumentNullException was not thrown");
            }
            catch (ArgumentNullException)
            {
                // Test passes - exception was thrown
            }
        }

        [TestMethod]
        public void QuantityService_NullSecond_ThrowsArgumentNullException()
        {
            // Arrange
            var q = new Quantity(5, LengthUnit.FEET);

            // Act & Assert - .NET 10.0 syntax
            try
            {
                _service.AreEqual(q, null!);
                Assert.Fail("Expected ArgumentNullException was not thrown");
            }
            catch (ArgumentNullException)
            {
                // Test passes - exception was thrown
            }
        }

        [TestMethod]
        public void AreAllEqual_AllEqual_ReturnsTrue()
        {
            // Arrange
            var q1 = new Quantity(1, LengthUnit.FEET);
            var q2 = new Quantity(12, LengthUnit.INCH);
            var q3 = new Quantity(1, LengthUnit.FEET);

            // Act
            var result = _service.AreAllEqual(q1, q2, q3);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AreAllEqual_OneDifferent_ReturnsFalse()
        {
            // Arrange
            var q1 = new Quantity(1, LengthUnit.FEET);
            var q2 = new Quantity(12, LengthUnit.INCH);
            var q3 = new Quantity(2, LengthUnit.FEET);

            // Act
            var result = _service.AreAllEqual(q1, q2, q3);

            // Assert
            Assert.IsFalse(result);
        }
    }
}