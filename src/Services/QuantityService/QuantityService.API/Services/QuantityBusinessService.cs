using QuantityService.Core.DTOs;
using QuantityService.Core.Helpers;
using QuantityService.Core.Interfaces;

namespace QuantityService.API.Services
{
    public class QuantityBusinessService : IQuantityBusinessService
    {
        private readonly IQuantityRepository _repository;

        public QuantityBusinessService(IQuantityRepository repository)
        {
            _repository = repository;
        }

        public async Task<QuantityResponse> AddAsync(AddRequest request)
        {
            try
            {
                // Detect unit types
                var type1 = UnitConverter.DetectUnitType(request.Quantity1.Unit);
                var type2 = UnitConverter.DetectUnitType(request.Quantity2.Unit);

                // Check if units are compatible
                if (type1 != type2)
                {
                    return new QuantityResponse
                    {
                        Success = false,
                        Message = $"Cannot add {type1} and {type2}. Units are incompatible.",
                        ErrorCode = "INCOMPATIBLE_UNITS"
                    };
                }

                // Convert both to base units
                var baseValue1 = UnitConverter.ToBaseUnit(request.Quantity1.Value, request.Quantity1.Unit, type1);
                var baseValue2 = UnitConverter.ToBaseUnit(request.Quantity2.Value, request.Quantity2.Unit, type2);

                // Add
                var resultBase = baseValue1 + baseValue2;

                // Convert back to original unit of first quantity
                var result = UnitConverter.FromBaseUnit(resultBase, request.Quantity1.Unit, type1);

                return new QuantityResponse
                {
                    Success = true,
                    Message = "Addition successful",
                    Result = Math.Round(result, 4),
                    Unit = request.Quantity1.Unit
                };
            }
            catch (Exception ex)
            {
                return new QuantityResponse
                {
                    Success = false,
                    Message = $"Addition failed: {ex.Message}",
                    ErrorCode = "ADDITION_ERROR"
                };
            }
        }

        public async Task<QuantityResponse> SubtractAsync(SubtractRequest request)
        {
            try
            {
                var type1 = UnitConverter.DetectUnitType(request.Quantity1.Unit);
                var type2 = UnitConverter.DetectUnitType(request.Quantity2.Unit);

                if (type1 != type2)
                {
                    return new QuantityResponse
                    {
                        Success = false,
                        Message = $"Cannot subtract {type2} from {type1}. Units are incompatible.",
                        ErrorCode = "INCOMPATIBLE_UNITS"
                    };
                }

                var baseValue1 = UnitConverter.ToBaseUnit(request.Quantity1.Value, request.Quantity1.Unit, type1);
                var baseValue2 = UnitConverter.ToBaseUnit(request.Quantity2.Value, request.Quantity2.Unit, type2);

                var resultBase = baseValue1 - baseValue2;
                var result = UnitConverter.FromBaseUnit(resultBase, request.Quantity1.Unit, type1);

                return new QuantityResponse
                {
                    Success = true,
                    Message = "Subtraction successful",
                    Result = Math.Round(result, 4),
                    Unit = request.Quantity1.Unit
                };
            }
            catch (Exception ex)
            {
                return new QuantityResponse
                {
                    Success = false,
                    Message = $"Subtraction failed: {ex.Message}",
                    ErrorCode = "SUBTRACTION_ERROR"
                };
            }
        }

        public async Task<QuantityResponse> MultiplyAsync(MultiplyRequest request)
        {
            try
            {
                var type = UnitConverter.DetectUnitType(request.Quantity.Unit);
                var baseValue = UnitConverter.ToBaseUnit(request.Quantity.Value, request.Quantity.Unit, type);

                var resultBase = baseValue * request.Factor;
                var result = UnitConverter.FromBaseUnit(resultBase, request.Quantity.Unit, type);

                return new QuantityResponse
                {
                    Success = true,
                    Message = "Multiplication successful",
                    Result = Math.Round(result, 4),
                    Unit = request.Quantity.Unit
                };
            }
            catch (Exception ex)
            {
                return new QuantityResponse
                {
                    Success = false,
                    Message = $"Multiplication failed: {ex.Message}",
                    ErrorCode = "MULTIPLICATION_ERROR"
                };
            }
        }

