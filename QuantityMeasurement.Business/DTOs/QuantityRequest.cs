namespace QuantityMeasurement.Business.DTOs;

public class QuantityRequest
{
    public double Value { get; set; }

    public string Unit { get; set; }

    public string MeasurementType { get; set; }
}