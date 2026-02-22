using QuantityMeasurementApp.Views;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Application entry point.
    /// Only responsible for starting the UI layer.
    /// </summary>
    internal class Program
    {
        static void Main()
        {
            var menu = new Menu();
            menu.Start();
        }
    }
}