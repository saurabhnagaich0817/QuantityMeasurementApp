using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTOs;
using ModelLayer.Enums;
using BusinessLayer.Interfaces;

namespace QuantityMeasurementApp.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class QuantityMeasurementController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;
        private readonly ILogger<QuantityMeasurementController> _logger;

        public QuantityMeasurementController(
            IQuantityMeasurementService service,
            ILogger<QuantityMeasurementController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Compare two quantities
        /// </summary>
        /// <param name="request">Request containing two quantities to compare</param>
        /// <returns>Comparison result</returns>
        [HttpPost("compare")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuantityMeasurementDTO>> CompareQuantities(
            [FromBody] CompareRequest request)
        {
            _logger.LogInformation("Comparing quantities: {First} vs {Second}", 
                request.First, request.Second);
            
            var result = await _service.CompareQuantities(request.First, request.Second);
            
            if (result.IsError)
                return BadRequest(result);
                
            return Ok(result);
        }

        /// <summary>
        /// Convert a quantity to different unit
        /// </summary>
        /// <param name="request">Request containing source and target quantities</param>
        /// <returns>Conversion result</returns>
        [HttpPost("convert")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuantityMeasurementDTO>> ConvertQuantity(
            [FromBody] ConvertRequest request)
        {
            var result = await _service.ConvertQuantity(request.Source, request.Target);
            
            if (result.IsError)
                return BadRequest(result);
                
            return Ok(result);
        }

        /// <summary>
        /// Add two quantities
        /// </summary>
        /// <param name="request">Request containing quantities to add and optional result unit</param>
        /// <returns>Addition result</returns>
        [HttpPost("add")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuantityMeasurementDTO>> AddQuantities(
            [FromBody] AddRequest request)
        {
            var result = await _service.AddQuantities(
                request.First, 
                request.Second, 
                request.ResultUnit);
            
            if (result.IsError)
                return BadRequest(result);
                
            return Ok(result);
        }

        /// <summary>
        /// Subtract two quantities
        /// </summary>
        /// <param name="request">Request containing quantities to subtract and optional result unit</param>
        /// <returns>Subtraction result</returns>
        [HttpPost("subtract")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuantityMeasurementDTO>> SubtractQuantities(
            [FromBody] SubtractRequest request)
        {
            var result = await _service.SubtractQuantities(
                request.First, 
                request.Second, 
                request.ResultUnit);
            
            if (result.IsError)
                return BadRequest(result);
                
            return Ok(result);
        }

        /// <summary>
        /// Divide two quantities
        /// </summary>
        /// <param name="request">Request containing quantities to divide</param>
        /// <returns>Division result (ratio)</returns>
        [HttpPost("divide")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<QuantityMeasurementDTO>> DivideQuantities(
            [FromBody] DivideRequest request)
        {
            var result = await _service.DivideQuantities(request.First, request.Second);
            
            if (result.IsError)
                return BadRequest(result);
                
            return Ok(result);
        }

        /// <summary>
        /// Get operation history by operation type
        /// </summary>
        /// <param name="operation">Operation type (COMPARE, CONVERT, ADD, SUBTRACT, DIVIDE)</param>
        /// <returns>List of operations</returns>
        [HttpGet("history/operation/{operation}")]
        [ProducesResponseType(typeof(List<QuantityMeasurementDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<QuantityMeasurementDTO>>> GetOperationHistory(
            string operation)
        {
            if (Enum.TryParse<OperationType>(operation, true, out var operationType))
            {
                var history = await _service.GetOperationHistory(operationType);
                return Ok(history);
            }
            
            return BadRequest($"Invalid operation type: {operation}");
        }

        /// <summary>
        /// Get operation history by measurement type
        /// </summary>
        /// <param name="type">Measurement type (Length, Weight, Volume, Temperature)</param>
        /// <returns>List of operations</returns>
        [HttpGet("history/type/{type}")]
        [ProducesResponseType(typeof(List<QuantityMeasurementDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<QuantityMeasurementDTO>>> GetMeasurementTypeHistory(
            string type)
        {
            var history = await _service.GetMeasurementTypeHistory(type);
            return Ok(history);
        }

        /// <summary>
        /// Get count of operations by type
        /// </summary>
        /// <param name="operation">Operation type (COMPARE, CONVERT, ADD, SUBTRACT, DIVIDE)</param>
        /// <returns>Count of operations</returns>
        [HttpGet("count/{operation}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> GetOperationCount(string operation)
        {
            if (Enum.TryParse<OperationType>(operation, true, out var operationType))
            {
                var count = await _service.GetOperationCount(operationType);
                return Ok(count);
            }
            
            return BadRequest($"Invalid operation type: {operation}");
        }

        /// <summary>
        /// Get error history
        /// </summary>
        /// <returns>List of failed operations</returns>
        [HttpGet("history/errors")]
        [ProducesResponseType(typeof(List<QuantityMeasurementDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<QuantityMeasurementDTO>>> GetErrorHistory()
        {
            var errors = await _service.GetErrorHistory();
            return Ok(errors);
        }
    }

    // Request DTOs
    public class CompareRequest
    {
        public QuantityInputDTO First { get; set; } = new();
        public QuantityInputDTO Second { get; set; } = new();
    }

    public class ConvertRequest
    {
        public QuantityInputDTO Source { get; set; } = new();
        public QuantityInputDTO Target { get; set; } = new();
    }

    public class AddRequest
    {
        public QuantityInputDTO First { get; set; } = new();
        public QuantityInputDTO Second { get; set; } = new();
        public string? ResultUnit { get; set; }
    }

    public class SubtractRequest
    {
        public QuantityInputDTO First { get; set; } = new();
        public QuantityInputDTO Second { get; set; } = new();
        public string? ResultUnit { get; set; }
    }

    public class DivideRequest
    {
        public QuantityInputDTO First { get; set; } = new();
        public QuantityInputDTO Second { get; set; } = new();
    }
}