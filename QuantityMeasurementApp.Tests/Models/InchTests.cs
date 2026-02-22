// ===================================================
// File: InchTests.cs
// Project: QuantityMeasurementApp.Tests.Models
// Description: Unit tests for Inch class - UC2
// Author: Development Team
// Version: 2.0
// ===================================================

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Test class for Inch measurement functionality.
    /// UC2: Validates inch equality implementation.
    /// </summary>
    [TestClass]
    public class InchTests
    {
        /// <summary>
        /// Test: Two inch objects with same value should be equal.
        /// </summary>
        [TestMethod]
        [Description("Verifies that two Inch objects with same value are equal")]
        [TestCategory("UC2")]
        public void GivenSameValue_ShouldBeEqual()
        {
            // Arrange
            var first = new Inch(10);
            var second = new Inch(10);

            // Act & Assert
            Assert.AreEqual(first, second, "Inches with value 10 should be equal");
        }

        /// <summary>
        /// Test: Two inch objects with different values should not be equal.
        /// </summary>
        [TestMethod]
        [Description("Verifies that two Inch objects with different values are not equal")]
        [TestCategory("UC2")]
        public void GivenDifferentValue_ShouldNotBeEqual()
        {
            // Arrange
            var first = new Inch(10);
            var second = new Inch(12);

            // Act & Assert
            Assert.AreNotEqual(first, second, "Inches with values 10 and 12 should not be equal");
        }

        /// <summary>
        /// Test: Comparing with null should return false.
        /// </summary>
        [TestMethod]
        [Description("Verifies that comparing Inch with null returns false")]
        [TestCategory("UC2")]
        public void GivenNullComparison_ShouldReturnFalse()
        {
            // Arrange
            var inch = new Inch(10);

            // Act & Assert
            Assert.IsFalse(inch.Equals(null), "Inch should not equal null");
        }

        /// <summary>
        /// Test: Same object reference should return true.
        /// </summary>
        [TestMethod]
        [Description("Verifies reflexive property of equality")]
        [TestCategory("UC2")]
        public void GivenSameReference_ShouldReturnTrue()
        {
            // Arrange
            var inch = new Inch(10);

            // Act & Assert
            Assert.IsTrue(inch.Equals(inch), "Object should equal itself");
        }

        /// <summary>
        /// Test: ToString returns formatted string.
        /// </summary>
        [TestMethod]
        [Description("Verifies ToString method returns correct format")]
        [TestCategory("UC2")]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var inch = new Inch(10.75);

            // Act
            string result = inch.ToString();

            // Assert
            Assert.AreEqual("10.75 Inches", result);
        }
    }
}