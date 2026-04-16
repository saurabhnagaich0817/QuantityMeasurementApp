using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuantityService.Core.DTOs;
using QuantityService.Core.Interfaces;

namespace QuantityService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // JWT authentication required
    public class QuantityController : ControllerBase
    {
        private readonly IQuantityBusinessService _quantityService;

        public QuantityController(IQuantityBusinessService quantityService)
        {
            _quantityService = quantityService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<QuantityResponse>> Add([FromBody] AddRequest request)
        {
            var result = await _quantityService.AddAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("subtract")]
        public async Task<ActionResult<QuantityResponse>> Subtract([FromBody] SubtractRequest request)
        {
            var result = await _quantityService.SubtractAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("multiply")]
        public async Task<ActionResult<QuantityResponse>> Multiply([FromBody] MultiplyRequest request)
        {
            var result = await _quantityService.MultiplyAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("divide")]
        public async Task<ActionResult<QuantityResponse>> Divide([FromBody] DivideRequest request)
        {
            var result = await _quantityService.DivideAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("convert")]
        public async Task<ActionResult<QuantityResponse>> Convert([FromBody] ConvertRequest request)
        {
            var result = await _quantityService.ConvertAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("compare")]
        public async Task<ActionResult<QuantityResponse>> Compare([FromBody] CompareRequest request)
        {
            var result = await _quantityService.CompareAsync(request);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}