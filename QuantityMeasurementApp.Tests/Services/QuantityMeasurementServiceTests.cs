using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests.Services
{
    /// <summary>
    /// Contains unit tests for QuantityMeasurementService validating
    /// quantity comparison, parsing logic, static equality checks,
    /// and backward compatibility methods for Feet and Inch.
    /// </summary>
    [TestClass]
    public class QuantityMeasurementServiceTests
    {
        private QuantityMeasurementService _measurementService = null!;

        [TestInitialize]
        public void Setup()
        {
            _measurementService = new QuantityMeasurementService();
        }

        #region Generic Quantity Tests

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) for equal same-unit values
        [TestMethod]
        public void CompareQuantities_BothNonNullEqualValues_SameUnit_ReturnsTrue()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(1.0, LengthUnit.FEET);
            bool comparisonResult = _measurementService.CompareQuantities(quantityOne, quantityTwo);
            Assert.IsTrue(comparisonResult);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) for different same-unit values
        [TestMethod]
        public void CompareQuantities_BothNonNullDifferentValues_SameUnit_ReturnsFalse()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(2.0, LengthUnit.FEET);
            bool comparisonResult = _measurementService.CompareQuantities(quantityOne, quantityTwo);
            Assert.IsFalse(comparisonResult);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) for equivalent cross-unit values
        [TestMethod]
        public void CompareQuantities_CrossUnitEquivalentValues_ReturnsTrue()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(12.0, LengthUnit.INCH);
            bool comparisonResult = _measurementService.CompareQuantities(quantityOne, quantityTwo);
            Assert.IsTrue(comparisonResult);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) for non-equivalent cross-unit values
        [TestMethod]
        public void CompareQuantities_CrossUnitNonEquivalentValues_ReturnsFalse()
        {
            var quantityOne = new Quantity(1.0, LengthUnit.FEET);
            var quantityTwo = new Quantity(13.0, LengthUnit.INCH);
            bool comparisonResult = _measurementService.CompareQuantities(quantityOne, quantityTwo);
            Assert.IsFalse(comparisonResult);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) when first parameter is null
        [TestMethod]
        public void CompareQuantities_FirstParameterNull_ReturnsFalse()
        {
            Quantity? firstQuantity = null;
            var secondQuantity = new Quantity(1.0, LengthUnit.FEET);
            bool comparisonResult = _measurementService.CompareQuantities(firstQuantity, secondQuantity);
            Assert.IsFalse(comparisonResult);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) when second parameter is null
        [TestMethod]
        public void CompareQuantities_SecondParameterNull_ReturnsFalse()
        {
            var firstQuantity = new Quantity(1.0, LengthUnit.FEET);
            Quantity? secondQuantity = null;
            bool comparisonResult = _measurementService.CompareQuantities(firstQuantity, secondQuantity);
            Assert.IsFalse(comparisonResult);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) when both parameters are null
        [TestMethod]
        public void CompareQuantities_BothParametersNull_ReturnsFalse()
        {
            Quantity? firstQuantity = null;
            Quantity? secondQuantity = null;
            bool comparisonResult = _measurementService.CompareQuantities(firstQuantity, secondQuantity);
            Assert.IsFalse(comparisonResult);
        }

        // Tests QuantityMeasurementService.CreateQuantityFromInput(string, LengthUnit) with valid numeric string
        [TestMethod]
        public void CreateQuantityFromInput_ValidNumericString_ReturnsQuantityObject()
        {
            string userInput = "3.5";
            Quantity? createdQuantity = _measurementService.CreateQuantityFromInput(userInput, LengthUnit.FEET);
            Assert.IsNotNull(createdQuantity);
            Assert.AreEqual(3.5, createdQuantity!.NumericValue);
            Assert.AreEqual(LengthUnit.FEET, createdQuantity.MeasurementUnit);
        }

        // Tests QuantityMeasurementService.CreateQuantityFromInput(string, LengthUnit) with null input
        [TestMethod]
        public void CreateQuantityFromInput_NullInput_ReturnsNull()
        {
            string? userInput = null;
            Quantity? createdQuantity = _measurementService.CreateQuantityFromInput(userInput, LengthUnit.FEET);
            Assert.IsNull(createdQuantity);
        }

        // Tests QuantityMeasurementService.CreateQuantityFromInput(string, LengthUnit) with empty string
        [TestMethod]
        public void CreateQuantityFromInput_EmptyString_ReturnsNull()
        {
            string userInput = "";
            Quantity? createdQuantity = _measurementService.CreateQuantityFromInput(userInput, LengthUnit.FEET);
            Assert.IsNull(createdQuantity);
        }

        // Tests QuantityMeasurementService.CreateQuantityFromInput(string, LengthUnit) with whitespace input
        [TestMethod]
        public void CreateQuantityFromInput_Whitespace_ReturnsNull()
        {
            string userInput = "   ";
            Quantity? createdQuantity = _measurementService.CreateQuantityFromInput(userInput, LengthUnit.FEET);
            Assert.IsNull(createdQuantity);
        }

        // Tests QuantityMeasurementService.CreateQuantityFromInput(string, LengthUnit) with non-numeric input
        [TestMethod]
        public void CreateQuantityFromInput_NonNumericString_ReturnsNull()
        {
            string userInput = "abc";
            Quantity? createdQuantity = _measurementService.CreateQuantityFromInput(userInput, LengthUnit.FEET);
            Assert.IsNull(createdQuantity);
        }

        // Tests QuantityMeasurementService.CreateQuantityFromInput(string, LengthUnit) with negative numeric input
        [TestMethod]
        public void CreateQuantityFromInput_NegativeNumber_ReturnsQuantityObject()
        {
            string userInput = "-2.5";
            Quantity? createdQuantity = _measurementService.CreateQuantityFromInput(userInput, LengthUnit.FEET);
            Assert.IsNotNull(createdQuantity);
            Assert.AreEqual(-2.5, createdQuantity!.NumericValue);
        }

        // Tests QuantityMeasurementService.CreateQuantityFromInput(string, LengthUnit) with zero value
        [TestMethod]
        public void CreateQuantityFromInput_Zero_ReturnsQuantityObject()
        {
            string userInput = "0";
            Quantity? createdQuantity = _measurementService.CreateQuantityFromInput(userInput, LengthUnit.FEET);
            Assert.IsNotNull(createdQuantity);
            Assert.AreEqual(0, createdQuantity!.NumericValue);
        }

        // Tests static QuantityMeasurementService.CheckQuantityEquality(double, LengthUnit, double, LengthUnit) for equal same-unit values
        [TestMethod]
        public void CheckQuantityEquality_EqualValues_SameUnit_ReturnsTrue()
        {
            bool equalityResult = QuantityMeasurementService.CheckQuantityEquality(
                1.0,
                LengthUnit.FEET,
                1.0,
                LengthUnit.FEET
            );
            Assert.IsTrue(equalityResult);
        }

        // Tests static QuantityMeasurementService.CheckQuantityEquality(double, LengthUnit, double, LengthUnit) for different same-unit values
        [TestMethod]
        public void CheckQuantityEquality_DifferentValues_SameUnit_ReturnsFalse()
        {
            bool equalityResult = QuantityMeasurementService.CheckQuantityEquality(
                1.0,
                LengthUnit.FEET,
                2.0,
                LengthUnit.FEET
            );
            Assert.IsFalse(equalityResult);
        }

        // Tests static QuantityMeasurementService.CheckQuantityEquality(double, LengthUnit, double, LengthUnit) for equivalent cross-unit values
        [TestMethod]
        public void CheckQuantityEquality_CrossUnitEquivalentValues_ReturnsTrue()
        {
            bool equalityResult = QuantityMeasurementService.CheckQuantityEquality(
                1.0,
                LengthUnit.FEET,
                12.0,
                LengthUnit.INCH
            );
            Assert.IsTrue(equalityResult);
        }

        // Tests static QuantityMeasurementService.CheckQuantityEquality(double, LengthUnit, double, LengthUnit) for non-equivalent cross-unit values
        [TestMethod]
        public void CheckQuantityEquality_CrossUnitNonEquivalentValues_ReturnsFalse()
        {
            bool equalityResult = QuantityMeasurementService.CheckQuantityEquality(
                1.0,
                LengthUnit.FEET,
                13.0,
                LengthUnit.INCH
            );
            Assert.IsFalse(equalityResult);
        }

        #endregion

        #region Backward Compatibility Tests (Feet)

        // Tests QuantityMeasurementService.CompareFeetMeasurements(Feet, Feet)
        [TestMethod]
        public void CompareFeetMeasurements_BothNonNullEqualValues_ReturnsTrue()
        {
            var firstFeet = new Feet(1.0);
            var secondFeet = new Feet(1.0);
            bool comparisonResult = _measurementService.CompareFeetMeasurements(firstFeet, secondFeet);
            Assert.IsTrue(comparisonResult);
        }

        // Tests QuantityMeasurementService.CreateFeetFromString(string)
        [TestMethod]
        public void CreateFeetFromString_ValidNumericString_ReturnsFeetObject()
        {
            string userInput = "3.5";
            Feet? createdFeet = _measurementService.CreateFeetFromString(userInput);
            Assert.IsNotNull(createdFeet);
            Assert.AreEqual(3.5, createdFeet!.Measurement, 0.0001);
        }

        #endregion

        #region Backward Compatibility Tests (Inch)

        // Tests QuantityMeasurementService.CompareInchMeasurements(Inch, Inch)
        [TestMethod]
        public void CompareInchMeasurements_BothNonNullEqualValues_ReturnsTrue()
        {
            var firstInch = new Inch(1.0);
            var secondInch = new Inch(1.0);
            bool comparisonResult = _measurementService.CompareInchMeasurements(firstInch, secondInch);
            Assert.IsTrue(comparisonResult);
        }

        // Tests QuantityMeasurementService.CreateInchFromString(string)
        [TestMethod]
        public void CreateInchFromString_ValidNumericString_ReturnsInchObject()
        {
            string userInput = "3.5";
            Inch? createdInch = _measurementService.CreateInchFromString(userInput);
            Assert.IsNotNull(createdInch);
            Assert.AreEqual(3.5, createdInch!.Measurement, 0.0001);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) for Yard same-unit values
        [TestMethod]
        public void CompareQuantities_YardToYardEqualValues_ReturnsTrue()
        {
            var firstQuantity = new Quantity(1.0, LengthUnit.YARD);
            var secondQuantity = new Quantity(1.0, LengthUnit.YARD);
            bool comparisonResult = _measurementService.CompareQuantities(firstQuantity, secondQuantity);
            Assert.IsTrue(comparisonResult);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) for Yard to Feet equivalent values
        [TestMethod]
        public void CompareQuantities_YardToFeetEquivalentValues_ReturnsTrue()
        {
            var firstQuantity = new Quantity(1.0, LengthUnit.YARD);
            var secondQuantity = new Quantity(3.0, LengthUnit.FEET);
            bool comparisonResult = _measurementService.CompareQuantities(firstQuantity, secondQuantity);
            Assert.IsTrue(comparisonResult);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) for Yard to Inches equivalent values
        [TestMethod]
        public void CompareQuantities_YardToInchesEquivalentValues_ReturnsTrue()
        {
            var firstQuantity = new Quantity(1.0, LengthUnit.YARD);
            var secondQuantity = new Quantity(36.0, LengthUnit.INCH);
            bool comparisonResult = _measurementService.CompareQuantities(firstQuantity, secondQuantity);
            Assert.IsTrue(comparisonResult);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) for Centimeter same-unit values
        [TestMethod]
        public void CompareQuantities_CentimeterToCentimeterEqualValues_ReturnsTrue()
        {
            var firstQuantity = new Quantity(1.0, LengthUnit.CENTIMETER);
            var secondQuantity = new Quantity(1.0, LengthUnit.CENTIMETER);
            bool comparisonResult = _measurementService.CompareQuantities(firstQuantity, secondQuantity);
            Assert.IsTrue(comparisonResult);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) for Centimeter to Inches equivalent values
        [TestMethod]
        public void CompareQuantities_CentimeterToInchesEquivalentValues_ReturnsTrue()
        {
            var firstQuantity = new Quantity(1.0, LengthUnit.CENTIMETER);
            var secondQuantity = new Quantity(0.393700787, LengthUnit.INCH);
            bool comparisonResult = _measurementService.CompareQuantities(firstQuantity, secondQuantity);
            Assert.IsTrue(comparisonResult);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) for Centimeter to Feet equivalent values
        [TestMethod]
        public void CompareQuantities_CentimeterToFeetEquivalentValues_ReturnsTrue()
        {
            var firstQuantity = new Quantity(30.48, LengthUnit.CENTIMETER);
            var secondQuantity = new Quantity(1.0, LengthUnit.FEET);
            bool comparisonResult = _measurementService.CompareQuantities(firstQuantity, secondQuantity);
            Assert.IsTrue(comparisonResult);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) for Centimeter to Yard equivalent values
        [TestMethod]
        public void CompareQuantities_CentimeterToYardEquivalentValues_ReturnsTrue()
        {
            var firstQuantity = new Quantity(91.44, LengthUnit.CENTIMETER);
            var secondQuantity = new Quantity(1.0, LengthUnit.YARD);
            bool comparisonResult = _measurementService.CompareQuantities(firstQuantity, secondQuantity);
            Assert.IsTrue(comparisonResult);
        }

        // Tests static QuantityMeasurementService.CheckQuantityEquality(double, LengthUnit, double, LengthUnit) for Yard to Feet equivalent values
        [TestMethod]
        public void CheckQuantityEquality_YardToFeetEquivalentValues_ReturnsTrue()
        {
            bool equalityResult = QuantityMeasurementService.CheckQuantityEquality(
                1.0,
                LengthUnit.YARD,
                3.0,
                LengthUnit.FEET
            );
            Assert.IsTrue(equalityResult);
        }

        // Tests static QuantityMeasurementService.CheckQuantityEquality(double, LengthUnit, double, LengthUnit) for Yard to Inches equivalent values
        [TestMethod]
        public void CheckQuantityEquality_YardToInchesEquivalentValues_ReturnsTrue()
        {
            bool equalityResult = QuantityMeasurementService.CheckQuantityEquality(
                1.0,
                LengthUnit.YARD,
                36.0,
                LengthUnit.INCH
            );
            Assert.IsTrue(equalityResult);
        }

        // Tests static QuantityMeasurementService.CheckQuantityEquality(double, LengthUnit, double, LengthUnit) for Centimeter to Inches equivalent values
        [TestMethod]
        public void CheckQuantityEquality_CentimeterToInchesEquivalentValues_ReturnsTrue()
        {
            bool equalityResult = QuantityMeasurementService.CheckQuantityEquality(
                1.0,
                LengthUnit.CENTIMETER,
                0.393700787,
                LengthUnit.INCH
            );
            Assert.IsTrue(equalityResult, "Static method: 1 cm should equal 0.393700787 inches");
        }

        // Tests static QuantityMeasurementService.CheckQuantityEquality(double, LengthUnit, double, LengthUnit) for Centimeter to Feet equivalent values
        [TestMethod]
        public void CheckQuantityEquality_CentimeterToFeetEquivalentValues_ReturnsTrue()
        {
            bool equalityResult = QuantityMeasurementService.CheckQuantityEquality(
                30.48,
                LengthUnit.CENTIMETER,
                1.0,
                LengthUnit.FEET
            );
            Assert.IsTrue(equalityResult);
        }

        // Tests static QuantityMeasurementService.CheckQuantityEquality(double, LengthUnit, double, LengthUnit) for Centimeter to Yard equivalent values
        [TestMethod]
        public void CheckQuantityEquality_CentimeterToYardEquivalentValues_ReturnsTrue()
        {
            bool equalityResult = QuantityMeasurementService.CheckQuantityEquality(
                91.44,
                LengthUnit.CENTIMETER,
                1.0,
                LengthUnit.YARD
            );
            Assert.IsTrue(equalityResult);
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) with rounded precision values
        [TestMethod]
        public void CompareQuantities_CentimeterToInches_RoundedValues_ReturnsTrue()
        {
            var firstQuantity = new Quantity(1.0, LengthUnit.CENTIMETER);
            var secondQuantity = new Quantity(0.393701, LengthUnit.INCH);
            bool comparisonResult = _measurementService.CompareQuantities(firstQuantity, secondQuantity);
            Assert.IsTrue(comparisonResult, "1 cm should approximately equal 0.393701 inches");
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) precision for Centimeter to Feet
        [TestMethod]
        public void CompareQuantities_CentimeterToFeet_PrecisionTest()
        {
            var firstQuantity = new Quantity(30.48, LengthUnit.CENTIMETER);
            var secondQuantity = new Quantity(1.0, LengthUnit.FEET);
            bool comparisonResult = _measurementService.CompareQuantities(firstQuantity, secondQuantity);
            Assert.IsTrue(comparisonResult, "30.48 cm should exactly equal 1 foot");
        }

        // Tests QuantityMeasurementService.CompareQuantities(Quantity, Quantity) precision for Centimeter to Yard
        [TestMethod]
        public void CompareQuantities_CentimeterToYard_PrecisionTest()
        {
            var firstQuantity = new Quantity(91.44, LengthUnit.CENTIMETER);
            var secondQuantity = new Quantity(1.0, LengthUnit.YARD);
            bool comparisonResult = _measurementService.CompareQuantities(firstQuantity, secondQuantity);
            Assert.IsTrue(comparisonResult, "91.44 cm should exactly equal 1 yard");
        }

        #endregion
    }
}