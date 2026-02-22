using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Views
{
    /// <summary>
    /// Handles all user interaction.
    /// Keeps presentation logic separate from business logic.
    /// </summary>
    public class Menu
    {
        private readonly QuantityMeasurementService _service;

        public Menu()
        {
            _service = new QuantityMeasurementService();
        }

        /// <summary>
        /// Starts menu loop.
        /// </summary>
        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\n=== Quantity Measurement ===");
                Console.WriteLine("1. Compare Feet");
                Console.WriteLine("2. Compare Inches");
                Console.WriteLine("3. Exit");
                Console.Write("Select option: ");

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
                        return;
                    default:
                        Console.WriteLine("Invalid selection.");
                        break;
                }
            }
        }

        private void CompareFeet()
        {
            Console.Write("Enter first feet value: ");
            double first = double.Parse(Console.ReadLine()!);

            Console.Write("Enter second feet value: ");
            double second = double.Parse(Console.ReadLine()!);

            bool result = _service.AreEqual(new Feet(first), new Feet(second));

            Console.WriteLine($"Result: {result}");
        }

        private void CompareInches()
        {
            Console.Write("Enter first inches value: ");
            double first = double.Parse(Console.ReadLine()!);

            Console.Write("Enter second inches value: ");
            double second = double.Parse(Console.ReadLine()!);

            bool result = _service.AreEqual(new Inches(first), new Inches(second));

            Console.WriteLine($"Result: {result}");
        }
    }
}