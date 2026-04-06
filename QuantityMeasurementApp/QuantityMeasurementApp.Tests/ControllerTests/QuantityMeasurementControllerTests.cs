using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLayer.Interfaces;
using ModelLayer.DTOs;
using ModelLayer.Enums;
using QuantityMeasurementApp.API.Controllers;
using QuantityMeasurementApp.API;  // For request DTOs

namespace QuantityMeasurementApp.Tests.ControllerTests
{
    [TestClass]
    public class QuantityMeasurementControllerTests
    {
        private Mock<IQuantityMeasurementService>? _mockService;
        private Mock<ILogger<QuantityMeasurementController>>? _mockLogger;
        private QuantityMeasurementController? _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<IQuantityMeasurementService>();
            _mockLogger = new Mock<ILogger<QuantityMeasurementController>>();
            _controller = new QuantityMeasurementController(_mockService.Object, _mockLogger.Object);

            // Add a default authenticated user context for controller methods that rely on claims
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "1") };
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuth"))
                }
            };
        }

        [TestMethod]
        public async Task CompareQuantities_ValidInput_ReturnsOkResult()
        {
            // Arrange - Use API's CompareRequest directly
            var request = new CompareRequest  // From QuantityMeasurementApp.API namespace
            {
                First = new QuantityInputDTO { Value = 1, Unit = "Feet", MeasurementType = "Length" },
                Second = new QuantityInputDTO { Value = 12, Unit = "Inches", MeasurementType = "Length" }
            };

            var expectedResult = new QuantityMeasurementDTO
            {
                Id = 1,
                Operation = OperationType.Compare,
                MeasurementType = "Length",
                FromValue = 1,
                FromUnit = "Feet",
                ToValue = 12,
                ToUnit = "Inches",
                Result = 1,
                ResultUnit = "Boolean",
                IsError = false,
                CreatedAt = DateTime.Now
            };

            _mockService!.Setup(s => s.CompareQuantities(request.First, request.Second))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller!.CompareQuantities(request);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var returnedDto = okResult.Value as QuantityMeasurementDTO;
            Assert.IsNotNull(returnedDto);
            Assert.AreEqual(expectedResult.Operation, returnedDto.Operation);
            Assert.IsFalse(returnedDto.IsError);
        }

        [TestMethod]
        public async Task CompareQuantities_Error_ReturnsBadRequest()
        {
            // Arrange
            var request = new CompareRequest
            {
                First = new QuantityInputDTO { Value = 1, Unit = "Feet", MeasurementType = "Length" },
                Second = new QuantityInputDTO { Value = 1, Unit = "Kilogram", MeasurementType = "Weight" }
            };

            var errorResult = new QuantityMeasurementDTO
            {
                IsError = true,
                ErrorMessage = "Cannot compare different measurement types"
            };

            _mockService!.Setup(s => s.CompareQuantities(request.First, request.Second))
                .ReturnsAsync(errorResult);

            // Act
            var result = await _controller!.CompareQuantities(request);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [TestMethod]
        public async Task ConvertQuantity_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var request = new ConvertRequest
            {
                Source = new QuantityInputDTO { Value = 1, Unit = "Feet", MeasurementType = "Length" },
                Target = new QuantityInputDTO { Value = 0, Unit = "Inches", MeasurementType = "Length" }
            };

            var expectedResult = new QuantityMeasurementDTO
            {
                Operation = OperationType.Convert,
                MeasurementType = "Length",
                FromValue = 1,
                FromUnit = "Feet",
                ToValue = 12,
                ToUnit = "Inches",
                Result = 12,
                ResultUnit = "Inches",
                IsError = false
            };

            _mockService!.Setup(s => s.ConvertQuantity(request.Source, request.Target))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller!.ConvertQuantity(request);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public async Task AddQuantities_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var request = new AddRequest
            {
                First = new QuantityInputDTO { Value = 1, Unit = "Feet", MeasurementType = "Length" },
                Second = new QuantityInputDTO { Value = 12, Unit = "Inches", MeasurementType = "Length" },
                ResultUnit = "Feet"
            };

            var expectedResult = new QuantityMeasurementDTO
            {
                Operation = OperationType.Add,
                MeasurementType = "Length",
                FromValue = 1,
                FromUnit = "Feet",
                ToValue = 12,
                ToUnit = "Inches",
                Result = 2,
                ResultUnit = "Feet",
                IsError = false
            };

            _mockService!.Setup(s => s.AddQuantities(request.First, request.Second, request.ResultUnit))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller!.AddQuantities(request);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
public async Task GetOperationCount_ReturnsOkResult()
{
    // Arrange
    var operation = OperationType.Compare;
    var expectedCount = 5;

    _mockService!.Setup(s => s.GetOperationCount(operation))
        .ReturnsAsync(expectedCount);

    // Act - Convert enum to string when calling controller
    var result = await _controller!.GetOperationCount(operation.ToString());

    // Assert
    var okResult = result.Result as OkObjectResult;
    Assert.IsNotNull(okResult);
    Assert.AreEqual(200, okResult.StatusCode);
    Assert.AreEqual(expectedCount, okResult.Value);
}

[TestMethod]
public async Task GetOperationHistory_ReturnsOkResult()
{
    // Arrange
    var operation = OperationType.Compare;
    var history = new List<QuantityMeasurementDTO>
    {
        new QuantityMeasurementDTO { Id = 1, Operation = OperationType.Compare },
        new QuantityMeasurementDTO { Id = 2, Operation = OperationType.Compare }
    };

    _mockService!.Setup(s => s.GetOperationHistory(operation))
        .ReturnsAsync(history);

    // Act - Convert enum to string when calling controller
    var result = await _controller!.GetOperationHistory(operation.ToString());

    // Assert
    var okResult = result.Result as OkObjectResult;
    Assert.IsNotNull(okResult);
    Assert.AreEqual(200, okResult.StatusCode);

    var returnedList = okResult.Value as List<QuantityMeasurementDTO>;
    Assert.IsNotNull(returnedList);
    Assert.AreEqual(2, returnedList.Count);
}
    }

}
