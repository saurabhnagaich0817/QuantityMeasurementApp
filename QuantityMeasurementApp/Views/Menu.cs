// ===================================================
// File: Menu.cs
// Project: QuantityMeasurementApp.Views
// Description: Interactive console menu - UC4
// Author: Development Team
// Version: 4.0 (UC4 - Added Yard and Centimeter options)
// ===================================================

using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Views
{
    public class Menu
    {
        private readonly QuantityMeasurementService _service;

        public Menu()
        {
            _service = new QuantityMeasurementService();
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    DisplayMainMenu();
                    string? choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            CompareFeet();
                            break;
                        case "2":
                            CompareInches();
                            break;
                        case "3":
                            CompareQuantity();
                            break;
                        case "4":
                            Console.WriteLine("Thank you for using Quantity Measurement App!");
                            return;
                        default:
                            Console.WriteLine("Invalid selection. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine("\n================================================");
            Console.WriteLine("   QUANTITY MEASUREMENT APP - UC4");
            Console.WriteLine("   Extended Units: Yards & Centimeters");
            Console.WriteLine("================================================");
            Console.WriteLine("1. Compare Feet (UC1 - Legacy)");
            Console.WriteLine("2. Compare Inches (UC2 - Legacy)");
            Console.WriteLine("3. Compare Quantity (UC3/UC4 - All Units)");
            Console.WriteLine("4. Exit");
            Console.WriteLine("================================================");
            Console.Write("Select an option: ");
        }

        private void CompareFeet()
        {
            Console.WriteLine("\n--- Compare Feet (UC1) ---");
            
            try
            {
                Console.Write("Enter first feet value: ");
                if (!double.TryParse(Console.ReadLine(), out double first))
                {
                    Console.WriteLine("Invalid input.");
                    return;
                }

                Console.Write("Enter second feet value: ");
                if (!double.TryParse(Console.ReadLine(), out double second))
                {
                    Console.WriteLine("Invalid input.");
                    return;
                }

                var feet1 = new Feet(first);
                var feet2 = new Feet(second);
                
                bool result = _service.AreEqual(feet1, feet2);
                
                Console.WriteLine($"\nResult: {result}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void CompareInches()
        {
            Console.WriteLine("\n--- Compare Inches (UC2) ---");
            
            try
            {
                Console.Write("Enter first inches value: ");
                if (!double.TryParse(Console.ReadLine(), out double first))
                {
                    Console.WriteLine("Invalid input.");
                    return;
                }

                Console.Write("Enter second inches value: ");
                if (!double.TryParse(Console.ReadLine(), out double second))
                {
                    Console.WriteLine("Invalid input.");
                    return;
                }

                var inch1 = new Inch(first);
                var inch2 = new Inch(second);
                
                bool result = _service.AreEqual(inch1, inch2);
                
                Console.WriteLine($"\nResult: {result}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void CompareQuantity()
        {
            Console.WriteLine("\n--- Compare Quantity (UC4 - All Units) ---");
            Console.WriteLine("Supported Units: FEET, INCH, YARD, CENTIMETER");
            
            try
            {
                Console.WriteLine("\nFirst Quantity:");
                var first = GetQuantityFromUser();
                if (first == null) return;

                Console.WriteLine("\nSecond Quantity:");
                var second = GetQuantityFromUser();
                if (second == null) return;

                bool result = _service.AreEqual(first, second);
                
                Console.WriteLine("\n========================================");
                Console.WriteLine($"Comparison Result: {result}");
                Console.WriteLine($"First: {first.ToDetailedString()}");
                Console.WriteLine($"Second: {second.ToDetailedString()}");
                
                // Show conversions
                Console.WriteLine("\nConversions:");
                Console.WriteLine($"First in feet: {first.ValueInFeet:F4} ft");
                Console.WriteLine($"First in inches: {first.ValueInInches:F4} in");
                Console.WriteLine($"First in yards: {first.ValueInYards:F4} yd");
                Console.WriteLine($"First in cm: {first.ValueInCentimeters:F4} cm");
                Console.WriteLine("========================================");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private Quantity? GetQuantityFromUser()
        {
            Console.Write("Enter value: ");
            if (!double.TryParse(Console.ReadLine(), out double value))
            {
                Console.WriteLine("Invalid value.");
                return null;
            }

            Console.WriteLine("Select unit:");
            Console.WriteLine("1. FEET");
            Console.WriteLine("2. INCH");
            Console.WriteLine("3. YARD");
            Console.WriteLine("4. CENTIMETER");
            Console.Write("Choice: ");
            
            if (!int.TryParse(Console.ReadLine(), out int unitChoice) || unitChoice < 1 || unitChoice > 4)
            {
                Console.WriteLine("Invalid unit choice.");
                return null;
            }

            LengthUnit unit = unitChoice switch
            {
                1 => LengthUnit.FEET,
                2 => LengthUnit.INCH,
                3 => LengthUnit.YARD,
                4 => LengthUnit.CENTIMETER,
                _ => LengthUnit.FEET
            };
            
            return new Quantity(value, unit);
        }
    }
}