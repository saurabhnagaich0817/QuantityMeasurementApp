using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Unit tests for the Inch class
    /// </summary>
    [TestClass]
    public class InchTests
    {
        // Tests Inch.Equals(object?) method for same value comparison
        [TestMethod]
        public void Equals_SameValue_ReturnsTrue()
        {
            var inchInstance1 = new Inch(1.0);
            var inchInstance2 = new Inch(1.0);
            bool comparisonResult = inchInstance1.Equals(inchInstance2);
            Assert.IsTrue(comparisonResult, "1.0 in should equal 1.0 in");
        }

        // Tests Inch.Equals(object?) method for different value comparison
        [TestMethod]
        public void Equals_DifferentValue_ReturnsFalse()
        {
            var inchInstance1 = new Inch(1.0);
            var inchInstance2 = new Inch(2.0);
            bool comparisonResult = inchInstance1.Equals(inchInstance2);
            Assert.IsFalse(comparisonResult, "1.0 in should not equal 2.0 in");
        }

        // Tests Inch.Equals(object?) method for same reference comparison
        [TestMethod]
        public void Equals_SameReference_ReturnsTrue()
        {
            var inchInstance = new Inch(1.0);
            bool comparisonResult = inchInstance.Equals(inchInstance);
            Assert.IsTrue(comparisonResult, "Object should equal itself");
        }

        // Tests Inch.Equals(object?) method when compared with null
        [TestMethod]
        public void Equals_NullComparison_ReturnsFalse()
        {
            var inchInstance = new Inch(1.0);
            bool comparisonResult = inchInstance.Equals(null);
            Assert.IsFalse(comparisonResult, "Object should not equal null");
        }

        // Tests symmetric property of Inch.Equals(object?)
        [TestMethod]
        public void Equals_SymmetricProperty_ReturnsTrue()
        {
            var inchInstance1 = new Inch(1.5);
            var inchInstance2 = new Inch(1.5);
            bool firstToSecondResult = inchInstance1.Equals(inchInstance2);
            bool secondToFirstResult = inchInstance2.Equals(inchInstance1);
            Assert.IsTrue(firstToSecondResult && secondToFirstResult, "Equality should be symmetric");
        }

        // Tests transitive property of Inch.Equals(object?)
        [TestMethod]
        public void Equals_TransitiveProperty_ReturnsTrue()
        {
            var inchFirst = new Inch(2.5);
            var inchSecond = new Inch(2.5);
            var inchThird = new Inch(2.5);
            bool firstEqualsSecond = inchFirst.Equals(inchSecond);
            bool secondEqualsThird = inchSecond.Equals(inchThird);
            bool firstEqualsThird = inchFirst.Equals(inchThird);
            Assert.IsTrue(firstEqualsSecond && secondEqualsThird && firstEqualsThird, "Equality should be transitive");
        }

        // Tests Inch.Equals(object?) method with different object type
        [TestMethod]
        public void Equals_DifferentType_ReturnsFalse()
        {
            var inchInstance = new Inch(1.0);
            var objectInstance = new object();
            bool comparisonResult = inchInstance.Equals(objectInstance);
            Assert.IsFalse(comparisonResult, "Inch should not equal object of different type");
        }

        // Tests consistency of multiple calls to Inch.Equals(object?)
        [TestMethod]
        public void Equals_ConsistentProperty_ReturnsTrue()
        {
            var inchInstance1 = new Inch(3.0);
            var inchInstance2 = new Inch(3.0);
            bool firstCallResult = inchInstance1.Equals(inchInstance2);
            bool secondCallResult = inchInstance1.Equals(inchInstance2);
            bool thirdCallResult = inchInstance1.Equals(inchInstance2);
            Assert.IsTrue(
                firstCallResult && secondCallResult && thirdCallResult,
                "Multiple calls should return consistent results"
            );
        }

        // Tests Inch.Equals(object?) method for floating point precision handling
        [TestMethod]
        public void Equals_FloatingPointPrecision_HandlesCorrectly()
        {
            var inchInstance1 = new Inch(1.000001);
            var inchInstance2 = new Inch(1.000002);
            bool comparisonResult = inchInstance1.Equals(inchInstance2);
            Assert.IsFalse(comparisonResult, "Even very close values should be considered different");
        }

        // Tests Inch.GetHashCode() method for equal objects
        [TestMethod]
        public void GetHashCode_EqualObjects_ReturnsSameHashCode()
        {
            var inchInstance1 = new Inch(5.0);
            var inchInstance2 = new Inch(5.0);
            int hashValue1 = inchInstance1.GetHashCode();
            int hashValue2 = inchInstance2.GetHashCode();
            Assert.AreEqual(hashValue1, hashValue2, "Equal objects should have equal hash codes");
        }

        // Tests Inch.GetHashCode() method for different objects
        [TestMethod]
        public void GetHashCode_DifferentObjects_ReturnsDifferentHashCode()
        {
            var inchInstance1 = new Inch(5.0);
            var inchInstance2 = new Inch(6.0);
            int hashValue1 = inchInstance1.GetHashCode();
            int hashValue2 = inchInstance2.GetHashCode();
            Assert.AreNotEqual(hashValue1, hashValue2, "Different objects should have different hash codes");
        }

        // Tests Inch.ToString() method formatting
        [TestMethod]
        public void ToString_ReturnsFormattedString()
        {
            var inchInstance = new Inch(7.5);
            string stringRepresentation = inchInstance.ToString();
            Assert.AreEqual("7.5 in", stringRepresentation, "ToString should return value with unit");
        }
    }
}