// ===================================================
// File: Menu.cs
// Project: QuantityMeasurementApp.Views
// Description: Interactive console menu for user interaction
// Author: Development Team
// Version: 3.0 (UC1, UC2, UC3)
// ===================================================

using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Views
{
    /// <summary>
    /// Handles all user interface interactions.
    /// UC3: Enhanced to support generic Quantity comparisons.
    /// </summary>
    /// <remarks>
    /// Follows separation of concerns:
    /// - Presentation logic only (no business logic)
    /// - Input validation and error handling
    /// - Clean separation from Models and Services
    /// </remarks>
    public class Menu
    {
        /// <summary>
        /// Service instance for measurement operations.
        /// </summary>
        private readonly QuantityMeasurementService _service;

        /// <summary>
        /// Initializes a new instance of the Menu class.
        /// </summary>
        public Menu()
        {
            _service = new QuantityMeasurementService();
        }

        /// <summary>
        /// Starts the main menu loop.
        /// Displays options and handles user input.
        /// </summary>
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

        /// <summary>
        /// Displays the main menu options.
        /// </summary>
        private void DisplayMainMenu()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("   QUANTITY MEASUREMENT APP - UC3");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Compare Feet (UC1 - Legacy)");
            Console.WriteLine("2. Compare Inches (UC2 - Legacy)");
            Console.WriteLine("3. Compare Quantity (UC3 - Generic)");
            Console.WriteLine("4. Exit");
            Console.WriteLine("========================================");
            Console.Write("Select an option: ");
        }

        /// <summary>
        /// Compares two Feet measurements.
        /// UC1: Legacy functionality.
        /// </summary>
        private void CompareFeet()
        {
            Console.WriteLine("\n--- Compare Feet (UC1) ---");
            
            try
            {
                // Get first feet value
                Console.Write("Enter first feet value: ");
                if (!double.TryParse(Console.ReadLine(), out double first))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    return;
                }

                // Get second feet value
                Console.Write("Enter second feet value: ");
                if (!double.TryParse(Console.ReadLine(), out double second))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    return;
                }

                // Create Feet objects and compare
                var feet1 = new Feet(first);
                var feet2 = new Feet(second);
                
                bool result = _service.AreEqual(feet1, feet2);
                
                Console.WriteLine($"\nResult: {result}");
                Console.WriteLine($"First: {feet1}");
                Console.WriteLine($"Second: {feet2}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}");
            }
        }

        /// <summary>
        /// Compares two Inch measurements.
        /// UC2: Legacy functionality.
        /// </summary>
        private void CompareInches()
        {
            Console.WriteLine("\n--- Compare Inches (UC2) ---");
            
            try
            {
                // Get first inch value
                Console.Write("Enter first inches value: ");
                if (!double.TryParse(Console.ReadLine(), out double first))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    return;
                }

                // Get second inch value
                Console.Write("Enter second inches value: ");
                if (!double.TryParse(Console.ReadLine(), out double second))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    return;
                }

                // Create Inch objects and compare
                var inch1 = new Inch(first);
                var inch2 = new Inch(second);
                
                bool result = _service.AreEqual(inch1, inch2);
                
                Console.WriteLine($"\nResult: {result}");
                Console.WriteLine($"First: {inch1}");
                Console.WriteLine($"Second: {inch2}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}");
            }
        }

        /// <summary>
        /// Compares two Quantity measurements.
        /// UC3: Generic comparison supporting different units.
        /// </summary>
        private void CompareQuantity()
        {
            Console.WriteLine("\n--- Compare Quantity (UC3 - Generic) ---");
            
            try
            {
                // Get first quantity
                Console.WriteLine("\nFirst Quantity:");
                var first = GetQuantityFromUser();
                if (first == null) return;

                // Get second quantity
                Console.WriteLine("\nSecond Quantity:");
                var second = GetQuantityFromUser();
                if (second == null) return;

                // Compare quantities
                bool result = _service.AreEqual(first, second);
                
                Console.WriteLine("\n========================================");
                Console.WriteLine($"Comparison Result: {result}");
                Console.WriteLine($"First: {first.ToDetailedString()}");
                Console.WriteLine($"Second: {second.ToDetailedString()}");
                Console.WriteLine("========================================");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}");
            }
        }

        /// <summary>
        /// Helper method to get quantity input from user.
        /// </summary>
        /// <returns>Quantity object or null if input invalid.</returns>
        private Quantity? GetQuantityFromUser()
        {
            // Get value
            Console.Write("Enter value: ");
            if (!double.TryParse(Console.ReadLine(), out double value))
            {
                Console.WriteLine("Invalid value. Please enter a valid number.");
                return null;
            }

            // Get unit
            Console.WriteLine("Select unit:");
            Console.WriteLine("1. FEET");
            Console.WriteLine("2. INCH");
            Console.Write("Choice: ");
            
            if (!int.TryParse(Console.ReadLine(), out int unitChoice) || (unitChoice != 1 && unitChoice != 2))
            {
                Console.WriteLine("Invalid unit choice. Please select 1 or 2.");
                return null;
            }

            LengthUnit unit = unitChoice == 1 ? LengthUnit.FEET : LengthUnit.INCH;
            
            return new Quantity(value, unit);
        }
    }
}