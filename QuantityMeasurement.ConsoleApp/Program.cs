using QuantityMeasurement.Business.DTOs;
using QuantityMeasurement.Business.Services;
using QuantityMeasurement.Repository;

IQuantityService service = new QuantityService(new QuantityRepository());

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
            Console.WriteLine("Enter first length:");
            double l1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter second length:");
            double l2 = Convert.ToDouble(Console.ReadLine());

            var addLength = new QuantityPairRequest
            {
                Q1 = new QuantityRequest { Value = l1, Unit = "meter" },
                Q2 = new QuantityRequest { Value = l2, Unit = "meter" }
            };

            var result = service.Add(addLength);

            Console.WriteLine($"Result: {result.Result} {result.Unit}");
            break;

        case 2:
            Console.WriteLine("Enter first length:");
            double c1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter second length:");
            double c2 = Convert.ToDouble(Console.ReadLine());

            var compareLength = new QuantityPairRequest
            {
                Q1 = new QuantityRequest { Value = c1, Unit = "meter" },
                Q2 = new QuantityRequest { Value = c2, Unit = "meter" }
            };

            bool equal = service.Compare(compareLength);

            Console.WriteLine(equal ? "Both lengths are equal" : "Lengths are NOT equal");
            break;

        case 3:
            Console.WriteLine("Enter temperature:");
            double temp = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter from unit (C/F):");
            string from = Console.ReadLine();

            Console.WriteLine("Enter to unit (C/F):");
            string to = Console.ReadLine();

            double converted = service.ConvertTemperature(temp, from, to);

            Console.WriteLine($"Converted Temperature: {converted}");
            break;

        case 4:
            Console.WriteLine("Enter weight 1:");
            double w1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter weight 2:");
            double w2 = Convert.ToDouble(Console.ReadLine());

            var addWeight = new QuantityPairRequest
            {
                Q1 = new QuantityRequest { Value = w1, Unit = "kg" },
                Q2 = new QuantityRequest { Value = w2, Unit = "kg" }
            };

            var weightResult = service.Add(addWeight);

            Console.WriteLine($"Result: {weightResult.Result} kg");
            break;

        case 5:
            Console.WriteLine("Enter weight 1:");
            double cw1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter weight 2:");
            double cw2 = Convert.ToDouble(Console.ReadLine());

            var compareWeight = new QuantityPairRequest
            {
                Q1 = new QuantityRequest { Value = cw1, Unit = "kg" },
                Q2 = new QuantityRequest { Value = cw2, Unit = "kg" }
            };

            bool weightEqual = service.Compare(compareWeight);

            Console.WriteLine(weightEqual ? "Weights are equal" : "Weights are NOT equal");
            break;

        case 6:
            Console.WriteLine("Enter volume 1:");
            double v1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter volume 2:");
            double v2 = Convert.ToDouble(Console.ReadLine());

            var addVolume = new QuantityPairRequest
            {
                Q1 = new QuantityRequest { Value = v1, Unit = "liter" },
                Q2 = new QuantityRequest { Value = v2, Unit = "liter" }
            };

            var volumeResult = service.Add(addVolume);

            Console.WriteLine($"Result: {volumeResult.Result} liter");
            break;

        case 7:
            Console.WriteLine("Enter volume 1:");
            double cv1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter volume 2:");
            double cv2 = Convert.ToDouble(Console.ReadLine());

            var compareVolume = new QuantityPairRequest
            {
                Q1 = new QuantityRequest { Value = cv1, Unit = "liter" },
                Q2 = new QuantityRequest { Value = cv2, Unit = "liter" }
            };

            bool volumeEqual = service.Compare(compareVolume);

            Console.WriteLine(volumeEqual ? "Volumes are equal" : "Volumes are NOT equal");
            break;

        case 8:
            Console.WriteLine("Enter Celsius:");
            double cel = Convert.ToDouble(Console.ReadLine());

            double fah = service.ConvertTemperature(cel, "C", "F");

            Console.WriteLine($"Fahrenheit: {fah}");
            break;

        case 9:
            Console.WriteLine("Enter Fahrenheit:");
            double f = Convert.ToDouble(Console.ReadLine());

            double c = service.ConvertTemperature(f, "F", "C");

            Console.WriteLine($"Celsius: {c}");
            break;

        case 10:
            return;

        default:
            Console.WriteLine("Invalid Choice");
            break;
    }
}