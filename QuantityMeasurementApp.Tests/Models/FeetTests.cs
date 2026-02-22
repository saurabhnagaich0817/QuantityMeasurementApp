// ===================================================
// File: FeetTests.cs
// Project: QuantityMeasurementApp.Tests.Models
// Description: Unit tests for Feet class - UC1
// Author: Development Team
// Version: 1.0
// ===================================================

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Test class for Feet measurement functionality.
    /// UC1: Validates feet equality implementation.
    /// </summary>
    [TestClass]
    public class FeetTests
    {
        /// <summary>
        /// Test: Two feet objects with same value should be equal.
        /// </summary>
        [TestMethod]
        [Description("Verifies that two Feet objects with same value are equal")]
        [TestCategory("UC1")]
        public void GivenSameValue_ShouldBeEqual()
        {
            // Arrange
            var first = new Feet(5);
            var second = new Feet(5);

            // Act & Assert
            Assert.AreEqual(first, second, "Feet with value 5 should be equal");
        }

        /// <summary>
        /// Test: Two feet objects with different values should not be equal.
        /// </summary>
        [TestMethod]
        [Description("Verifies that two Feet objects with different values are not equal")]
        [TestCategory("UC1")]
        public void GivenDifferentValue_ShouldNotBeEqual()
        {
            // Arrange
            var first = new Feet(5);
            var second = new Feet(6);

            // Act & Assert
            Assert.AreNotEqual(first, second, "Feet with values 5 and 6 should not be equal");
        }

        /// <summary>
        /// Test: Comparing with null should return false.
        /// </summary>
        [TestMethod]
        [Description("Verifies that comparing Feet with null returns false")]
        [TestCategory("UC1")]
        public void GivenNullComparison_ShouldReturnFalse()
        {
            // Arrange
            var feet = new Feet(5);

            // Act & Assert
            Assert.IsFalse(feet.Equals(null), "Feet should not equal null");
        }

        /// <summary>
        /// Test: Same object reference should return true.
        /// </summary>
        [TestMethod]
        [Description("Verifies reflexive property of equality")]
        [TestCategory("UC1")]
        public void GivenSameReference_ShouldReturnTrue()
        {
            // Arrange
            var feet = new Feet(5);

            // Act & Assert
            Assert.IsTrue(feet.Equals(feet), "Object should equal itself");
        }

        /// <summary>
        /// Test: ToString returns formatted string.
        /// </summary>
        [TestMethod]
        [Description("Verifies ToString method returns correct format")]
        [TestCategory("UC1")]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var feet = new Feet(5.5);

            // Act
            string result = feet.ToString();

            // Assert
            Assert.AreEqual("5.5 Feet", result);
        }
    }
}