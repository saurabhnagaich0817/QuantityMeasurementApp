namespace QuantityMeasurement.Business.DTOs;

public class QuantityResponse
{
    public double Result { get; set; }

    public string Unit { get; set; } = default!;
}