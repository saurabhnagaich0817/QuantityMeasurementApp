using QuantityMeasurement.Business.DTOs;
using QuantityMeasurement.Repository.Interfaces;
using System.Linq;

namespace QuantityMeasurement.Business.Services;

public class QuantityService : IQuantityService
{
    private readonly IQuantityRepository repository;

    public QuantityService(IQuantityRepository repo)
    {
        repository = repo;
    }

  public QuantityResponse Add(QuantityPairRequest request)
{
    string unit = request.Q1.Unit.ToLower();

    string[] lengthUnits = { "meter", "centimeter", "kilometer", "feet", "inch" };
    string[] weightUnits = { "kg", "gram" };
    string[] volumeUnits = { "liter", "ml" };

    if (!lengthUnits.Contains(unit) &&
        !weightUnits.Contains(unit) &&
        !volumeUnits.Contains(unit))
    {
        throw new Exception("Invalid unit provided");
    }

    double result = request.Q1.Value + request.Q2.Value;

    repository.SaveResult(request.Q1.Value, request.Q2.Value, "+", result);

    return new QuantityResponse
    {
        Result = result,
        Unit = request.Q1.Unit
    };
}
    public double ConvertTemperature(double value, string fromUnit, string toUnit)
{
    if (fromUnit.ToLower() == "celsius" && toUnit.ToLower() == "fahrenheit")
        return (value * 9 / 5) + 32;

    if (fromUnit.ToLower() == "fahrenheit" && toUnit.ToLower() == "celsius")
        return (value - 32) * 5 / 9;

    return value;
}

   public bool Compare(QuantityPairRequest request)
{
    if (request.Q1.Unit != request.Q2.Unit)
        throw new Exception("Units must be same for comparison");

    return request.Q1.Value == request.Q2.Value;
}
}