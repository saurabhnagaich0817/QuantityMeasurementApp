using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Contains extended unit tests for Quantity class covering equality comparisons,
    /// unit conversions across different length units, transitive properties,
    /// hash code consistency, and string formatting.
    /// </summary>
    [TestClass]
    public class QuantityExtendedTests
    {
        // Tolerance for floating point comparisons
        private const double NumericTolerance = 0.000001;

        #region Yard Tests

        // Tests Quantity.Equals(object) for Yard to Yard same value
        [TestMethod]
        public void Equals_YardToYardSameValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.YARD);
            var quantityTwo = new Quantity(1.0, LengthUnit.YARD);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "1.0 yd should equal 1.0 yd");
        }

        // Tests Quantity.Equals(object) for Yard to Yard different value
        [TestMethod]
        public void Equals_YardToYardDifferentValue_ReturnsFalse()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.YARD);
            var quantityTwo = new Quantity(2.0, LengthUnit.YARD);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsFalse(comparisonResult, "1.0 yd should not equal 2.0 yd");
        }

        // Tests Quantity.Equals(object) for Yard to Feet equivalent value
        [TestMethod]
        public void Equals_YardToFeetEquivalentValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.YARD);
            var quantityTwo = new Quantity(3.0, LengthUnit.FEET);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "1.0 yd should equal 3.0 ft");
        }

        // Tests Quantity.Equals(object) for Feet to Yard equivalent value
        [TestMethod]
        public void Equals_FeetToYardEquivalentValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(3.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(1.0, LengthUnit.YARD);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "3.0 ft should equal 1.0 yd");
        }

        // Tests Quantity.Equals(object) for Yard to Inches equivalent value
        [TestMethod]
        public void Equals_YardToInchesEquivalentValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.YARD);
            var quantityTwo = new Quantity(36.0, LengthUnit.INCH);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "1.0 yd should equal 36.0 in");
        }

        // Tests Quantity.Equals(object) for Inches to Yard equivalent value
        [TestMethod]
        public void Equals_InchesToYardEquivalentValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(36.0, LengthUnit.INCH);
            var quantityTwo = new Quantity(1.0, LengthUnit.YARD);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "36.0 in should equal 1.0 yd");
        }

        // Tests Quantity.Equals(object) for non-equivalent Yard and Feet values
        [TestMethod]
        public void Equals_YardToFeetNonEquivalentValue_ReturnsFalse()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.YARD);
            var quantityTwo = new Quantity(2.0, LengthUnit.FEET);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsFalse(comparisonResult, "1.0 yd should not equal 2.0 ft");
        }

        // Tests Quantity.Equals(object) for non-equivalent Yard and Inches values
        [TestMethod]
        public void Equals_YardToInchesNonEquivalentValue_ReturnsFalse()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.YARD);
            var quantityTwo = new Quantity(35.0, LengthUnit.INCH);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsFalse(comparisonResult, "1.0 yd should not equal 35.0 in");
        }

        #endregion

        #region Centimeter Tests

        // Tests Quantity.Equals(object) for Centimeter to Centimeter same value
        [TestMethod]
        public void Equals_CentimeterToCentimeterSameValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.CENTIMETER);
            var quantityTwo = new Quantity(1.0, LengthUnit.CENTIMETER);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "1.0 cm should equal 1.0 cm");
        }

        // Tests Quantity.Equals(object) for Centimeter to Centimeter different value
        [TestMethod]
        public void Equals_CentimeterToCentimeterDifferentValue_ReturnsFalse()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.CENTIMETER);
            var quantityTwo = new Quantity(2.0, LengthUnit.CENTIMETER);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsFalse(comparisonResult, "1.0 cm should not equal 2.0 cm");
        }

        // Tests Quantity.Equals(object) for Centimeter to Inches equivalent value
        [TestMethod]
        public void Equals_CentimeterToInchesEquivalentValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.CENTIMETER);
            var quantityTwo = new Quantity(0.393700787, LengthUnit.INCH);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "1.0 cm should equal 0.393700787 in");
        }

        // Tests Quantity.Equals(object) for Inches to Centimeter equivalent value
        [TestMethod]
        public void Equals_InchesToCentimeterEquivalentValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(0.393700787, LengthUnit.INCH);
            var quantityTwo = new Quantity(1.0, LengthUnit.CENTIMETER);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "0.393700787 in should equal 1.0 cm");
        }

        // Tests Quantity.Equals(object) for Centimeter to Feet equivalent value
        [TestMethod]
        public void Equals_CentimeterToFeetEquivalentValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(30.48, LengthUnit.CENTIMETER);
            var quantityTwo = new Quantity(1.0, LengthUnit.FEET);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "30.48 cm should equal 1.0 ft");
        }

        // Tests Quantity.Equals(object) for Feet to Centimeter equivalent value
        [TestMethod]
        public void Equals_FeetToCentimeterEquivalentValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(30.48, LengthUnit.CENTIMETER);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "1.0 ft should equal 30.48 cm");
        }

        // Tests Quantity.Equals(object) for Centimeter to Yard equivalent value
        [TestMethod]
        public void Equals_CentimeterToYardEquivalentValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(91.44, LengthUnit.CENTIMETER);
            var quantityTwo = new Quantity(1.0, LengthUnit.YARD);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "91.44 cm should equal 1.0 yd");
        }

        // Tests Quantity.Equals(object) for Yard to Centimeter equivalent value
        [TestMethod]
        public void Equals_YardToCentimeterEquivalentValue_ReturnsTrue()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.YARD);
            var quantityTwo = new Quantity(91.44, LengthUnit.CENTIMETER);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsTrue(comparisonResult, "1.0 yd should equal 91.44 cm");
        }

        // Tests Quantity.Equals(object) for non-equivalent Centimeter and Inch values
        [TestMethod]
        public void Equals_CentimeterToInchesNonEquivalentValue_ReturnsFalse()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.CENTIMETER);
            var quantityTwo = new Quantity(1.0, LengthUnit.INCH);
            bool comparisonResult = quantityOne.Equals(quantityTwo);
            Assert.IsFalse(comparisonResult, "1.0 cm should not equal 1.0 in");
        }

        #endregion

        #region Multi-Unit Transitive Tests

        // Tests Quantity.Equals(object) for transitive property across Yard, Feet, and Inches
        [TestMethod]
        public void Equals_TransitiveYardFeetInches_ReturnsTrue()
        {
            var yardsQuantity = new Quantity(2.0, LengthUnit.YARD);
            var feetQuantity = new Quantity(6.0, LengthUnit.FEET);
            var inchesQuantity = new Quantity(72.0, LengthUnit.INCH);

            bool yardsEqualsFeet = yardsQuantity.Equals(feetQuantity);
            bool feetEqualsInches = feetQuantity.Equals(inchesQuantity);
            bool yardsEqualsInches = yardsQuantity.Equals(inchesQuantity);

            Assert.IsTrue(yardsEqualsFeet);
            Assert.IsTrue(feetEqualsInches);
            Assert.IsTrue(yardsEqualsInches);
        }

        // Tests Quantity.Equals(object) for transitive property across Centimeter, Inches, and Feet
        [TestMethod]
        public void Equals_TransitiveCmInchesFeet_ReturnsTrue()
        {
            var cmQuantity = new Quantity(30.48, LengthUnit.CENTIMETER);
            var inchesQuantity = new Quantity(12.0, LengthUnit.INCH);
            var feetQuantity = new Quantity(1.0, LengthUnit.FEET);

            bool cmEqualsInches = cmQuantity.Equals(inchesQuantity);
            bool inchesEqualsFeet = inchesQuantity.Equals(feetQuantity);
            bool cmEqualsFeet = cmQuantity.Equals(feetQuantity);

            Assert.IsTrue(cmEqualsInches);
            Assert.IsTrue(inchesEqualsFeet);
            Assert.IsTrue(cmEqualsFeet);
        }

        // Tests Quantity.Equals(object) for complex equality across all units
        [TestMethod]
        public void Equals_AllUnitsComplexScenario_ReturnsTrue()
        {
            var yardQuantity = new Quantity(1.0, LengthUnit.YARD);
            var feetQuantity = new Quantity(3.0, LengthUnit.FEET);
            var inchesQuantity = new Quantity(36.0, LengthUnit.INCH);
            var cmQuantity = new Quantity(91.44, LengthUnit.CENTIMETER);

            Assert.IsTrue(yardQuantity.Equals(feetQuantity));
            Assert.IsTrue(yardQuantity.Equals(inchesQuantity));
            Assert.IsTrue(yardQuantity.Equals(cmQuantity));
            Assert.IsTrue(feetQuantity.Equals(inchesQuantity));
            Assert.IsTrue(feetQuantity.Equals(cmQuantity));
            Assert.IsTrue(inchesQuantity.Equals(cmQuantity));
        }

        #endregion

        #region Edge Cases and Validation

        // Tests Quantity.Equals(object) for reflexive property (Yard)
        [TestMethod]
        public void Equals_YardSameReference_ReturnsTrue()
        {
            var quantity = new Quantity(1.0, LengthUnit.YARD);
            bool comparisonResult = quantity.Equals(quantity);
            Assert.IsTrue(comparisonResult);
        }

        // Tests Quantity.Equals(object) for reflexive property (Centimeter)
        [TestMethod]
        public void Equals_CentimeterSameReference_ReturnsTrue()
        {
            var quantity = new Quantity(1.0, LengthUnit.CENTIMETER);
            bool comparisonResult = quantity.Equals(quantity);
            Assert.IsTrue(comparisonResult);
        }

        // Tests Quantity.Equals(object) for null comparison
        [TestMethod]
        public void Equals_YardNullComparison_ReturnsFalse()
        {
            var quantity = new Quantity(1.0, LengthUnit.YARD);
            bool comparisonResult = quantity.Equals(null);
            Assert.IsFalse(comparisonResult);
        }

        // Tests Quantity.Equals(object) for null comparison
        [TestMethod]
        public void Equals_CentimeterNullComparison_ReturnsFalse()
        {
            var quantity = new Quantity(1.0, LengthUnit.CENTIMETER);
            bool comparisonResult = quantity.Equals(null);
            Assert.IsFalse(comparisonResult);
        }

        // Tests Quantity.Equals(object) for different type comparison
        [TestMethod]
        public void Equals_YardDifferentType_ReturnsFalse()
        {
            var quantity = new Quantity(1.0, LengthUnit.YARD);
            var objectInstance = new object();
            bool comparisonResult = quantity.Equals(objectInstance);
            Assert.IsFalse(comparisonResult);
        }

        #endregion

        #region GetHashCode Tests

        // Tests Quantity.GetHashCode() for equal Yard values
        [TestMethod]
        public void GetHashCode_EqualYards_ReturnsSameHashCode()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.YARD);
            var quantityTwo = new Quantity(1.0, LengthUnit.YARD);
            Assert.AreEqual(quantityOne.GetHashCode(), quantityTwo.GetHashCode());
        }

        // Tests Quantity.GetHashCode() for equal Centimeter values
        [TestMethod]
        public void GetHashCode_EqualCentimeters_ReturnsSameHashCode()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.CENTIMETER);
            var quantityTwo = new Quantity(1.0, LengthUnit.CENTIMETER);
            Assert.AreEqual(quantityOne.GetHashCode(), quantityTwo.GetHashCode());
        }

        // Tests Quantity.GetHashCode() for equivalent Yard and Feet values
        [TestMethod]
        public void GetHashCode_EquivalentYardAndFeet_ReturnsSameHashCode()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.YARD);
            var quantityTwo = new Quantity(3.0, LengthUnit.FEET);
            Assert.AreEqual(quantityOne.GetHashCode(), quantityTwo.GetHashCode());
        }

        // Tests Quantity.GetHashCode() for equivalent Yard and Inches values
        [TestMethod]
        public void GetHashCode_EquivalentYardAndInches_ReturnsSameHashCode()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.YARD);
            var quantityTwo = new Quantity(36.0, LengthUnit.INCH);
            Assert.AreEqual(quantityOne.GetHashCode(), quantityTwo.GetHashCode());
        }

        // Tests Quantity.GetHashCode() for equivalent Yard and Centimeter values
        [TestMethod]
        public void GetHashCode_EquivalentYardAndCentimeter_ReturnsSameHashCode()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.YARD);
            var quantityTwo = new Quantity(91.44, LengthUnit.CENTIMETER);
            Assert.AreEqual(quantityOne.GetHashCode(), quantityTwo.GetHashCode());
        }

        // Tests Quantity.GetHashCode() for equivalent Centimeter and Inch values
        [TestMethod]
        public void GetHashCode_EquivalentCentimeterAndInch_ReturnsSameHashCode()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.CENTIMETER);
            var quantityTwo = new Quantity(0.393700787, LengthUnit.INCH);
            Assert.AreEqual(quantityOne.GetHashCode(), quantityTwo.GetHashCode());
        }

        #endregion

        #region ToString Tests

        // Tests Quantity.ToString() for Yard unit formatting
        [TestMethod]
        public void ToString_YardUnit_ReturnsFormattedString()
        {
            var quantity = new Quantity(7.5, LengthUnit.YARD);
            Assert.AreEqual("7.5 yd", quantity.ToString());
        }

        // Tests Quantity.ToString() for Centimeter unit formatting
        [TestMethod]
        public void ToString_CentimeterUnit_ReturnsFormattedString()
        {
            var quantity = new Quantity(7.5, LengthUnit.CENTIMETER);
            Assert.AreEqual("7.5 cm", quantity.ToString());
        }

        #endregion
    }
}