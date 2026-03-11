using QuantityMeasurement.Business.DTOs;

namespace QuantityMeasurement.Business.Services;

public interface IQuantityService
{
    QuantityResponse Add(QuantityPairRequest request);

    bool Compare(QuantityPairRequest request);

    double ConvertTemperature(double value, string fromUnit, string toUnit);
}