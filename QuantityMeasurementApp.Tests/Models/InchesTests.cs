using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Unit tests for Inches value object.
    /// 
    /// These tests ensure UC2 requirement:
    /// Inches equality behaves independently and correctly.
    /// </summary>
    [TestClass]
    public class InchesTests
    {
        /// <summary>
        /// Validates that two Inches objects with identical values are equal.
        /// </summary>
        [TestMethod]
        public void GivenSameValue_ShouldBeEqual()
        {
            var first = new Inches(10);
            var second = new Inches(10);

            Assert.AreEqual(first, second);
        }

        /// <summary>
        /// Ensures two Inches objects with different values are not equal.
        /// </summary>
        [TestMethod]
        public void GivenDifferentValue_ShouldNotBeEqual()
        {
            var first = new Inches(10);
            var second = new Inches(12);

            Assert.AreNotEqual(first, second);
        }

        /// <summary>
        /// Validates null comparison safety.
        /// </summary>
        [TestMethod]
        public void GivenNullComparison_ShouldReturnFalse()
        {
            var inch = new Inches(10);

            Assert.IsFalse(inch.Equals(null));
        }

        /// <summary>
        /// Ensures equality holds for same object reference.
        /// </summary>
        [TestMethod]
        public void GivenSameReference_ShouldReturnTrue()
        {
            var inch = new Inches(10);

            Assert.IsTrue(inch.Equals(inch));
        }
    }
}