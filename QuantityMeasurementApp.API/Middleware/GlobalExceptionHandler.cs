using System.Net;
using System.Text.Json;
using ModelLayer.Exceptions;

namespace QuantityMeasurementApp.API.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Timestamp = DateTime.UtcNow,
                Path = context.Request.Path
            };

            switch (exception)
            {
                case QuantityMeasurementException qmEx:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.StatusCode = response.StatusCode;
                    errorResponse.Error = "Quantity Measurement Error";
                    errorResponse.Message = qmEx.Message;
                    break;

                case DatabaseException dbEx:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.StatusCode = response.StatusCode;
                    errorResponse.Error = "Database Error";
                    errorResponse.Message = dbEx.Message;
                    errorResponse.Details = $"Error Code: {dbEx.ErrorCode}";
                    break;

                case ArgumentException argEx:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.StatusCode = response.StatusCode;
                    errorResponse.Error = "Invalid Argument";
                    errorResponse.Message = argEx.Message;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.StatusCode = response.StatusCode;
                    errorResponse.Error = "Internal Server Error";
                    errorResponse.Message = "An unexpected error occurred. Please try again later.";
                    errorResponse.Details = exception.Message; // In production, don't show this
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await response.WriteAsync(jsonResponse);
        }
    }

    public class ErrorResponse
    {
        public DateTime Timestamp { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }
        public string? Message { get; set; }
        public string? Details { get; set; }
        public string? Path { get; set; }
    }
}