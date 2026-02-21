using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

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

        [TestMethod]
        public void CompareFeetEquality_ValidEqual_ReturnsTrue()
        {
            var first = new Feet(2.0);
            var second = new Feet(2.0);

            Assert.IsTrue(_service.CompareFeetEquality(first, second));
        }

        [TestMethod]
        public void ConvertToFeet_InvalidInput_ReturnsNull()
        {
            var result = _service.ConvertToFeet("abc");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ConvertToFeet_ValidInput_ReturnsFeet()
        {
            var result = _service.ConvertToFeet("4.5");

            Assert.IsNotNull(result);
            Assert.AreEqual(4.5, result!.Value);
        }
    }
}