using System;
using ModelLayer.Models;
using ModelLayer.Enums;
using ModelLayer.Interfaces;
using ModelLayer.DTOs;
using RepoLayer.Interfaces;
using RepoLayer.Repositories;
using BusinessLayer.Interfaces;

namespace BusinessLayer.Services
{
    public class QuantityMeasurementService : IQuantityMeasurementService
    {
        private readonly IQuantityRepository repository;

        public QuantityMeasurementService()
        {
            repository = new QuantityRepository();
        }

        private IUnitConverter<T> ResolveConverter<T>() where T : struct, Enum
        {
            if (typeof(T) == typeof(LengthUnit))
                return (IUnitConverter<T>)(object)new LengthUnitConverter();

            if (typeof(T) == typeof(WeightUnit))
                return (IUnitConverter<T>)(object)new WeightUnitConverter();

            if (typeof(T) == typeof(VolumeUnit))
                return (IUnitConverter<T>)(object)new VolumeUnitConverter();

            if (typeof(T) == typeof(TemperatureUnit))
                return (IUnitConverter<T>)(object)new TemperatureUnitConverter();

            throw new NotSupportedException($"Unsupported unit type {typeof(T).Name}");
        }

        private QuantityResultDto MapQuantityToDto<T>(Quantity<T> quantity) where T : struct, Enum
        {
            return new QuantityResultDto
            {
                Value = quantity.Value,
                UnitSymbol = quantity.ToString().Split(' ')[1]
            };
        }

        public ComparisonResultDto Compare<U>(Quantity<U> firstQuantity, Quantity<U> secondQuantity) where U : struct, Enum
        {
            if (firstQuantity == null || secondQuantity == null)
                return new ComparisonResultDto { AreEqual = false };

            return new ComparisonResultDto
            {
                AreEqual = firstQuantity.Equals(secondQuantity)
            };
        }

        public QuantityResultDto DemonstrateConversion<U>(double numericValue, U sourceType, U targetType) where U : struct, Enum
        {
            var converter = ResolveConverter<U>();

            Quantity<U> tempQuantity = new Quantity<U>(numericValue, sourceType, converter);
            Quantity<U> converted = tempQuantity.ConvertTo(targetType);

            return MapQuantityToDto(converted);
        }

        public QuantityResultDto DemonstrateConversion<U>(Quantity<U> originalQuantity, U desiredUnit) where U : struct, Enum
        {
            Quantity<U> resultQuantity = originalQuantity.ConvertTo(desiredUnit);

            return MapQuantityToDto(resultQuantity);
        }

        public QuantityResultDto DemonstrateAddition<U>(Quantity<U> leftOperand, Quantity<U> rightOperand) where U : struct, Enum
        {
            Quantity<U> sum = leftOperand.Add(rightOperand);

            return MapQuantityToDto(sum);
        }

        public QuantityResultDto DemonstrateAddition<U>(Quantity<U> leftOperand, Quantity<U> rightOperand, U resultUnit) where U : struct, Enum
        {
            Quantity<U> result = leftOperand.Add(rightOperand, resultUnit);

            return MapQuantityToDto(result);
        }

        public QuantityResultDto Subtract<U>(Quantity<U> firstValue, Quantity<U> secondValue, U resultUnit) where U : struct, Enum
        {
            Quantity<U> difference = firstValue.Subtract(secondValue, resultUnit);

            return MapQuantityToDto(difference);
        }

        public DivisionResultDto Divide<T>(double firstValue, T firstUnit, double secondValue, T secondUnit) where T : struct, Enum
        {
            var converter = ResolveConverter<T>();

            Quantity<T> dividend = new Quantity<T>(firstValue, firstUnit, converter);
            Quantity<T> divisor = new Quantity<T>(secondValue, secondUnit, converter);

            double outcome = dividend.Divide(divisor);

            return new DivisionResultDto
            {
                Ratio = outcome
            };
        }
    }
}