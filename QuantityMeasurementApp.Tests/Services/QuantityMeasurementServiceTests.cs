using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests.Services
{
    /// <summary>
    /// Unit tests for QuantityMeasurementService.
    /// 
    /// These tests validate business logic separation from models.
    /// Service layer acts as abstraction over equality comparison.
    /// </summary>
    [TestClass]
    public class QuantityMeasurementServiceTests
    {
        private readonly QuantityMeasurementService _service = new();

        /// <summary>
        /// Ensures service correctly returns true
        /// when two Feet objects are equal.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrue_ForEqualFeet()
        {
            var result = _service.AreEqual(new Feet(5), new Feet(5));

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Ensures service returns false
        /// when Feet values differ.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalse_ForDifferentFeet()
        {
            var result = _service.AreEqual(new Feet(5), new Feet(7));

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Validates correct equality behavior for Inches.
        /// </summary>
        [TestMethod]
        public void ShouldReturnTrue_ForEqualInches()
        {
            var result = _service.AreEqual(new Inches(10), new Inches(10));

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Ensures service identifies inequality for different Inches values.
        /// </summary>
        [TestMethod]
        public void ShouldReturnFalse_ForDifferentInches()
        {
            var result = _service.AreEqual(new Inches(10), new Inches(15));

            Assert.IsFalse(result);
        }

        /// <summary>
        /// Verifies that passing null to service throws exception.
        /// Ensures defensive programming in business layer.
        /// </summary>
        [TestMethod]
public void ShouldThrowException_WhenNullPassed()
{
    Assert.Throws<ArgumentNullException>(
        () => _service.AreEqual(null!, new Feet(5))
    );
}
    }
}