using System;
using ModelLayer.Enums;
using ModelLayer.Entities;
using ModelLayer.Interfaces;
using ModelLayer.DTOs;
using BusinessLayer.Services;
using BusinessLayer.Interfaces;
using QuantityMeasurementApp.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace QuantityMeasurementApp.Menu
{
    public class QuantityMeasurementAppMenu : IQuantityMeasurementAppMenu
    {
        private readonly IQuantityMeasurementService _measurementService;
        
        // Updated constructor - takes service
        public QuantityMeasurementAppMenu(IQuantityMeasurementService measurementService)
        {
            _measurementService = measurementService;
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

        public void Run()
        {
            bool terminateProgram = false;

            while (!terminateProgram)
            {
                
                Console.WriteLine("   QUANTITY MEASUREMENT APP v3.0");
                
               
                Console.WriteLine("1. Length Measurement");
                Console.WriteLine("2. Weight Measurement");
                Console.WriteLine("3. Volume Measurement");
                Console.WriteLine("4. Temperature Measurement");
                Console.WriteLine("5. View Database History");
                Console.WriteLine("6. Exit");
             
                Console.Write("Select option: ");

                string menuChoice = Console.ReadLine() ?? "";

                switch (menuChoice)
                {
                    case "1":
                        RunCategory<LengthUnit>("Length", "0:Inches, 1:Feet, 2:Yards, 3:Centimeters, 4:Meters, 5:Kilometers");
                        break;
                    case "2":
                        RunCategory<WeightUnit>("Weight", "0:Grams, 1:Kilograms, 2:Pounds, 3:Ounces, 4:Tons");
                        break;
                    case "3":
                        RunCategory<VolumeUnit>("Volume", "0:Milliliters, 1:Liters, 2:Gallons, 3:Cubic Meters");
                        break;
                    case "4":
                        RunCategory<TemperatureUnit>("Temperature", "0:Celsius, 1:Fahrenheit, 2:Kelvin");
                        break;
                    case "5":
                        ShowDatabaseHistory();
                        break;
                    case "6":
                        terminateProgram = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
                
                if (!terminateProgram && menuChoice != "5")
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private async void ShowDatabaseHistory()
        {
            try
            {
                var history = await _measurementService.GetMeasurementTypeHistory("Length");
                
                Console.WriteLine("\n=== DATABASE HISTORY ===");
                Console.WriteLine("ID | Operation | Type | From → To | Result");
                // Console.WriteLine("----------------------------------------");
                
                foreach (var item in history)
                {
                    Console.WriteLine($"{item.Id} | {item.Operation} | {item.MeasurementType} | " +
                                    $"{item.FromValue} {item.FromUnit} → {item.ToValue} {item.ToUnit} | " +
                                    $"{item.Result} {item.ResultUnit}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void RunCategory<T>(string categoryTitle, string unitOptions) where T : struct, Enum
        {
            bool goBack = false;

            while (!goBack)
            {
                Console.WriteLine($"\n--- {categoryTitle} Measurement ---");
                Console.WriteLine("1. Compare\n2. Convert\n3. Add\n4. Subtract\n5. Divide\n6. Back");

                string actionChoice = Console.ReadLine() ?? "";

                switch (actionChoice)
                {
                    case "1":
                        HandleComparison<T>(unitOptions);
                        break;
                    case "2":
                        HandleConversion<T>(unitOptions);
                        break;
                    case "3":
                        HandleAddition<T>(unitOptions);
                        break;
                    case "4":
                        HandleSubtraction<T>(unitOptions);
                        break;
                    case "5":
                        HandleDivision<T>(unitOptions);
                        break;
                    case "6":
                        goBack = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }
            }
        }

        private async void HandleComparison<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine(unitOptions);

                Console.Write("Value 1: ");
                double v1 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 1 Index: ");
                string u1 = ((T)(object)int.Parse(Console.ReadLine()!)).ToString();

                Console.Write("Value 2: ");
                double v2 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 2 Index: ");
                string u2 = ((T)(object)int.Parse(Console.ReadLine()!)).ToString();

                var first = new QuantityInputDTO { Value = v1, Unit = u1, MeasurementType = typeof(T).Name };
                var second = new QuantityInputDTO { Value = v2, Unit = u2, MeasurementType = typeof(T).Name };

                var result = await _measurementService.CompareQuantities(first, second);

                Console.WriteLine($"\nResult: {v1} {u1} {(result.Result == 1 ? "==" : "!=")} {v2} {u2}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private async void HandleConversion<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine(unitOptions);

                Console.Write("Value: ");
                double value = double.Parse(Console.ReadLine()!);

                Console.Write("Source Unit Index: ");
                string source = ((T)(object)int.Parse(Console.ReadLine()!)).ToString();

                Console.Write("Target Unit Index: ");
                string target = ((T)(object)int.Parse(Console.ReadLine()!)).ToString();

                var sourceDto = new QuantityInputDTO { Value = value, Unit = source, MeasurementType = typeof(T).Name };
                var targetDto = new QuantityInputDTO { Value = 0, Unit = target, MeasurementType = typeof(T).Name };

                var result = await _measurementService.ConvertQuantity(sourceDto, targetDto);

                Console.WriteLine($"Result: {result.Result} {target}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private async void HandleAddition<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine(unitOptions);

                Console.Write("Value 1: ");
                double v1 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 1 Index: ");
                string u1 = ((T)(object)int.Parse(Console.ReadLine()!)).ToString();

                Console.Write("Value 2: ");
                double v2 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 2 Index: ");
                string u2 = ((T)(object)int.Parse(Console.ReadLine()!)).ToString();

                Console.Write("Result Unit Index (or press Enter for default): ");
                string resultUnitInput = Console.ReadLine()!;
                string? resultUnit = string.IsNullOrEmpty(resultUnitInput) ? null : ((T)(object)int.Parse(resultUnitInput)).ToString();

                var first = new QuantityInputDTO { Value = v1, Unit = u1, MeasurementType = typeof(T).Name };
                var second = new QuantityInputDTO { Value = v2, Unit = u2, MeasurementType = typeof(T).Name };

                var result = await _measurementService.AddQuantities(first, second, resultUnit);

                Console.WriteLine($"Result: {result.Result} {result.ResultUnit}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private async void HandleSubtraction<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine(unitOptions);

                Console.Write("Value 1: ");
                double v1 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 1 Index: ");
                string u1 = ((T)(object)int.Parse(Console.ReadLine()!)).ToString();

                Console.Write("Value 2: ");
                double v2 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 2 Index: ");
                string u2 = ((T)(object)int.Parse(Console.ReadLine()!)).ToString();

                Console.Write("Result Unit Index (or press Enter for default): ");
                string resultUnitInput = Console.ReadLine()!;
                string? resultUnit = string.IsNullOrEmpty(resultUnitInput) ? null : ((T)(object)int.Parse(resultUnitInput)).ToString();

                var first = new QuantityInputDTO { Value = v1, Unit = u1, MeasurementType = typeof(T).Name };
                var second = new QuantityInputDTO { Value = v2, Unit = u2, MeasurementType = typeof(T).Name };

                var result = await _measurementService.SubtractQuantities(first, second, resultUnit);

                Console.WriteLine($"Result: {result.Result} {result.ResultUnit}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private async void HandleDivision<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine(unitOptions);

                Console.Write("Value 1: ");
                double v1 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 1 Index: ");
                string u1 = ((T)(object)int.Parse(Console.ReadLine()!)).ToString();

                Console.Write("Value 2: ");
                double v2 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 2 Index: ");
                string u2 = ((T)(object)int.Parse(Console.ReadLine()!)).ToString();

                var first = new QuantityInputDTO { Value = v1, Unit = u1, MeasurementType = typeof(T).Name };
                var second = new QuantityInputDTO { Value = v2, Unit = u2, MeasurementType = typeof(T).Name };

                var result = await _measurementService.DivideQuantities(first, second);

                Console.WriteLine($"Result Ratio: {result.Result:F4}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
