using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Entry point of Quantity Measurement Application.
    /// </summary>
    internal class Program
    {
        private static readonly QuantityMeasurementService _service = new QuantityMeasurementService();

        static void Main(string[] args)
        {
            Console.WriteLine("=== Quantity Measurement Application ===");
            Console.WriteLine("UC1 - Feet Equality Check");
            Console.WriteLine("----------------------------------------");
            RunApplication();
        }

        private static void RunApplication()
        {
            while (true)
            {
                Console.Write("\nEnter first value in feet (or type 'exit'): ");
                string? firstInput = Console.ReadLine();

                if(IsExitCommand(firstInput))
                    break;

                Console.Write("Enter second value in feet: ");
                string? secondInput = Console.ReadLine();

                if (IsExitCommand(secondInput))
                    break;

                ProcessComparison(firstInput, secondInput);
            }

            Console.WriteLine("\nApplication closed successfully.");
        }

        private static bool IsExitCommand(string? input)
        {
            return input?.Trim().ToLower() == "exit";
        }

        private static void ProcessComparison(string? input1, string? input2)
        {
            Feet? first = _service.ConvertToFeet(input1);
            Feet? second = _service.ConvertToFeet(input2);

            if (first is null || second is null)
            {
                Console.WriteLine("Invalid input. Please enter numeric values only.");
                return;
            }

            bool result = _service.CompareFeetEquality(first, second);
            Console.WriteLine($"\nFirst: {first}");
            Console.WriteLine($"Second: {second}");
            Console.WriteLine($"Result: {(result ? "Equal" : "Not Equal")}");
        }
    }
}