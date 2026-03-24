using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ModelLayer.DTOs;
using ModelLayer.Enums;
using BusinessLayer.Interfaces;

namespace QuantityMeasurementApp.API.Controllers
{
    [Authorize]  // . ALL APIs require authentication
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

        //  2. Helper method to get current user ID from token
        private int GetCurrentUserId()
        {
            var user = ControllerContext?.HttpContext?.User;
            var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out var userId))
                return userId;

            // Fallback for unit tests or anonymous requests
            return 0;
        }

        /// <summary>
        /// Compare two quantities with unit conversion
        /// </summary>
        /// <remarks>
        /// Compares two quantities by first converting them to a common base unit before comparison.
        /// Supports length, weight, volume, and temperature measurements. The comparison result
        /// indicates which quantity is larger, smaller, or if they are equal after unit conversion.
        /// 
        /// **Example Request:**
        /// ```
        /// POST /api/v1/QuantityMeasurement/compare
        /// {
        ///   "first": {
        ///     "value": 100,
        ///     "unit": "cm",
        ///     "measurementType": "length"
        ///   },
        ///   "second": {
        ///     "value": 1,
        ///     "unit": "m",
        ///     "measurementType": "length"
        ///   }
        /// }
        /// ```
        /// </remarks>
        /// <param name="request">The comparison request containing two quantities to compare</param>
        /// <response code="200">Returns the comparison result with equality status and converted values</response>
        /// <response code="400">Returns error details if quantities cannot be compared (invalid units/types)</response>
        /// <response code="401">Unauthorized - Valid JWT token required</response>
        [HttpPost("compare")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<QuantityMeasurementDTO>> CompareQuantities(
            [FromBody] CompareRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();  //  3. Get user ID
                _logger.LogInformation("User {UserId} comparing quantities: {First} vs {Second}", 
                    userId, request.First, request.Second);
                
                var result = await _service.CompareQuantities(
                    request.First, 
                    request.Second, 
                    userId);  //  4. Pass userId
                
                if (result.IsError)
                    return BadRequest(result);
                    
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Convert a quantity to a different unit
        /// </summary>
        /// <remarks>
        /// Converts a quantity from one unit to another within the same measurement type.
        /// The conversion maintains mathematical accuracy and handles different unit systems
        /// (metric, imperial, etc.). Temperature conversions account for absolute zero differences.
        /// 
        /// **Supported Conversions:**
        /// - Length: mm, cm, m, km, inch, foot, yard, mile
        /// - Weight: mg, g, kg, tonne, oz, lb, stone
        /// - Volume: ml, l, gallon, quart, pint, cup, tbsp, tsp
        /// - Temperature: Celsius, Fahrenheit, Kelvin
        /// 
        /// **Example Request:**
        /// ```
        /// POST /api/v1/QuantityMeasurement/convert
        /// {
        ///   "source": {
        ///     "value": 100,
        ///     "unit": "cm",
        ///     "measurementType": "length"
        ///   },
        ///   "target": {
        ///     "value": 0,
        ///     "unit": "inch",
        ///     "measurementType": "length"
        ///   }
        /// }
        /// ```
        /// </remarks>
        /// <param name="request">The conversion request with source and target quantity specifications</param>
        /// <response code="200">Returns the converted quantity with the new value and unit</response>
        /// <response code="400">Returns error details if conversion fails (invalid units/types)</response>
        /// <response code="401">Unauthorized - Valid JWT token required</response>
        [HttpPost("convert")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<QuantityMeasurementDTO>> ConvertQuantity(
            [FromBody] ConvertRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _service.ConvertQuantity(
                    request.Source, 
                    request.Target, 
                    userId);
                
                if (result.IsError)
                    return BadRequest(result);
                    
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Add two quantities with unit normalization
        /// </summary>
        /// <remarks>
        /// Adds two quantities by first converting them to a common base unit, performing the addition,
        /// and then converting the result to the specified result unit. If no result unit is specified,
        /// the result uses the unit of the first quantity.
        /// 
        /// **Process:**
        /// 1. Convert both quantities to base units
        /// 2. Add the base unit values
        /// 3. Convert result back to desired unit
        /// 
        /// **Example Request:**
        /// ```
        /// POST /api/v1/QuantityMeasurement/add
        /// {
        ///   "first": {
        ///     "value": 50,
        ///     "unit": "cm",
        ///     "measurementType": "length"
        ///   },
        ///   "second": {
        ///     "value": 2,
        ///     "unit": "m",
        ///     "measurementType": "length"
        ///   },
        ///   "resultUnit": "inch"
        /// }
        /// ```
        /// </remarks>
        /// <param name="request">The addition request with two quantities and optional result unit</param>
        /// <response code="200">Returns the sum of the quantities in the specified result unit</response>
        /// <response code="400">Returns error details if addition fails (invalid units/types)</response>
        /// <response code="401">Unauthorized - Valid JWT token required</response>
        [HttpPost("add")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<QuantityMeasurementDTO>> AddQuantities(
            [FromBody] AddRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _service.AddQuantities(
                    request.First, 
                    request.Second, 
                    request.ResultUnit,
                    userId);
                
                if (result.IsError)
                    return BadRequest(result);
                    
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Subtract two quantities with unit conversion
        /// </summary>
        /// <remarks>
        /// Subtracts the second quantity from the first by converting both to a common base unit,
        /// performing the subtraction, and converting the result to the specified result unit.
        /// If no result unit is specified, the result uses the unit of the first quantity.
        /// 
        /// **Process:**
        /// 1. Convert both quantities to base units
        /// 2. Subtract second from first (base unit values)
        /// 3. Convert result back to desired unit
        /// 
        /// **Example Request:**
        /// ```
        /// POST /api/v1/QuantityMeasurement/subtract
        /// {
        ///   "first": {
        ///     "value": 2,
        ///     "unit": "m",
        ///     "measurementType": "length"
        ///   },
        ///   "second": {
        ///     "value": 50,
        ///     "unit": "cm",
        ///     "measurementType": "length"
        ///   },
        ///   "resultUnit": "inch"
        /// }
        /// ```
        /// </remarks>
        /// <param name="request">The subtraction request with two quantities and optional result unit</param>
        /// <response code="200">Returns the difference of the quantities in the specified result unit</response>
        /// <response code="400">Returns error details if subtraction fails (invalid units/types)</response>
        /// <response code="401">Unauthorized - Valid JWT token required</response>
        [HttpPost("subtract")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<QuantityMeasurementDTO>> SubtractQuantities(
            [FromBody] SubtractRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _service.SubtractQuantities(
                    request.First, 
                    request.Second, 
                    request.ResultUnit,
                    userId);
                
                if (result.IsError)
                    return BadRequest(result);
                    
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Divide two quantities with division-by-zero handling
        /// </summary>
        /// <remarks>
        /// Divides the first quantity by the second by converting both to a common base unit,
        /// performing the division, and returning the quotient as a dimensionless result.
        /// Includes protection against division by zero.
        /// 
        /// **Process:**
        /// 1. Convert both quantities to base units
        /// 2. Check for division by zero (second quantity = 0)
        /// 3. Divide first by second (base unit values)
        /// 4. Return dimensionless quotient
        /// 
        /// **Example Request:**
        /// ```
        /// POST /api/v1/QuantityMeasurement/divide
        /// {
        ///   "first": {
        ///     "value": 1,
        ///     "unit": "m",
        ///     "measurementType": "length"
        ///   },
        ///   "second": {
        ///     "value": 50,
        ///     "unit": "cm",
        ///     "measurementType": "length"
        ///   }
        /// }
        /// ```
        /// 
        /// **Note:** Result is always dimensionless (no unit) since we're dividing same-type quantities.
        /// </remarks>
        /// <param name="request">The division request with two quantities</param>
        /// <response code="200">Returns the quotient as a dimensionless value</response>
        /// <response code="400">Returns error details if division fails (division by zero, invalid units/types)</response>
        /// <response code="401">Unauthorized - Valid JWT token required</response>
        [HttpPost("divide")]
        [ProducesResponseType(typeof(QuantityMeasurementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<QuantityMeasurementDTO>> DivideQuantities(
            [FromBody] DivideRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _service.DivideQuantities(
                    request.First, 
                    request.Second, 
                    userId);
                
                if (result.IsError)
                    return BadRequest(result);
                    
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Get current user's operation history
        /// </summary>
        /// <remarks>
        /// Retrieves all quantity measurement operations performed by the currently authenticated user.
        /// Results are cached for 5 minutes to improve performance. Includes all operation types:
        /// compare, convert, add, subtract, and divide operations.
        /// 
        /// **Response includes:**
        /// - Operation timestamp
        /// - Operation type (compare/convert/add/subtract/divide)
        /// - Input quantities and units
        /// - Result values and units
        /// - Success/error status
        /// 
        /// **Caching:** Results are cached for 5 minutes to reduce database load for frequently accessed user history.
        /// </remarks>
        /// <response code="200">Returns list of user's operation history</response>
        /// <response code="401">Unauthorized - Valid JWT token required</response>
        [HttpGet("my-operations")]
        [ProducesResponseType(typeof(List<QuantityMeasurementDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<QuantityMeasurementDTO>>> GetMyOperations()
        {
            try
            {
                var userId = GetCurrentUserId();
                var operations = await _service.GetUserOperationsAsync(userId);  // 👈 5. New method
                return Ok(operations);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Get operation history by operation type (user-specific)
        /// </summary>
        /// <remarks>
        /// Retrieves quantity measurement operations of a specific type performed by the currently authenticated user.
        /// Supports filtering by operation type: compare, convert, add, subtract, divide.
        /// 
        /// **Supported Operation Types:**
        /// - compare: Quantity comparison operations
        /// - convert: Unit conversion operations
        /// - add: Quantity addition operations
        /// - subtract: Quantity subtraction operations
        /// - divide: Quantity division operations
        /// 
        /// **Example:** GET /api/v1/QuantityMeasurement/my-operations/convert
        /// 
        /// **Note:** This endpoint filters the user's operations by type but does not use caching
        /// as it requires real-time filtering. For full user history, use /my-operations.
        /// </remarks>
        /// <param name="operation">The operation type to filter by (compare/convert/add/subtract/divide)</param>
        /// <response code="200">Returns filtered list of user's operations by type</response>
        /// <response code="400">Invalid operation type specified</response>
        /// <response code="401">Unauthorized - Valid JWT token required</response>
        [HttpGet("my-operations/{operation}")]
        [ProducesResponseType(typeof(List<QuantityMeasurementDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<QuantityMeasurementDTO>>> GetMyOperationsByType(string operation)
        {
            try
            {
                if (!Enum.TryParse<OperationType>(operation, true, out var operationType))
                    return BadRequest($"Invalid operation type: {operation}");

                var userId = GetCurrentUserId();
                var history = await _service.GetOperationHistory(operationType);
                // Filter by user if needed, or modify service to be user-specific
                return Ok(history);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Get operation history by type (Admin only)
        /// </summary>
        /// <remarks>
        /// **ADMIN ENDPOINT:** Retrieves all operations of a specific type across all users.
        /// Requires Admin or User role. Results are cached for 10 minutes.
        /// 
        /// **Supported Operation Types:**
        /// - compare: All quantity comparison operations
        /// - convert: All unit conversion operations
        /// - add: All quantity addition operations
        /// - subtract: All quantity subtraction operations
        /// - divide: All quantity division operations
        /// 
        /// **Example:** GET /api/v1/QuantityMeasurement/history/operation/convert
        /// 
        /// **Caching:** Results are cached for 10 minutes to improve performance for frequently accessed operation types.
        /// **Authorization:** Requires Admin role.
        /// </remarks>
        /// <param name="operation">The operation type to retrieve (compare/convert/add/subtract/divide)</param>
        /// <response code="200">Returns all operations of the specified type</response>
        /// <response code="400">Invalid operation type specified</response>
        /// <response code="401">Unauthorized - Valid JWT token required</response>
        /// <response code="403">Forbidden - Admin role required</response>
        [Authorize(Roles = "Admin,User")]  //  6. Admin-only endpoint
        [HttpGet("history/operation/{operation}")]
        [ProducesResponseType(typeof(List<QuantityMeasurementDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<QuantityMeasurementDTO>>> GetOperationHistory(string operation)
        {
            if (!Enum.TryParse<OperationType>(operation, true, out var operationType))
                return BadRequest($"Invalid operation type: {operation}");

            var history = await _service.GetOperationHistory(operationType);
            return Ok(history);
        }

        /// <summary>
        /// Get measurement history by type
        /// </summary>
        /// <remarks>
        /// Retrieves all operations for a specific measurement type (length, weight, volume, temperature).
        /// Results are cached for 10 minutes to improve performance.
        /// 
        /// **Supported Measurement Types:**
        /// - length: Length/distance measurements
        /// - weight: Mass/weight measurements
        /// - volume: Volume/capacity measurements
        /// - temperature: Temperature measurements
        /// 
        /// **Example:** GET /api/v1/QuantityMeasurement/history/type/length
        /// 
        /// **Caching:** Results are cached for 10 minutes to reduce database load for frequently accessed measurement types.
        /// </remarks>
        /// <param name="type">The measurement type to filter by (length/weight/volume/temperature)</param>
        /// <response code="200">Returns all operations for the specified measurement type</response>
        /// <response code="401">Unauthorized - Valid JWT token required</response>
        [HttpGet("history/type/{type}")]
        [ProducesResponseType(typeof(List<QuantityMeasurementDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<QuantityMeasurementDTO>>> GetMeasurementTypeHistory(string type)
        {
            var history = await _service.GetMeasurementTypeHistory(type);
            return Ok(history);
        }

        /// <summary>
        /// Get operation count by type
        /// </summary>
        /// <remarks>
        /// Returns the total count of operations performed for a specific operation type.
        /// Useful for analytics and monitoring system usage.
        /// 
        /// **Supported Operation Types:**
        /// - compare: Quantity comparison operations
        /// - convert: Unit conversion operations
        /// - add: Quantity addition operations
        /// - subtract: Quantity subtraction operations
        /// - divide: Quantity division operations
        /// 
        /// **Example:** GET /api/v1/QuantityMeasurement/count/convert
        /// </remarks>
        /// <param name="operation">The operation type to count (compare/convert/add/subtract/divide)</param>
        /// <response code="200">Returns the count of operations for the specified type</response>
        /// <response code="400">Invalid operation type specified</response>
        /// <response code="401">Unauthorized - Valid JWT token required</response>
        [HttpGet("count/{operation}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> GetOperationCount(string operation)
        {
            if (!Enum.TryParse<OperationType>(operation, true, out var operationType))
                return BadRequest($"Invalid operation type: {operation}");

            var count = await _service.GetOperationCount(operationType);
            return Ok(count);
        }

        /// <summary>
        /// Get error operation history
        /// </summary>
        /// <remarks>
        /// Retrieves all operations that resulted in errors. Useful for debugging and monitoring
        /// system issues. Currently returns an empty list as error tracking is not fully implemented.
        /// 
        /// **Future Implementation:**
        /// - Track operations that failed due to invalid inputs
        /// - Log conversion errors
        /// - Monitor division by zero attempts
        /// - Track authentication failures
        /// </remarks>
        /// <response code="200">Returns list of error operations (currently empty)</response>
        /// <response code="401">Unauthorized - Valid JWT token required</response>
        [HttpGet("history/errors")]
        [ProducesResponseType(typeof(List<QuantityMeasurementDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<QuantityMeasurementDTO>>> GetErrorHistory()
        {
            var errors = await _service.GetErrorHistory();
            return Ok(errors);
        }
    }

    // Request DTOs (keep as is)
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
