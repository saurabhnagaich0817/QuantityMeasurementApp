using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Unit tests for the Feet class
    /// </summary>
    [TestClass]
    public class FeetTests
    {
        // Tests Feet.Equals(object?) method for same value comparison
        [TestMethod]
        public void Equals_SameValue_ReturnsTrue()
        {
            var feetInstance1 = new Feet(1.0);
            var feetInstance2 = new Feet(1.0);
            bool comparisonResult = feetInstance1.Equals(feetInstance2);
            Assert.IsTrue(comparisonResult, "1.0 ft should equal 1.0 ft");
        }

        // Tests Feet.Equals(object?) method for different value comparison
        [TestMethod]
        public void Equals_DifferentValue_ReturnsFalse()
        {
            var feetInstance1 = new Feet(1.0);
            var feetInstance2 = new Feet(2.0);
            bool comparisonResult = feetInstance1.Equals(feetInstance2);
            Assert.IsFalse(comparisonResult, "1.0 ft should not equal 2.0 ft");
        }

        // Tests Feet.Equals(object?) method for same reference comparison
        [TestMethod]
        public void Equals_SameReference_ReturnsTrue()
        {
            var feetInstance = new Feet(1.0);
            bool comparisonResult = feetInstance.Equals(feetInstance);
            Assert.IsTrue(comparisonResult, "Object should equal itself");
        }

        // Tests Feet.Equals(object?) method when compared with null
        [TestMethod]
        public void Equals_NullComparison_ReturnsFalse()
        {
            var feetInstance = new Feet(1.0);
            bool comparisonResult = feetInstance.Equals(null);
            Assert.IsFalse(comparisonResult, "Object should not equal null");
        }

        // Tests symmetric property of Feet.Equals(object?)
        [TestMethod]
        public void Equals_SymmetricProperty_ReturnsTrue()
        {
            var feetInstance1 = new Feet(1.5);
            var feetInstance2 = new Feet(1.5);
            bool firstToSecondResult = feetInstance1.Equals(feetInstance2);
            bool secondToFirstResult = feetInstance2.Equals(feetInstance1);
            Assert.IsTrue(firstToSecondResult && secondToFirstResult, "Equality should be symmetric");
        }

        // Tests transitive property of Feet.Equals(object?)
        [TestMethod]
        public void Equals_TransitiveProperty_ReturnsTrue()
        {
            var feetFirst = new Feet(2.5);
            var feetSecond = new Feet(2.5);
            var feetThird = new Feet(2.5);
            bool firstEqualsSecond = feetFirst.Equals(feetSecond);
            bool secondEqualsThird = feetSecond.Equals(feetThird);
            bool firstEqualsThird = feetFirst.Equals(feetThird);
            Assert.IsTrue(firstEqualsSecond && secondEqualsThird && firstEqualsThird, "Equality should be transitive");
        }

        // Tests Feet.Equals(object?) method with different object type
        [TestMethod]
        public void Equals_DifferentType_ReturnsFalse()
        {
            var feetInstance = new Feet(1.0);
            var objectInstance = new object();
            bool comparisonResult = feetInstance.Equals(objectInstance);
            Assert.IsFalse(comparisonResult, "Feet should not equal object of different type");
        }

        // Tests consistency of multiple calls to Feet.Equals(object?)
        [TestMethod]
        public void Equals_ConsistentProperty_ReturnsTrue()
        {
            var feetInstance1 = new Feet(3.0);
            var feetInstance2 = new Feet(3.0);
            bool firstCallResult = feetInstance1.Equals(feetInstance2);
            bool secondCallResult = feetInstance1.Equals(feetInstance2);
            bool thirdCallResult = feetInstance1.Equals(feetInstance2);
            Assert.IsTrue(
                firstCallResult && secondCallResult && thirdCallResult,
                "Multiple calls should return consistent results"
            );
        }

        // Tests Feet.Equals(object?) method for floating point precision handling
        [TestMethod]
        public void Equals_FloatingPointPrecision_HandlesCorrectly()
        {
            var feetInstance1 = new Feet(1.000001);
            var feetInstance2 = new Feet(1.000002);
            bool comparisonResult = feetInstance1.Equals(feetInstance2);
            Assert.IsFalse(comparisonResult, "Even very close values should be considered different");
        }

        // Tests Feet.GetHashCode() method for equal objects
        [TestMethod]
        public void GetHashCode_EqualObjects_ReturnsSameHashCode()
        {
            var feetInstance1 = new Feet(5.0);
            var feetInstance2 = new Feet(5.0);
            int hashValue1 = feetInstance1.GetHashCode();
            int hashValue2 = feetInstance2.GetHashCode();
            Assert.AreEqual(hashValue1, hashValue2, "Equal objects should have equal hash codes");
        }

        // Tests Feet.GetHashCode() method for different objects
        [TestMethod]
        public void GetHashCode_DifferentObjects_ReturnsDifferentHashCode()
        {
            var feetInstance1 = new Feet(5.0);
            var feetInstance2 = new Feet(6.0);
            int hashValue1 = feetInstance1.GetHashCode();
            int hashValue2 = feetInstance2.GetHashCode();
            Assert.AreNotEqual(hashValue1, hashValue2, "Different objects should have different hash codes");
        }

        // Tests Feet.ToString() method formatting
        [TestMethod]
        public void ToString_ReturnsFormattedString()
        {
            var feetInstance = new Feet(7.5);
            string stringRepresentation = feetInstance.ToString();
            Assert.AreEqual("7.5 ft", stringRepresentation, "ToString should return value with unit");
        }
    }
}