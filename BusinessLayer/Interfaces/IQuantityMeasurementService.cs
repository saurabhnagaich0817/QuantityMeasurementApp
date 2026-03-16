using System;
using ModelLayer.Models;
using ModelLayer.DTOs;

namespace BusinessLayer.Interfaces
{
    /// <summary>
    /// Defines operations for working with Quantity objects.
    /// </summary>
    public interface IQuantityMeasurementService
    {
        ComparisonResultDto Compare<U>(Quantity<U> firstQuantity, Quantity<U> secondQuantity)
            where U : struct, Enum;

        QuantityResultDto DemonstrateConversion<U>(double numericValue, U sourceType, U targetType)
            where U : struct, Enum;

        QuantityResultDto DemonstrateConversion<U>(Quantity<U> originalQuantity, U desiredUnit)
            where U : struct, Enum;

        QuantityResultDto DemonstrateAddition<U>(Quantity<U> leftOperand, Quantity<U> rightOperand)
            where U : struct, Enum;

        QuantityResultDto DemonstrateAddition<U>(Quantity<U> leftOperand, Quantity<U> rightOperand, U resultUnit)
            where U : struct, Enum;

        QuantityResultDto Subtract<U>(Quantity<U> firstValue, Quantity<U> secondValue, U resultUnit)
            where U : struct, Enum;

        DivisionResultDto Divide<T>(double firstValue, T firstUnit, double secondValue, T secondUnit)
            where T : struct, Enum;
    }
}