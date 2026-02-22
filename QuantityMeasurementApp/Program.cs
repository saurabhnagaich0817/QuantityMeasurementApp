// ===================================================
// File: Program.cs
// Project: QuantityMeasurementApp
// Description: Application entry point with main method

// ===================================================

using QuantityMeasurementApp.Views;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Main entry point for the Quantity Measurement Application.
    /// UC3: Generic Length implementation with backward compatibility.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Application starting point.
        /// Creates and starts the menu system.
        /// </summary>
        /// <param name="args">Command line arguments (not used)</param>
        static void Main(string[] args)
        {
            Console.WriteLine("=== Quantity Measurement App UC3 ===");
         
            
            
            // Create and start the interactive menu
            var menu = new Menu();
            menu.Start();
        }
    }
}