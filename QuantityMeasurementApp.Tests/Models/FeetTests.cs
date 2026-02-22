using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Unit tests for Feet value object.
    /// 
    /// These tests validate:
    /// 1. Value-based equality
    /// 2. Reference equality handling
    /// 3. Null safety
    /// 4. Equality contract behavior
    /// 
    /// This ensures UC1 requirement of correct Feet comparison is fully validated.
    /// </summary>
    [TestClass]
    public class FeetTests
    {
        /// <summary>
        /// Verifies that two Feet objects with the same numeric value are considered equal.
        /// This validates value-based equality implementation.
        /// </summary>
        [TestMethod]
        public void GivenSameValue_ShouldBeEqual()
        {
            var first = new Feet(5);
            var second = new Feet(5);

            Assert.AreEqual(first, second);
        }

        /// <summary>
        /// Verifies that two Feet objects with different values are not equal.
        /// Ensures comparison is based strictly on value.
        /// </summary>
        [TestMethod]
        public void GivenDifferentValue_ShouldNotBeEqual()
        {
            var first = new Feet(5);
            var second = new Feet(6);

            Assert.AreNotEqual(first, second);
        }

        /// <summary>
        /// Ensures comparison with null returns false.
        /// Validates null-safety in Equals implementation.
        /// </summary>
        [TestMethod]
        public void GivenNullComparison_ShouldReturnFalse()
        {
            var feet = new Feet(5);

            Assert.IsFalse(feet.Equals(null));
        }

        /// <summary>
        /// Verifies that object equals itself.
        /// This ensures reference equality shortcut works correctly.
        /// </summary>
        [TestMethod]
        public void GivenSameReference_ShouldReturnTrue()
        {
            var feet = new Feet(5);

            Assert.IsTrue(feet.Equals(feet));
        }
    }
}