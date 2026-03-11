using QuantityMeasurement.Repository.Interfaces;

namespace QuantityMeasurement.Repository;

public class QuantityRepository : IQuantityRepository
{
    public void SaveResult(double operand1, double operand2, string operation, double result)
    {
        Console.WriteLine($"Saved: {operand1} {operation} {operand2} = {result}");
    }
}