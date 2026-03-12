using QuantityMeasurement.Business.DTOs;
using QuantityMeasurement.Business.Services;

namespace QuantityMeasurement.ConsoleApp.Menu;

public class Menu : IMenu
{
    private readonly IQuantityService service;

    public Menu(IQuantityService service)
    {
        this.service = service;
    }

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("\n==== Quantity Measurement System ====");
            Console.WriteLine("1. Add Length");
            Console.WriteLine("2. Compare Length");
            Console.WriteLine("3. Convert Temperature");
            Console.WriteLine("4. Add Weight");
            Console.WriteLine("5. Compare Weight");
            Console.WriteLine("6. Add Volume");
            Console.WriteLine("7. Compare Volume");
            Console.WriteLine("8. Convert Celsius to Fahrenheit");
            Console.WriteLine("9. Convert Fahrenheit to Celsius");
            Console.WriteLine("10. Exit");

            Console.Write("\nEnter Choice: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddLength();
                    break;

                case 2:
                    CompareLength();
                    break;

                case 3:
                    ConvertTemperature();
                    break;

                case 4:
                    AddWeight();
                    break;

                case 5:
                    CompareWeight();
                    break;

                case 6:
                    AddVolume();
                    break;

                case 7:
                    CompareVolume();
                    break;

                case 8:
                    ConvertCtoF();
                    break;

                case 9:
                    ConvertFtoC();
                    break;

                case 10:
                    return;

                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }
    }

    private void AddLength()
    {
        Console.WriteLine("Enter first length:");
        double l1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter second length:");
        double l2 = Convert.ToDouble(Console.ReadLine());

        var request = new QuantityPairRequest
        {
            Q1 = new QuantityRequest { Value = l1, Unit = "meter" },
            Q2 = new QuantityRequest { Value = l2, Unit = "meter" }
        };

        var result = service.Add(request);

        Console.WriteLine($"Result: {result.Result} {result.Unit}");
    }

    private void CompareLength()
    {
        Console.WriteLine("Enter first length:");
        double l1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter second length:");
        double l2 = Convert.ToDouble(Console.ReadLine());

        var request = new QuantityPairRequest
        {
            Q1 = new QuantityRequest { Value = l1, Unit = "meter" },
            Q2 = new QuantityRequest { Value = l2, Unit = "meter" }
        };

        bool result = service.Compare(request);

        Console.WriteLine(result ? "Lengths are equal" : "Lengths are NOT equal");
    }

    private void ConvertTemperature()
    {
        Console.WriteLine("Enter temperature:");
        double temp = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter from unit (C/F):");
        string from = Console.ReadLine();

        Console.WriteLine("Enter to unit (C/F):");
        string to = Console.ReadLine();

        double result = service.ConvertTemperature(temp, from, to);

        Console.WriteLine($"Converted Temperature: {result}");
    }

    private void AddWeight()
    {
        Console.WriteLine("Enter weight 1:");
        double w1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter weight 2:");
        double w2 = Convert.ToDouble(Console.ReadLine());

        var request = new QuantityPairRequest
        {
            Q1 = new QuantityRequest { Value = w1, Unit = "kg" },
            Q2 = new QuantityRequest { Value = w2, Unit = "kg" }
        };

        var result = service.Add(request);

        Console.WriteLine($"Result: {result.Result} kg");
    }

    private void CompareWeight()
    {
        Console.WriteLine("Enter weight 1:");
        double w1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter weight 2:");
        double w2 = Convert.ToDouble(Console.ReadLine());

        var request = new QuantityPairRequest
        {
            Q1 = new QuantityRequest { Value = w1, Unit = "kg" },
            Q2 = new QuantityRequest { Value = w2, Unit = "kg" }
        };

        bool result = service.Compare(request);

        Console.WriteLine(result ? "Weights are equal" : "Weights are NOT equal");
    }

    private void AddVolume()
    {
        Console.WriteLine("Enter volume 1:");
        double v1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter volume 2:");
        double v2 = Convert.ToDouble(Console.ReadLine());

        var request = new QuantityPairRequest
        {
            Q1 = new QuantityRequest { Value = v1, Unit = "liter" },
            Q2 = new QuantityRequest { Value = v2, Unit = "liter" }
        };

        var result = service.Add(request);

        Console.WriteLine($"Result: {result.Result} liter");
    }

    private void CompareVolume()
    {
        Console.WriteLine("Enter volume 1:");
        double v1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Enter volume 2:");
        double v2 = Convert.ToDouble(Console.ReadLine());

        var request = new QuantityPairRequest
        {
            Q1 = new QuantityRequest { Value = v1, Unit = "liter" },
            Q2 = new QuantityRequest { Value = v2, Unit = "liter" }
        };

        bool result = service.Compare(request);

        Console.WriteLine(result ? "Volumes are equal" : "Volumes are NOT equal");
    }

    private void ConvertCtoF()
    {
        Console.WriteLine("Enter Celsius:");
        double cel = Convert.ToDouble(Console.ReadLine());

        double result = service.ConvertTemperature(cel, "C", "F");

        Console.WriteLine($"Fahrenheit: {result}");
    }

    private void ConvertFtoC()
    {
        Console.WriteLine("Enter Fahrenheit:");
        double f = Convert.ToDouble(Console.ReadLine());

        double result = service.ConvertTemperature(f, "F", "C");

        Console.WriteLine($"Celsius: {result}");
    }
}