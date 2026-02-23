using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Contains unit tests for Quantity class validating equality logic,
    /// unit conversions, hash code consistency, and string formatting behavior.
    /// </summary>
    [TestClass]
    public class QuantityTests
    {
        // Tests Quantity.Equals(object) for same unit and same value
        [TestMethod]
        public void Equals_SameUnitSameValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(1.0, LengthUnit.FEET);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "1.0 ft should equal 1.0 ft");
        }

        // Tests Quantity.Equals(object) for same unit with different values
        [TestMethod]
        public void Equals_SameUnitDifferentValue_ReturnsFalse()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(2.0, LengthUnit.FEET);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsFalse(comparisonResult, "1.0 ft should not equal 2.0 ft");
        }

        // Tests Quantity.Equals(object) for Inch to Inch same value
        [TestMethod]
        public void Equals_InchToInchSameValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.INCH);
            var quantityTwo = new Quantity(1.0, LengthUnit.INCH);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "1.0 in should equal 1.0 in");
        }

        // Tests Quantity.Equals(object) for Inch to Inch different value
        [TestMethod]
        public void Equals_InchToInchDifferentValue_ReturnsFalse()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.INCH);
            var quantityTwo = new Quantity(2.0, LengthUnit.INCH);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsFalse(comparisonResult, "1.0 in should not equal 2.0 in");
        }

        // Tests Quantity.Equals(object) for Feet to Inch equivalent values
        [TestMethod]
        public void Equals_FeetToInchEquivalentValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(12.0, LengthUnit.INCH);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "1.0 ft should equal 12.0 in");
        }

        // Tests Quantity.Equals(object) for Inch to Feet equivalent values
        [TestMethod]
        public void Equals_InchToFeetEquivalentValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(12.0, LengthUnit.INCH);
            var quantityTwo = new Quantity(1.0, LengthUnit.FEET);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "12.0 in should equal 1.0 ft");
        }

        // Tests Quantity.Equals(object) for non-equivalent Feet and Inch values
        [TestMethod]
        public void Equals_FeetToInchNonEquivalentValue_ReturnsFalse()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(13.0, LengthUnit.INCH);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsFalse(comparisonResult, "1.0 ft should not equal 13.0 in");
        }

        // Tests Quantity.Equals(object) for reflexive property
        [TestMethod]
        public void Equals_SameReference_ReturnsTrue()
        {
            var quantity = new Quantity(1.0, LengthUnit.FEET);
            bool comparisonResult = quantity.Equals(quantity);
            Assert.IsTrue(comparisonResult, "Object should equal itself");
        }

        // Tests Quantity.Equals(object) for null comparison
        [TestMethod]
        public void Equals_NullComparison_ReturnsFalse()
        {
            var quantity = new Quantity(1.0, LengthUnit.FEET);
            bool comparisonResult = quantity.Equals(null);
            Assert.IsFalse(comparisonResult, "Object should not equal null");
        }

        // Tests Quantity.Equals(object) for symmetric property
        [TestMethod]
        public void Equals_SymmetricProperty_ReturnsTrue()
        {
            var quantityOne = new Quantity(1.5, LengthUnit.FEET);
            var quantityTwo = new Quantity(1.5, LengthUnit.FEET);
            bool firstToSecondResult = quantityOne.Equals(quantityTwo);
            bool secondToFirstResult = quantityTwo.Equals(quantityOne);
            Assert.IsTrue(firstToSecondResult && secondToFirstResult, "Equality should be symmetric");
        }

        // Tests Quantity.Equals(object) for transitive property
        [TestMethod]
        public void Equals_TransitiveProperty_ReturnsTrue()
        {
            var quantityFirst = new Quantity(2.5, LengthUnit.FEET);
            var quantitySecond = new Quantity(2.5, LengthUnit.FEET);
            var quantityThird = new Quantity(2.5, LengthUnit.FEET);
            bool firstEqualsSecond = quantityFirst.Equals(quantitySecond);
            bool secondEqualsThird = quantitySecond.Equals(quantityThird);
            bool firstEqualsThird = quantityFirst.Equals(quantityThird);
            Assert.IsTrue(firstEqualsSecond && secondEqualsThird && firstEqualsThird, "Equality should be transitive");
        }

        // Tests Quantity.Equals(object) for different type comparison
        [TestMethod]
        public void Equals_DifferentType_ReturnsFalse()
        {
            var quantity = new Quantity(1.0, LengthUnit.FEET);
            var objectInstance = new object();
            bool comparisonResult = quantity.Equals(objectInstance);
            Assert.IsFalse(comparisonResult, "Quantity should not equal object of different type");
        }

        // Tests Quantity.Equals(object) for consistent repeated calls
        [TestMethod]
        public void Equals_ConsistentProperty_ReturnsTrue()
        {
            var quantityOne = new Quantity(3.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(3.0, LengthUnit.FEET);
            bool firstCallResult = quantityOne.Equals(quantityTwo);
            bool secondCallResult = quantityOne.Equals(quantityTwo);
            bool thirdCallResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(
                firstCallResult && secondCallResult && thirdCallResult,
                "Multiple calls should return consistent results"
            );
        }

        // Tests Quantity.Equals(object) for floating point precision handling
        [TestMethod]
        public void Equals_FloatingPointPrecision_HandlesCorrectly()
        {
            var quantityOne = new Quantity(1.000001, LengthUnit.FEET);
            var quantityTwo = new Quantity(1.000002, LengthUnit.FEET);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsFalse(comparisonResult, "Even very close values should be considered different");
        }

        // Tests Quantity.GetHashCode() for equal objects
        [TestMethod]
        public void GetHashCode_EqualObjects_ReturnsSameHashCode()
        {
            var quantityOne = new Quantity(5.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(5.0, LengthUnit.FEET);
            int hashValue1 = quantityOne.GetHashCode();
            int hashValue2 = quantityTwo.GetHashCode();
            Assert.AreEqual(hashValue1, hashValue2);
        }

        // Tests Quantity.GetHashCode() for different objects
        [TestMethod]
        public void GetHashCode_DifferentObjects_ReturnsDifferentHashCode()
        {
            var quantityOne = new Quantity(5.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(6.0, LengthUnit.FEET);
            int hashValue1 = quantityOne.GetHashCode();
            int hashValue2 = quantityTwo.GetHashCode();
            Assert.AreNotEqual(hashValue1, hashValue2);
        }

        // Tests Quantity.GetHashCode() for equivalent cross-unit objects
        [TestMethod]
        public void GetHashCode_EquivalentCrossUnitObjects_ReturnsSameHashCode()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(12.0, LengthUnit.INCH);
            int hashValue1 = quantityOne.GetHashCode();
            int hashValue2 = quantityTwo.GetHashCode();
            Assert.AreEqual(hashValue1, hashValue2, "Equivalent quantities should have equal hash codes");
        }

        // Tests Quantity.ToString() for Feet formatting
        [TestMethod]
        public void ToString_FeetUnit_ReturnsFormattedString()
        {
            var quantity = new Quantity(7.5, LengthUnit.FEET);
            string stringRepresentation = quantity.ToString();
            Assert.AreEqual("7.5 ft", stringRepresentation);
        }

        // Tests Quantity.ToString() for Inch formatting
        [TestMethod]
        public void ToString_InchUnit_ReturnsFormattedString()
        {
            var quantity = new Quantity(7.5, LengthUnit.INCH);
            string stringRepresentation = quantity.ToString();
            Assert.AreEqual("7.5 in", stringRepresentation);
        }
    }
}