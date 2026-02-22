// ===================================================
// File: LengthUnitTests.cs
// Project: QuantityMeasurementApp.Tests.Models
// Description: Unit tests for LengthUnit enum and extensions - UC4
// Author: Development Team
// Version: 4.0 (UC4 - Added Yard and Centimeter tests)
// ===================================================

using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Test class for LengthUnit enum and extension methods.
    /// UC3: Basic unit tests
    /// UC4: Added Yard and Centimeter conversion tests
    /// </summary>
    [TestClass]
    public class LengthUnitTests
    {
        // ==================== UC3 TESTS (Keep existing) ====================

        [TestMethod]
        public void FeetToFeetConversion_ShouldReturnOne()
        {
            Assert.AreEqual(1.0, LengthUnit.FEET.ToFeet());
        }

        [TestMethod]
        public void FeetToInchesConversion_ShouldReturnTwelve()
        {
            Assert.AreEqual(12.0, LengthUnit.FEET.ToInches());
        }

        [TestMethod]
        public void InchToFeetConversion_ShouldReturnOneByTwelve()
        {
            Assert.AreEqual(1.0 / 12.0, LengthUnit.INCH.ToFeet(), 0.000001);
        }

        [TestMethod]
        public void InchToInchesConversion_ShouldReturnOne()
        {
            Assert.AreEqual(1.0, LengthUnit.INCH.ToInches());
        }

        // ==================== UC4 YARD TESTS ====================

        [TestMethod]
        public void YardToFeetConversion_ShouldReturnThree()
        {
            // Arrange
            LengthUnit unit = LengthUnit.YARD;

            // Act
            double result = unit.ToFeet();

            // Assert
            Assert.AreEqual(3.0, result, 0.000001, "1 YARD should equal 3 FEET");
        }

        [TestMethod]
        public void YardToInchesConversion_ShouldReturnThirtySix()
        {
            // Arrange
            LengthUnit unit = LengthUnit.YARD;

            // Act
            double result = unit.ToInches();

            // Assert
            Assert.AreEqual(36.0, result, 0.000001, "1 YARD should equal 36 INCHES");
        }

        [TestMethod]
        public void YardToYardsConversion_ShouldReturnOne()
        {
            // Arrange
            LengthUnit unit = LengthUnit.YARD;

            // Act
            double result = unit.ToYards();

            // Assert
            Assert.AreEqual(1.0, result, "1 YARD should equal 1 YARD");
        }

        [TestMethod]
        public void YardToCentimetersConversion_ShouldReturnNinetyOnePointFourFour()
        {
            // Arrange
            LengthUnit unit = LengthUnit.YARD;

            // Act
            double result = unit.ToCentimeters();

            // Assert
            Assert.AreEqual(91.44, result, 0.000001, "1 YARD should equal 91.44 CENTIMETERS");
        }

        // ==================== UC4 CENTIMETER TESTS ====================

        [TestMethod]
        public void CentimeterToFeetConversion_ShouldReturnZeroPointZeroThreeTwoEight()
        {
            // Arrange
            LengthUnit unit = LengthUnit.CENTIMETER;

            // Act
            double result = unit.ToFeet();

            // Assert
            Assert.AreEqual(0.0328084, result, 0.000001, "1 CENTIMETER should equal 0.0328084 FEET");
        }

        [TestMethod]
        public void CentimeterToInchesConversion_ShouldReturnZeroPointThreeNineThreeSeven()
        {
            // Arrange
            LengthUnit unit = LengthUnit.CENTIMETER;

            // Act
            double result = unit.ToInches();

            // Assert
            Assert.AreEqual(0.393701, result, 0.000001, "1 CENTIMETER should equal 0.393701 INCHES");
        }

        [TestMethod]
        public void CentimeterToYardsConversion_ShouldReturnZeroPointZeroOneZeroNineThreeSix()
        {
            // Arrange
            LengthUnit unit = LengthUnit.CENTIMETER;

            // Act
            double result = unit.ToYards();

            // Assert
            Assert.AreEqual(0.0109361, result, 0.000001, "1 CENTIMETER should equal 0.0109361 YARDS");
        }

        [TestMethod]
        public void CentimeterToCentimetersConversion_ShouldReturnOne()
        {
            // Arrange
            LengthUnit unit = LengthUnit.CENTIMETER;

            // Act
            double result = unit.ToCentimeters();

            // Assert
            Assert.AreEqual(1.0, result, "1 CENTIMETER should equal 1 CENTIMETER");
        }

        // ==================== UC4 CROSS UNIT VERIFICATION TESTS ====================

        [TestMethod]
        public void VerifyYardInchesFeetRelationship()
        {
            // Verify: 1 yard = 3 feet = 36 inches
            double yardInFeet = LengthUnit.YARD.ToFeet();
            double yardInInches = LengthUnit.YARD.ToInches();

            Assert.AreEqual(3.0, yardInFeet, 0.000001);
            Assert.AreEqual(36.0, yardInInches, 0.000001);
            Assert.AreEqual(yardInFeet * 12, yardInInches, 0.000001);
        }

        [TestMethod]
        public void VerifyCentimeterInchesFeetRelationship()
        {
            // Verify: 1 cm = 0.393701 inches = 0.0328084 feet
            double cmInInches = LengthUnit.CENTIMETER.ToInches();
            double cmInFeet = LengthUnit.CENTIMETER.ToFeet();

            Assert.AreEqual(0.393701, cmInInches, 0.000001);
            Assert.AreEqual(0.0328084, cmInFeet, 0.000001);
            Assert.AreEqual(cmInInches / 12, cmInFeet, 0.000001);
        }

        [TestMethod]
        public void GetDisplayName_ShouldReturnCorrectNames()
        {
            Assert.AreEqual("Feet", LengthUnit.FEET.GetDisplayName());
            Assert.AreEqual("Inches", LengthUnit.INCH.GetDisplayName());
            Assert.AreEqual("Yards", LengthUnit.YARD.GetDisplayName());
            Assert.AreEqual("Centimeters", LengthUnit.CENTIMETER.GetDisplayName());
        }

        [TestMethod]
        public void GetAbbreviation_ShouldReturnCorrectAbbreviations()
        {
            Assert.AreEqual("ft", LengthUnit.FEET.GetAbbreviation());
            Assert.AreEqual("in", LengthUnit.INCH.GetAbbreviation());
            Assert.AreEqual("yd", LengthUnit.YARD.GetAbbreviation());
            Assert.AreEqual("cm", LengthUnit.CENTIMETER.GetAbbreviation());
        }

        [TestMethod]
        public void GetConversionDescription_ShouldReturnCorrectDescriptions()
        {
            StringAssert.Contains(LengthUnit.YARD.GetConversionDescription(), "3 ft");
            StringAssert.Contains(LengthUnit.CENTIMETER.GetConversionDescription(), "0.0328084 ft");
        }
    }
}