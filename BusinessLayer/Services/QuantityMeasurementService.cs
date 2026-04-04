#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ModelLayer.Entities;
using ModelLayer.Enums;
using ModelLayer.Interfaces;
using ModelLayer.DTOs;
using ModelLayer.Exceptions;
using RepoLayer.Interfaces;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Services
{
    public class QuantityMeasurementService : IQuantityMeasurementService
    {
        private readonly IQuantityRepository _repository;
        private readonly LengthUnitConverter _lengthConverter;
        private readonly WeightUnitConverter _weightConverter;
        private readonly VolumeUnitConverter _volumeConverter;
        private readonly TemperatureUnitConverter _temperatureConverter;
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QuantityMeasurementService(
            IQuantityRepository repository,
            LengthUnitConverter lengthConverter,
            WeightUnitConverter weightConverter,
            VolumeUnitConverter volumeConverter,
            TemperatureUnitConverter temperatureConverter,
            IMemoryCache cache,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _lengthConverter = lengthConverter;
            _weightConverter = weightConverter;
            _volumeConverter = volumeConverter;
            _temperatureConverter = temperatureConverter;
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }

        private IUnitConverter<T> GetConverter<T>() where T : struct, Enum
        {
            if (typeof(T) == typeof(LengthUnit))
                return (IUnitConverter<T>)_lengthConverter;
            if (typeof(T) == typeof(WeightUnit))
                return (IUnitConverter<T>)_weightConverter;
            if (typeof(T) == typeof(VolumeUnit))
                return (IUnitConverter<T>)_volumeConverter;
            if (typeof(T) == typeof(TemperatureUnit))
                return (IUnitConverter<T>)_temperatureConverter;
            
            throw new NotSupportedException($"Unsupported type {typeof(T).Name}");
        }

       private object ParseUnit(string unitName, string measurementType)
{
    return measurementType.ToLower() switch
    {
        "length" => unitName.ToLower() switch
        {
            "mm" => LengthUnit.mm,
            "cm" => LengthUnit.cm,
            "m" => LengthUnit.m,
            "km" => LengthUnit.km,
            "inch" or "inches" => LengthUnit.inch,
            "foot" or "feet" or "ft" => LengthUnit.ft,
            "yard" or "yards" or "yd" => LengthUnit.yd,
            "mile" or "miles" => LengthUnit.mile,
            _ => Enum.Parse<LengthUnit>(unitName, true)
        },
        "weight" => unitName.ToLower() switch
        {
            "mg" => WeightUnit.mg,
            "g" => WeightUnit.g,
            "kg" => WeightUnit.kg,
            "tonne" => WeightUnit.tonne,
            "oz" => WeightUnit.oz,
            "lb" => WeightUnit.lb,
            "stone" => WeightUnit.stone,
            _ => Enum.Parse<WeightUnit>(unitName, true)
        },
        "volume" => unitName.ToLower() switch
        {
            "ml" => VolumeUnit.ml,
            "l" => VolumeUnit.l,
            "gallon" => VolumeUnit.gallon,
            "quart" => VolumeUnit.quart,
            "pint" => VolumeUnit.pint,
            "cup" => VolumeUnit.cup,
            "tbsp" => VolumeUnit.tbsp,
            "tsp" => VolumeUnit.tsp,
            _ => Enum.Parse<VolumeUnit>(unitName, true)
        },
        "temperature" => unitName.ToLower() switch
        {
            "celsius" or "°c" => TemperatureUnit.Celsius,
            "fahrenheit" or "°f" => TemperatureUnit.Fahrenheit,
            "kelvin" or "k" => TemperatureUnit.Kelvin,
            _ => Enum.Parse<TemperatureUnit>(unitName, true)
        },
        _ => throw new QuantityMeasurementException($"Invalid measurement type: {measurementType}")
    };
}

        private dynamic CreateQuantity(QuantityInputDTO dto)
        {
            var unit = ParseUnit(dto.Unit, dto.MeasurementType);
            
            return dto.MeasurementType.ToLower() switch
            {
                "length" => new Quantity<LengthUnit>(dto.Value, (LengthUnit)unit, _lengthConverter),
                "weight" => new Quantity<WeightUnit>(dto.Value, (WeightUnit)unit, _weightConverter),
                "volume" => new Quantity<VolumeUnit>(dto.Value, (VolumeUnit)unit, _volumeConverter),
                "temperature" => new Quantity<TemperatureUnit>(dto.Value, (TemperatureUnit)unit, _temperatureConverter),
                _ => throw new QuantityMeasurementException($"Invalid measurement type: {dto.MeasurementType}")
            };
        }

        // UPDATED: SaveOperation with UserId
        private async Task<QuantityMeasurementDTO> SaveOperation(
            OperationType operation,
            QuantityInputDTO first,
            QuantityInputDTO second,
            object? result,
            string? errorMessage = null)
        {
            try
            {
                double resultValue = 0;
                string resultUnit = string.Empty;

                if (errorMessage == null && result != null)
                {
                    // Safely extract Value and Unit from anonymous/result objects
                    var valueProp = result.GetType().GetProperty("Value");
                    var unitProp = result.GetType().GetProperty("Unit");

                    if (valueProp != null)
                    {
                        var rawValue = valueProp.GetValue(result);
                        if (rawValue != null)
                        {
                            try { resultValue = Convert.ToDouble(rawValue); } catch { resultValue = 0; }
                        }
                    }

                    if (unitProp != null)
                    {
                        var rawUnit = unitProp.GetValue(result);
                        resultUnit = rawUnit?.ToString() ?? string.Empty;
                    }
                }

                var entity = new QuantityMeasurementEntity
                {
                    UserId = GetCurrentUserId(),
                    OperationType = operation.ToString(),
                    MeasurementType = first.MeasurementType,
                    FromValue = first.Value,
                    FromUnit = first.Unit,
                    ToValue = second.Value,
                    ToUnit = second.Unit,
                    Result = resultValue,
                    ResultUnit = resultUnit,
                    CreatedAt = DateTime.UtcNow,  //  Use UtcNow
                    SessionId = Guid.NewGuid(),
                    IsError = !string.IsNullOrEmpty(errorMessage),
                    ErrorMessage = errorMessage
                };

                var saved = await _repository.SaveToDatabaseAsync(entity);
                var dto = QuantityMeasurementDTO.FromEntity(saved);
                
                return dto;
            }
            catch (Exception ex)
            {
                return new QuantityMeasurementDTO
                {
                    Operation = operation,
                    MeasurementType = first.MeasurementType,
                    FromValue = first.Value,
                    FromUnit = first.Unit,
                    ToValue = second.Value,
                    ToUnit = second.Unit,
                    IsError = true,
                    ErrorMessage = errorMessage ?? ex.Message,
                    CreatedAt = DateTime.UtcNow
                };
            }
        }

        private int GetCurrentUserId()
        {
            var user = _httpContextAccessor?.HttpContext?.User;
            var claim = user?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(claim, out var id)) return id;
            return 0;
        }

        //  UPDATED: All methods with userId parameter
        public async Task<QuantityMeasurementDTO> CompareQuantities(
            QuantityInputDTO first, 
            QuantityInputDTO second)  
        {
            try
            {
                if (first.MeasurementType != second.MeasurementType)
                    throw new QuantityMeasurementException($"Cannot compare different measurement types: {first.MeasurementType} and {second.MeasurementType}");

                dynamic q1 = CreateQuantity(first);
                dynamic q2 = CreateQuantity(second);
                
                bool areEqual = q1.Equals(q2);
                
                var result = new { Value = areEqual ? 1.0 : 0.0, Unit = "Boolean" };
                
                return await SaveOperation(OperationType.Compare, first, second, result);
            }
            catch (Exception ex)
            {
                var dummyResult = new { Value = 0.0, Unit = "" };
                return await SaveOperation(OperationType.Compare, first, second, dummyResult, ex.Message);
            }
        }

        public async Task<QuantityMeasurementDTO> ConvertQuantity(
            QuantityInputDTO source, 
            QuantityInputDTO target)  
        {
            try
            {
                if (source.MeasurementType != target.MeasurementType)
                    throw new QuantityMeasurementException($"Cannot convert between different measurement types: {source.MeasurementType} and {target.MeasurementType}");

                dynamic q1 = CreateQuantity(source);
                var targetUnit = ParseUnit(target.Unit, target.MeasurementType);
                
                dynamic converted = ConvertQuantityByType(q1, source.MeasurementType, targetUnit);
                
                return await SaveOperation(OperationType.Convert, source, target, converted);
            }
            catch (Exception ex)
            {
                var dummyResult = new { Value = 0.0, Unit = "" };
                return await SaveOperation(OperationType.Convert, source, target, dummyResult, ex.Message);
            }
        }

        private dynamic ConvertQuantityByType(dynamic quantity, string measurementType, object targetUnit)
        {
            return measurementType.ToLower() switch
            {
                "length" => ((Quantity<LengthUnit>)quantity).ConvertTo((LengthUnit)targetUnit),
                "weight" => ((Quantity<WeightUnit>)quantity).ConvertTo((WeightUnit)targetUnit),
                "volume" => ((Quantity<VolumeUnit>)quantity).ConvertTo((VolumeUnit)targetUnit),
                "temperature" => ((Quantity<TemperatureUnit>)quantity).ConvertTo((TemperatureUnit)targetUnit),
                _ => throw new QuantityMeasurementException($"Unknown measurement type: {measurementType}")
            };
        }

        public async Task<QuantityMeasurementDTO> AddQuantities(
            QuantityInputDTO first, 
            QuantityInputDTO second, 
            string? resultUnit)  
        {
            try
            {
                if (first.MeasurementType != second.MeasurementType)
                    throw new QuantityMeasurementException($"Cannot add different measurement types: {first.MeasurementType} and {second.MeasurementType}");

                dynamic q1 = CreateQuantity(first);
                dynamic q2 = CreateQuantity(second);
                
                dynamic sum;
                if (!string.IsNullOrEmpty(resultUnit))
                {
                    var targetUnit = ParseUnit(resultUnit, first.MeasurementType);
                    sum = AddQuantitiesByType(q1, q2, first.MeasurementType, targetUnit);
                }
                else
                {
                    sum = AddQuantitiesByType(q1, q2, first.MeasurementType, null);
                }
                
                return await SaveOperation(OperationType.Add, first, second, sum);
            }
            catch (Exception ex)
            {
                var dummyResult = new { Value = 0.0, Unit = "" };
                return await SaveOperation(OperationType.Add, first, second, dummyResult, ex.Message);
            }
        }

        private dynamic AddQuantitiesByType(dynamic q1, dynamic q2, string measurementType, object? targetUnit)
        {
            return measurementType.ToLower() switch
            {
                "length" => targetUnit != null 
                    ? ((Quantity<LengthUnit>)q1).Add((Quantity<LengthUnit>)q2, (LengthUnit)targetUnit)
                    : ((Quantity<LengthUnit>)q1).Add((Quantity<LengthUnit>)q2),
                "weight" => targetUnit != null
                    ? ((Quantity<WeightUnit>)q1).Add((Quantity<WeightUnit>)q2, (WeightUnit)targetUnit)
                    : ((Quantity<WeightUnit>)q1).Add((Quantity<WeightUnit>)q2),
                "volume" => targetUnit != null
                    ? ((Quantity<VolumeUnit>)q1).Add((Quantity<VolumeUnit>)q2, (VolumeUnit)targetUnit)
                    : ((Quantity<VolumeUnit>)q1).Add((Quantity<VolumeUnit>)q2),
                "temperature" => targetUnit != null
                    ? ((Quantity<TemperatureUnit>)q1).Add((Quantity<TemperatureUnit>)q2, (TemperatureUnit)targetUnit)
                    : ((Quantity<TemperatureUnit>)q1).Add((Quantity<TemperatureUnit>)q2),
                _ => throw new QuantityMeasurementException($"Unknown measurement type: {measurementType}")
            };
        }

        public async Task<QuantityMeasurementDTO> SubtractQuantities(
            QuantityInputDTO first, 
            QuantityInputDTO second, 
            string? resultUnit)  
        {
            try
            {
                if (first.MeasurementType != second.MeasurementType)
                    throw new QuantityMeasurementException($"Cannot subtract different measurement types: {first.MeasurementType} and {second.MeasurementType}");

                dynamic q1 = CreateQuantity(first);
                dynamic q2 = CreateQuantity(second);
                
                dynamic difference;
                if (!string.IsNullOrEmpty(resultUnit))
                {
                    var targetUnit = ParseUnit(resultUnit, first.MeasurementType);
                    difference = SubtractQuantitiesByType(q1, q2, first.MeasurementType, targetUnit);
                }
                else
                {
                    difference = SubtractQuantitiesByType(q1, q2, first.MeasurementType, null);
                }
                
                return await SaveOperation(OperationType.Subtract, first, second, difference);
            }
            catch (Exception ex)
            {
                var dummyResult = new { Value = 0.0, Unit = "" };
                return await SaveOperation(OperationType.Subtract, first, second, dummyResult, ex.Message);
            }
        }

        private dynamic SubtractQuantitiesByType(dynamic q1, dynamic q2, string measurementType, object? targetUnit)
        {
            return measurementType.ToLower() switch
            {
                "length" => targetUnit != null
                    ? ((Quantity<LengthUnit>)q1).Subtract((Quantity<LengthUnit>)q2, (LengthUnit)targetUnit)
                    : ((Quantity<LengthUnit>)q1).Subtract((Quantity<LengthUnit>)q2),
                "weight" => targetUnit != null
                    ? ((Quantity<WeightUnit>)q1).Subtract((Quantity<WeightUnit>)q2, (WeightUnit)targetUnit)
                    : ((Quantity<WeightUnit>)q1).Subtract((Quantity<WeightUnit>)q2),
                "volume" => targetUnit != null
                    ? ((Quantity<VolumeUnit>)q1).Subtract((Quantity<VolumeUnit>)q2, (VolumeUnit)targetUnit)
                    : ((Quantity<VolumeUnit>)q1).Subtract((Quantity<VolumeUnit>)q2),
                "temperature" => targetUnit != null
                    ? ((Quantity<TemperatureUnit>)q1).Subtract((Quantity<TemperatureUnit>)q2, (TemperatureUnit)targetUnit)
                    : ((Quantity<TemperatureUnit>)q1).Subtract((Quantity<TemperatureUnit>)q2),
                _ => throw new QuantityMeasurementException($"Unknown measurement type: {measurementType}")
            };
        }

        public async Task<QuantityMeasurementDTO> DivideQuantities(
            QuantityInputDTO first, 
            QuantityInputDTO second)  
        {
            try
            {
                if (first.MeasurementType != second.MeasurementType)
                    throw new QuantityMeasurementException($"Cannot divide different measurement types: {first.MeasurementType} and {second.MeasurementType}");

                if (second.Value == 0)
                    throw new QuantityMeasurementException("Cannot divide by zero");

                dynamic q1 = CreateQuantity(first);
                dynamic q2 = CreateQuantity(second);
                
                double ratio = DivideQuantitiesByType(q1, q2, first.MeasurementType);
                
                var result = new { Value = ratio, Unit = "Ratio" };
                
                return await SaveOperation(OperationType.Divide, first, second, result);
            }
            catch (Exception ex)
            {
                var dummyResult = new { Value = 0.0, Unit = "" };
                return await SaveOperation(OperationType.Divide, first, second, dummyResult, ex.Message);
            }
        }

        private double DivideQuantitiesByType(dynamic q1, dynamic q2, string measurementType)
        {
            return measurementType.ToLower() switch
            {
                "length" => ((Quantity<LengthUnit>)q1).Divide((Quantity<LengthUnit>)q2),
                "weight" => ((Quantity<WeightUnit>)q1).Divide((Quantity<WeightUnit>)q2),
                "volume" => ((Quantity<VolumeUnit>)q1).Divide((Quantity<VolumeUnit>)q2),
                "temperature" => ((Quantity<TemperatureUnit>)q1).Divide((Quantity<TemperatureUnit>)q2),
                _ => throw new QuantityMeasurementException($"Unknown measurement type: {measurementType}")
            };
        }

        //  NEW: Get user-specific operations with caching
        public async Task<List<QuantityMeasurementDTO>> GetMyOperationsAsync()
        {
            var userId = GetCurrentUserId();

            var cacheKey = $"UserOperations_{userId}";

            if (!_cache.TryGetValue(cacheKey, out List<QuantityMeasurementDTO>? operations))
            {
                var entities = await _repository.GetByUserIdAsync(userId);
                operations = entities.Select(e => QuantityMeasurementDTO.FromEntity(e)).ToList();

                // Cache for 5 minutes
                _cache.Set(cacheKey, operations, TimeSpan.FromMinutes(5));
            }

            return operations ?? new List<QuantityMeasurementDTO>();
        }

        //  Keep existing methods for backward compatibility with caching
        public async Task<List<QuantityMeasurementDTO>> GetOperationHistory(OperationType operation)
        {
            var cacheKey = $"OperationHistory_{operation}";
            
            if (!_cache.TryGetValue(cacheKey, out List<QuantityMeasurementDTO>? history))
            {
                var entities = _repository.GetByOperationType(operation.ToString());
                history = entities.Select(e => QuantityMeasurementDTO.FromEntity(e)).ToList();
                
                // Cache for 10 minutes
                _cache.Set(cacheKey, history, TimeSpan.FromMinutes(10));
            }
            
            return history ?? new List<QuantityMeasurementDTO>();
        }

        public async Task<List<QuantityMeasurementDTO>> GetMeasurementTypeHistory(string measurementType)
        {
            var cacheKey = $"MeasurementTypeHistory_{measurementType}";
            
            if (!_cache.TryGetValue(cacheKey, out List<QuantityMeasurementDTO>? history))
            {
                var all = _repository.GetAllFromDatabase();
                history = all
                    .Where(e => e.MeasurementType?.Equals(measurementType, StringComparison.OrdinalIgnoreCase) == true)
                    .Select(e => QuantityMeasurementDTO.FromEntity(e))
                    .ToList();
                
                // Cache for 10 minutes
                _cache.Set(cacheKey, history, TimeSpan.FromMinutes(10));
            }
            
            return history ?? new List<QuantityMeasurementDTO>();
        }

        public async Task<int> GetOperationCount(OperationType operation)
        {
            return await Task.FromResult(_repository.GetByOperationType(operation.ToString()).Count);
        }

        public async Task<List<QuantityMeasurementDTO>> GetErrorHistory()
        {
            return await Task.FromResult(new List<QuantityMeasurementDTO>());
        }
        
    }
}
