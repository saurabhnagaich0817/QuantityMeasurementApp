using QuantityMeasurement.Business.Services;
using QuantityMeasurement.Repository;
using QuantityMeasurement.ConsoleApp.Menu;

IQuantityService service = new QuantityService(new QuantityRepository());

IMenu menu = new Menu(service);

menu.Start();