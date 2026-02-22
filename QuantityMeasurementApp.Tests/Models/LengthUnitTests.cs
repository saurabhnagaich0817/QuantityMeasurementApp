// ===================================================
// File: LengthUnitTests.cs
// Project: QuantityMeasurementApp.Tests.Models
// Description: Unit tests for LengthUnit enum and extensions - UC3
// Author: Development Team
// Version: 3.0
// ===================================================

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Test class for LengthUnit enum and extension methods.
    /// UC3: Validates unit conversion factors.
    /// </summary>
    [TestClass]
    public class LengthUnitTests
    {
        /// <summary>
        /// Test: FEET to FEET conversion should return 1.0
        /// </summary>
        [TestMethod]
        [Description("Verifies that FEET to FEET conversion returns 1.0")]
        [TestCategory("UC3")]
        public void FeetToFeetConversion_ShouldReturnOne()
        {
            // Arrange
            LengthUnit unit = LengthUnit.FEET;

            // Act
            double result = unit.ToFeet();

            // Assert
            Assert.AreEqual(1.0, result, "FEET to FEET conversion should be 1.0");
        }

        /// <summary>
        /// Test: FEET to INCHES conversion should return 12.0
        /// </summary>
        [TestMethod]
        [Description("Verifies that FEET to INCHES conversion returns 12.0")]
        [TestCategory("UC3")]
        public void FeetToInchesConversion_ShouldReturnTwelve()
        {
            // Arrange
            LengthUnit unit = LengthUnit.FEET;

            // Act
            double result = unit.ToInches();

            // Assert
            Assert.AreEqual(12.0, result, "FEET to INCHES conversion should be 12.0");
        }

        /// <summary>
        /// Test: INCH to FEET conversion should return 1/12
        /// </summary>
        [TestMethod]
        [Description("Verifies that INCH to FEET conversion returns 1/12")]
        [TestCategory("UC3")]
        public void InchToFeetConversion_ShouldReturnOneByTwelve()
        {
            // Arrange
            LengthUnit unit = LengthUnit.INCH;

            // Act
            double result = unit.ToFeet();

            // Assert
            Assert.AreEqual(1.0 / 12.0, result, 0.000001, "INCH to FEET should be 1/12");
        }

        /// <summary>
        /// Test: INCH to INCHES conversion should return 1.0
        /// </summary>
        [TestMethod]
        [Description("Verifies that INCH to INCHES conversion returns 1.0")]
        [TestCategory("UC3")]
        public void InchToInchesConversion_ShouldReturnOne()
        {
            // Arrange
            LengthUnit unit = LengthUnit.INCH;

            // Act
            double result = unit.ToInches();

            // Assert
            Assert.AreEqual(1.0, result, "INCH to INCHES conversion should be 1.0");
        }

        /// <summary>
        /// Test: GetDisplayName returns correct names
        /// </summary>
        [TestMethod]
        [Description("Verifies GetDisplayName returns correct unit names")]
        [TestCategory("UC3")]
        public void GetDisplayName_ShouldReturnCorrectNames()
        {
            Assert.AreEqual("Feet", LengthUnit.FEET.GetDisplayName());
            Assert.AreEqual("Inches", LengthUnit.INCH.GetDisplayName());
        }
    }
}