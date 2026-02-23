using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests.Models
{
    /// <summary>
    /// Contains unit tests for validating LengthUnit enum methods including conversion factors,
    /// unit symbols, unit names, and exception handling for invalid units.
    /// </summary>
    [TestClass]
    public class LengthUnitTests
    {
        // Tests LengthUnit.GetFeetConversionFactor() for FEET
        [TestMethod]
        public void GetFeetConversionFactor_FeetUnit_ReturnsOne()
        {
            double factor = LengthUnit.FEET.GetFeetConversionFactor();
            Assert.AreEqual(1.0, factor, 0.0001);
        }

        // Tests LengthUnit.GetFeetConversionFactor() for INCH
        [TestMethod]
        public void GetFeetConversionFactor_InchUnit_ReturnsOneTwelfth()
        {
            double factor = LengthUnit.INCH.GetFeetConversionFactor();
            Assert.AreEqual(1.0 / 12.0, factor, 0.0001);
        }

        // Tests LengthUnit.GetFeetConversionFactor() for YARD
        [TestMethod]
        public void GetFeetConversionFactor_YardUnit_ReturnsThree()
        {
            double factor = LengthUnit.YARD.GetFeetConversionFactor();
            Assert.AreEqual(3.0, factor, 0.0001);
        }

        // Tests LengthUnit.GetFeetConversionFactor() for CENTIMETER
        [TestMethod]
        public void GetFeetConversionFactor_CentimeterUnit_ReturnsCorrectValue()
        {
            double factor = LengthUnit.CENTIMETER.GetFeetConversionFactor();
            // 1 cm = 1/(2.54*12) feet = 1/30.48 feet = 0.0328083989501312 feet
            double expected = 0.0328083989501312;
            Assert.AreEqual(expected, factor, 0.0000001);
        }

        // Tests LengthUnit.GetSymbol() for FEET
        [TestMethod]
        public void GetSymbol_FeetUnit_ReturnsFt()
        {
            string symbol = LengthUnit.FEET.GetSymbol();
            Assert.AreEqual("ft", symbol);
        }

        // Tests LengthUnit.GetSymbol() for INCH
        [TestMethod]
        public void GetSymbol_InchUnit_ReturnsIn()
        {
            string symbol = LengthUnit.INCH.GetSymbol();
            Assert.AreEqual("in", symbol);
        }

        // Tests LengthUnit.GetSymbol() for YARD
        [TestMethod]
        public void GetSymbol_YardUnit_ReturnsYd()
        {
            string symbol = LengthUnit.YARD.GetSymbol();
            Assert.AreEqual("yd", symbol);
        }

        // Tests LengthUnit.GetSymbol() for CENTIMETER
        [TestMethod]
        public void GetSymbol_CentimeterUnit_ReturnsCm()
        {
            string symbol = LengthUnit.CENTIMETER.GetSymbol();
            Assert.AreEqual("cm", symbol);
        }

        // Tests LengthUnit.GetFullName() for FEET
        [TestMethod]
        public void GetFullName_FeetUnit_ReturnsFeet()
        {
            string name = LengthUnit.FEET.GetFullName();
            Assert.AreEqual("feet", name);
        }

        // Tests LengthUnit.GetFullName() for INCH
        [TestMethod]
        public void GetFullName_InchUnit_ReturnsInches()
        {
            string name = LengthUnit.INCH.GetFullName();
            Assert.AreEqual("inches", name);
        }

        // Tests LengthUnit.GetFullName() for YARD
        [TestMethod]
        public void GetFullName_YardUnit_ReturnsYards()
        {
            string name = LengthUnit.YARD.GetFullName();
            Assert.AreEqual("yards", name);
        }

        // Tests LengthUnit.GetFullName() for CENTIMETER
        [TestMethod]
        public void GetFullName_CentimeterUnit_ReturnsCentimeters()
        {
            string name = LengthUnit.CENTIMETER.GetFullName();
            Assert.AreEqual("centimeters", name);
        }

        // Tests LengthUnit.GetFeetConversionFactor() for invalid enum value
        // FIXED: Without using Assert.ThrowsException
        [TestMethod]
        public void GetFeetConversionFactor_InvalidUnit_ThrowsException()
        {
            LengthUnit invalidUnit = (LengthUnit)99;
            
            try
            {
                invalidUnit.GetFeetConversionFactor();
                Assert.Fail("Expected ArgumentException but no exception was thrown");
            }
            catch (ArgumentException)
            {
                // Expected exception - test passes
            }
            catch (Exception ex)
            {
                Assert.Fail($"Expected ArgumentException but got {ex.GetType().Name}");
            }
        }
    }
}