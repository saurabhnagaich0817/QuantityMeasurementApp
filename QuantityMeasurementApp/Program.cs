using QuantityMeasurementApp.Factories;
using QuantityMeasurementApp.Interfaces;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Entry point of the Quantity Measurement application.
    /// Initializes required components using the factory pattern
    /// and starts the application menu.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Application starting point. Creates the menu using MenuFactory
        /// and runs the main application menu.
        /// </summary>
        private static void Main(string[] args)
        {
            IMenuFactory factory = new MenuFactory();

            IQuantityMeasurementAppMenu menu = factory.CreateMenu();

            menu.Run();
        }
    }
}