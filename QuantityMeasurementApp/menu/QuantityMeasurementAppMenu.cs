using System;
using ModelLayer.Enums;
using ModelLayer.Models;
using ModelLayer.Interfaces;
using ModelLayer.DTOs;
using BusinessLayer.Services;
using BusinessLayer.Interfaces;
using QuantityMeasurementApp.Interfaces;
using RepoLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace QuantityMeasurementApp.Menu
{
    public class QuantityMeasurementAppMenu : IQuantityMeasurementAppMenu
    {
        private readonly IQuantityMeasurementService _measurementService;
        
        public QuantityMeasurementAppMenu(IQuantityMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }
        
        public QuantityMeasurementAppMenu() : this(new QuantityMeasurementService())
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

        public void Run()
        {
            bool terminateProgram = false;

            while (!terminateProgram)
            {
                Console.WriteLine("\n====================================");
                Console.WriteLine("   QUANTITY MEASUREMENT APP v2.0");
                Console.WriteLine("   (With Advanced Database - UC16)");
                Console.WriteLine("====================================");
                Console.WriteLine("1. Length Measurement");
                Console.WriteLine("2. Weight Measurement");
                Console.WriteLine("3. Volume Measurement");
                Console.WriteLine("4. Temperature Measurement");
                Console.WriteLine("5. View Database History");
                Console.WriteLine("6. Database Statistics");
                Console.WriteLine("7. Exit");
                Console.WriteLine("====================================");
                Console.Write("Select option: ");

                string menuChoice = Console.ReadLine() ?? "";

                switch (menuChoice)
                {
                    case "1":
                        RunCategory<LengthUnit>("Length", "0:Inches, 1:Feet, 2:Yards, 3:CM, 4:Meters, 5:KM");
                        break;
                    case "2":
                        RunCategory<WeightUnit>("Weight", "0:Grams, 1:Kilograms, 2:Pounds, 3:Ounces, 4:Tons");
                        break;
                    case "3":
                        RunCategory<VolumeUnit>("Volume", "0:Liter, 1:MilliLiter, 2:Gallon, 3:Cubic Meters");
                        break;
                    case "4":
                        RunCategory<TemperatureUnit>("Temperature", "0:Celsius, 1:Fahrenheit, 2:Kelvin");
                        break;
                    case "5":
                        ShowDatabaseHistory();
                        break;
                    case "6":
                        ShowDatabaseStatistics();
                        break;
                    case "7":
                        terminateProgram = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
                
                if (!terminateProgram && menuChoice != "5" && menuChoice != "6")
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private void ShowDatabaseHistory()
        {
            Console.Clear();
            Console.WriteLine("\nDATABASE HISTORY (UC16)");
            Console.WriteLine("========================");
            
            if (_measurementService is QuantityMeasurementService service)
            {
                Console.Write("Enter page number (1-10): ");
                if (int.TryParse(Console.ReadLine(), out int page))
                {
                    service.ShowHistory(page);
                }
                else
                {
                    service.ShowHistory(1);
                }
            }
            else
            {
                Console.WriteLine("History view not available");
            }
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void ShowDatabaseStatistics()
        {
            Console.Clear();
            Console.WriteLine("\n DATABASE STATISTICS");
            Console.WriteLine("=====================");
            
            if (_measurementService is QuantityMeasurementService service)
            {
                service.ShowDatabaseStatistics();
            }
            else
            {
                Console.WriteLine("Statistics not available");
            }
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void RunCategory<T>(string categoryTitle, string unitOptions) where T : struct, Enum
        {
            bool goBack = false;

            while (!goBack)
            {
                Console.WriteLine($"\n--- {categoryTitle} Measurement ---");
                Console.WriteLine("1. Conversion\n2. Comparison\n3. Addition\n4. Subtraction\n5. Divide\n6. Back");

                string actionChoice = Console.ReadLine() ?? "";

                switch (actionChoice)
                {
                    case "1":
                        HandleConversion<T>(unitOptions);
                        break;
                    case "2":
                        HandleComparison<T>(unitOptions);
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

        private void HandleComparison<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine(unitOptions);
                var converter = ResolveConverter<T>();

                Console.Write("Value 1: ");
                double v1 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 1 Index: ");
                T u1 = (T)(object)int.Parse(Console.ReadLine()!);

                Console.Write("Value 2: ");
                double v2 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 2 Index: ");
                T u2 = (T)(object)int.Parse(Console.ReadLine()!);

                Quantity<T> q1 = new Quantity<T>(v1, u1, converter);
                Quantity<T> q2 = new Quantity<T>(v2, u2, converter);

                var result = _measurementService.Compare(q1, q2);

                Console.WriteLine($"\nResult: {q1} {(result.AreEqual ? "==" : "!=")} {q2}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private void HandleConversion<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine(unitOptions);
                var converter = ResolveConverter<T>();

                Console.Write("Value: ");
                double value = double.Parse(Console.ReadLine()!);

                Console.Write("Source Unit Index: ");
                T source = (T)(object)int.Parse(Console.ReadLine()!);

                Console.Write("Target Unit Index: ");
                T target = (T)(object)int.Parse(Console.ReadLine()!);

                var result = _measurementService.DemonstrateConversion(value, source, target);

                Console.WriteLine($"Result: {result.Value} {result.UnitSymbol}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private void HandleAddition<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine(unitOptions);
                var converter = ResolveConverter<T>();

                Console.Write("Value 1: ");
                double v1 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 1 Index: ");
                T u1 = (T)(object)int.Parse(Console.ReadLine()!);

                Console.Write("Value 2: ");
                double v2 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 2 Index: ");
                T u2 = (T)(object)int.Parse(Console.ReadLine()!);

                Quantity<T> q1 = new Quantity<T>(v1, u1, converter);
                Quantity<T> q2 = new Quantity<T>(v2, u2, converter);

                var result = _measurementService.DemonstrateAddition(q1, q2);

                Console.WriteLine($"Result: {result.Value} {result.UnitSymbol}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private void HandleSubtraction<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine(unitOptions);
                var converter = ResolveConverter<T>();

                Console.Write("Value 1: ");
                double v1 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 1 Index: ");
                T u1 = (T)(object)int.Parse(Console.ReadLine()!);

                Console.Write("Value 2: ");
                double v2 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 2 Index: ");
                T u2 = (T)(object)int.Parse(Console.ReadLine()!);

                Quantity<T> q1 = new Quantity<T>(v1, u1, converter);
                Quantity<T> q2 = new Quantity<T>(v2, u2, converter);

                var result = _measurementService.Subtract(q1, q2, q1.Unit);

                Console.WriteLine($"Result: {result.Value} {result.UnitSymbol}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        private void HandleDivision<T>(string unitOptions) where T : struct, Enum
        {
            try
            {
                Console.WriteLine(unitOptions);

                Console.Write("Value 1: ");
                double v1 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 1 Index: ");
                T u1 = (T)(object)int.Parse(Console.ReadLine()!);

                Console.Write("Value 2: ");
                double v2 = double.Parse(Console.ReadLine()!);

                Console.Write("Unit 2 Index: ");
                T u2 = (T)(object)int.Parse(Console.ReadLine()!);

                var result = _measurementService.Divide(v1, u1, v2, u2);

                Console.WriteLine($"Result Ratio: {result.Ratio:F4}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}