        public async Task<QuantityResponse> DivideAsync(DivideRequest request)
        {
            try
            {
                if (request.Divisor == 0)
                {
                    return new QuantityResponse
                    {
                        Success = false,
                        Message = "Cannot divide by zero",
                        ErrorCode = "DIVIDE_BY_ZERO"
                    };
                }

                var type = UnitConverter.DetectUnitType(request.Quantity.Unit);
                var baseValue = UnitConverter.ToBaseUnit(request.Quantity.Value, request.Quantity.Unit, type);

                var resultBase = baseValue / request.Divisor;
                var result = UnitConverter.FromBaseUnit(resultBase, request.Quantity.Unit, type);

                return new QuantityResponse
                {
                    Success = true,
                    Message = "Division successful",
                    Result = Math.Round(result, 4),
                    Unit = request.Quantity.Unit
                };
            }
            catch (Exception ex)
            {
                return new QuantityResponse
                {
                    Success = false,
                    Message = $"Division failed: {ex.Message}",
                    ErrorCode = "DIVISION_ERROR"
                };
            }
        }

        public async Task<QuantityResponse> ConvertAsync(ConvertRequest request)
        {
            try
            {
                var type = UnitConverter.DetectUnitType(request.Quantity.Unit);
                var targetType = UnitConverter.DetectUnitType(request.TargetUnit);

                if (type != targetType)
                {
                    return new QuantityResponse
                    {
                        Success = false,
                        Message = $"Cannot convert from {type} to {targetType}. Different unit types.",
                        ErrorCode = "INCOMPATIBLE_CONVERSION"
                    };
                }

                var baseValue = UnitConverter.ToBaseUnit(request.Quantity.Value, request.Quantity.Unit, type);
                var result = UnitConverter.FromBaseUnit(baseValue, request.TargetUnit, type);

                return new QuantityResponse
                {
                    Success = true,
                    Message = "Conversion successful",
                    Result = Math.Round(result, 4),
                    Unit = request.TargetUnit
                };
            }
            catch (Exception ex)
            {
                return new QuantityResponse
                {
                    Success = false,
                    Message = $"Conversion failed: {ex.Message}",
                    ErrorCode = "CONVERSION_ERROR"
                };
            }
        }

        public async Task<QuantityResponse> CompareAsync(CompareRequest request)
        {
            try
            {
                var type1 = UnitConverter.DetectUnitType(request.Quantity1.Unit);
                var type2 = UnitConverter.DetectUnitType(request.Quantity2.Unit);

                if (type1 != type2)
                {
                    return new QuantityResponse
                    {
                        Success = false,
                        Message = $"Cannot compare {type1} and {type2}. Different unit types.",
                        ErrorCode = "INCOMPATIBLE_COMPARISON"
                    };
                }

                var baseValue1 = UnitConverter.ToBaseUnit(request.Quantity1.Value, request.Quantity1.Unit, type1);
                var baseValue2 = UnitConverter.ToBaseUnit(request.Quantity2.Value, request.Quantity2.Unit, type2);

                int comparison;
                if (baseValue1 < baseValue2) comparison = -1;
                else if (baseValue1 > baseValue2) comparison = 1;
                else comparison = 0;

                string message = comparison switch
                {
                    -1 => $"{request.Quantity1.Value} {request.Quantity1.Unit} is less than {request.Quantity2.Value} {request.Quantity2.Unit}",
                    0 => $"{request.Quantity1.Value} {request.Quantity1.Unit} is equal to {request.Quantity2.Value} {request.Quantity2.Unit}",
                    1 => $"{request.Quantity1.Value} {request.Quantity1.Unit} is greater than {request.Quantity2.Value} {request.Quantity2.Unit}",
                    _ => "Comparison completed"
                };

                return new QuantityResponse
                {
                    Success = true,
                    Message = message,
                    Comparison = comparison
                };
            }
            catch (Exception ex)
            {
                return new QuantityResponse
                {
                    Success = false,
                    Message = $"Comparison failed: {ex.Message}",
                    ErrorCode = "COMPARISON_ERROR"
                };
            }
        }
    }
}