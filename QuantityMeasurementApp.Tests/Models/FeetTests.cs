using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    [TestClass]
    public class FeetTests
    {
        [TestMethod]
        public void Equals_SameValue_ReturnsTrue()
        {
            var first = new Feet(1.0);
            var second = new Feet(1.0);

            Assert.IsTrue(first.Equals(second));
        }

        [TestMethod]
        public void Equals_DifferentValue_ReturnsFalse()
        {
            var first = new Feet(1.0);
            var second = new Feet(2.0);

            Assert.IsFalse(first.Equals(second));
        }

        [TestMethod]
        public void Equals_Null_ReturnsFalse()
        {
            var first = new Feet(1.0);

            Assert.IsFalse(first.Equals(null));
        }

        [TestMethod]
        public void GetHashCode_EqualObjects_ReturnSameHash()
        {
            var first = new Feet(5.0);
            var second = new Feet(5.0);

            Assert.AreEqual(first.GetHashCode(), second.GetHashCode());
        }

        [TestMethod] 
        public void ToString_ReturnsFormattedValue()
        {
            var feet = new Feet(3.5);

            Assert.AreEqual("3.5 ft", feet.ToString());
        }
    }
}