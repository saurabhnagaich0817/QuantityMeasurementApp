namespace QuantityMeasurement.Business.DTOs;

public class QuantityPairRequest
{
    public QuantityRequest Q1 { get; set; } = default!;

    public QuantityRequest Q2 { get; set; } = default!;
}