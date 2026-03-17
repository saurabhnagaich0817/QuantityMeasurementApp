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
        private readonly IQuantityRepository _repository;
        
        public QuantityMeasurementService(IQuantityRepository repository)
        {
            _repository = repository;
        }
        
        public QuantityMeasurementService() : this(new QuantityRepository())
        {
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

            bool areEqual = firstQuantity.Equals(secondQuantity);
            
            try
            {
                var entity = new QuantityMeasurementEntity
                {
                    OperationType = "Compare",
                    MeasurementType = typeof(U).Name,
                    FromValue = firstQuantity.Value,
                    FromUnit = firstQuantity.Unit.ToString(),
                    ToValue = secondQuantity.Value,
                    ToUnit = secondQuantity.Unit.ToString(),
                    Result = areEqual ? 1 : 0,
                    ResultUnit = "Boolean",
                    CreatedAt = DateTime.Now,
                    SessionId = Guid.NewGuid()
                };
                
                _repository.SaveToDatabase(entity);
                Console.WriteLine(" Operation saved to database");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database warning: {ex.Message}");
            }

            return new ComparisonResultDto
            {
                AreEqual = areEqual
            };
        }

        public QuantityResultDto DemonstrateConversion<U>(double numericValue, U sourceType, U targetType) where U : struct, Enum
        {
            var converter = ResolveConverter<U>();

            Quantity<U> tempQuantity = new Quantity<U>(numericValue, sourceType, converter);
            Quantity<U> converted = tempQuantity.ConvertTo(targetType);

            try
            {
                var entity = new QuantityMeasurementEntity
                {
                    OperationType = "Convert",
                    MeasurementType = typeof(U).Name,
                    FromValue = numericValue,
                    FromUnit = sourceType.ToString(),
                    ToValue = converted.Value,
                    ToUnit = targetType.ToString(),
                    Result = converted.Value,
                    ResultUnit = targetType.ToString(),
                    CreatedAt = DateTime.Now,
                    SessionId = Guid.NewGuid()
                };
                
                _repository.SaveToDatabase(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database warning: {ex.Message}");
            }

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
            
            try
            {
                var entity = new QuantityMeasurementEntity
                {
                    OperationType = "Add",
                    MeasurementType = typeof(U).Name,
                    FromValue = leftOperand.Value,
                    FromUnit = leftOperand.Unit.ToString(),
                    ToValue = rightOperand.Value,
                    ToUnit = rightOperand.Unit.ToString(),
                    Result = sum.Value,
                    ResultUnit = sum.Unit.ToString(),
                    CreatedAt = DateTime.Now,
                    SessionId = Guid.NewGuid()
                };
                
                _repository.SaveToDatabase(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database warning: {ex.Message}");
            }
            
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
            
            try
            {
                var entity = new QuantityMeasurementEntity
                {
                    OperationType = "Subtract",
                    MeasurementType = typeof(U).Name,
                    FromValue = firstValue.Value,
                    FromUnit = firstValue.Unit.ToString(),
                    ToValue = secondValue.Value,
                    ToUnit = secondValue.Unit.ToString(),
                    Result = difference.Value,
                    ResultUnit = difference.Unit.ToString(),
                    CreatedAt = DateTime.Now,
                    SessionId = Guid.NewGuid()
                };
                
                _repository.SaveToDatabase(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database warning: {ex.Message}");
            }
            
            return MapQuantityToDto(difference);
        }

        public DivisionResultDto Divide<T>(double firstValue, T firstUnit, double secondValue, T secondUnit) where T : struct, Enum
        {
            var converter = ResolveConverter<T>();

            Quantity<T> dividend = new Quantity<T>(firstValue, firstUnit, converter);
            Quantity<T> divisor = new Quantity<T>(secondValue, secondUnit, converter);

            double outcome = dividend.Divide(divisor);
            
            try
            {
                var entity = new QuantityMeasurementEntity
                {
                    OperationType = "Divide",
                    MeasurementType = typeof(T).Name,
                    FromValue = firstValue,
                    FromUnit = firstUnit.ToString(),
                    ToValue = secondValue,
                    ToUnit = secondUnit.ToString(),
                    Result = outcome,
                    ResultUnit = "Ratio",
                    CreatedAt = DateTime.Now,
                    SessionId = Guid.NewGuid()
                };
                
                _repository.SaveToDatabase(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database warning: {ex.Message}");
            }

            return new DivisionResultDto
            {
                Ratio = outcome
            };
        }
        
        public void ShowDatabaseStatistics()
        {
            if (_repository is QuantityDatabaseRepository dbRepo)
            {
                try
                {
                    string stats = dbRepo.GetStatistics();
                    Console.WriteLine(stats);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting stats: {ex.Message}");
                }
            }
        }
        
        public void ShowHistory(int page = 1, int pageSize = 10)
        {
            if (_repository is QuantityDatabaseRepository dbRepo)
            {
                try
                {
                    var records = dbRepo.GetMeasurementsPaged(page, pageSize);
                    
                    Console.WriteLine("\nID  Operation  Type       From           To             Result   Date");
                    // Console.WriteLine("----------------------------------------------------------------------");
                    
                    foreach (var item in records)
                    {
                        string line = item.Id.ToString().PadRight(3) + " " +
                                     (item.OperationType ?? "").PadRight(9) + " " +
                                     (item.MeasurementType ?? "").PadRight(8) + " " +
                                     item.FromValue.ToString("F2").PadRight(6) + " " +
                                     (item.FromUnit ?? "").PadRight(5) + " " +
                                     item.ToValue.ToString("F2").PadRight(6) + " " +
                                     (item.ToUnit ?? "").PadRight(5) + " " +
                                     item.Result.ToString("F2").PadRight(5) + " " +
                                     item.CreatedAt.ToString("yyyy-MM-dd HH:mm");
                        
                        Console.WriteLine(line);
                    }
                    
                    Console.WriteLine($"\nPage {page}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading history: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("History only available with database repository");
            }
        }
    }
}