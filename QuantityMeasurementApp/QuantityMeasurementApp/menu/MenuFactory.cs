using BusinessLayer.Interfaces;
using QuantityMeasurementApp.Interfaces;
using QuantityMeasurementApp.Menu;

namespace QuantityMeasurementApp.Factories
{
    public class MenuFactory : IMenuFactory
    {
        private readonly IQuantityMeasurementService _measurementService;

        public MenuFactory(IQuantityMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        public IQuantityMeasurementAppMenu CreateMenu()
        {
            return new QuantityMeasurementAppMenu(_measurementService);
        }
    }
}
