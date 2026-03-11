namespace QuantityMeasurement.Repository.Interfaces;

public interface IQuantityRepository
{
    void SaveResult(double operand1, double operand2, string operation, double result);
} 