namespace QuantityService.Core.DTOs
{
    // Base Quantity DTO
    public class QuantityDto
    {
        public double Value { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string UnitType { get; set; } = string.Empty;
    }

    // Add Request
    public class AddRequest
    {
        public QuantityDto Quantity1 { get; set; } = new();
        public QuantityDto Quantity2 { get; set; } = new();
    }

    // Subtract Request
    public class SubtractRequest
    {
        public QuantityDto Quantity1 { get; set; } = new();
        public QuantityDto Quantity2 { get; set; } = new();
    }

    // Multiply Request
    public class MultiplyRequest
    {
        public QuantityDto Quantity { get; set; } = new();
        public double Factor { get; set; }
    }

    // Divide Request
    public class DivideRequest
    {
        public QuantityDto Quantity { get; set; } = new();
        public double Divisor { get; set; }
    }

    // Convert Request
    public class ConvertRequest
    {
        public QuantityDto Quantity { get; set; } = new();
        public string TargetUnit { get; set; } = string.Empty;
    }

    // Compare Request
    public class CompareRequest
    {
        public QuantityDto Quantity1 { get; set; } = new();
        public QuantityDto Quantity2 { get; set; } = new();
    }

    // API Response
    public class QuantityResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public double? Result { get; set; }
        public string? Unit { get; set; }
        public int? Comparison { get; set; } // -1: less, 0: equal, 1: greater
        public string? ErrorCode { get; set; }
    }
}