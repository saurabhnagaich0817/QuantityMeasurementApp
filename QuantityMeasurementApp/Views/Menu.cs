using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Views
{
    /// <summary>
    /// View class responsible for all user interface interactions.
    /// Handles displaying menus, getting user input, and showing results.
    /// </summary>
    public class Menu
    {
        private readonly QuantityMeasurementService _measurementService;

        /// <summary>
        /// Initializes a new instance of the Menu class.
        /// </summary>
        public Menu()
        {
            _measurementService = new QuantityMeasurementService();
        }

        /// <summary>
        /// Displays the main menu and handles user interaction.
        /// </summary>
        public void Show()
        {
            Console.WriteLine("=== Quantity Measurement Application ===");
            Console.WriteLine("UC5: Unit-to-Unit Conversion (Same Measurement Type)\n");

            DisplayStaticExamples();

            while (true)
            {
                DisplayMainMenu();

                string? userChoice = Console.ReadLine();

                if (userChoice == "6" || userChoice?.ToLower() == "exit")
                    break;

                ProcessUserChoice(userChoice);
            }

            Console.WriteLine("\nThank you for using Quantity Measurement Application!");
        }

        /// <summary>
        /// Displays static method examples for conversion and equality.
        /// </summary>
        private void DisplayStaticExamples()
        {
            Console.WriteLine("--- Conversion Examples ---");
            Console.WriteLine(
                $"1.0 FEET to INCHES: {Quantity.ConvertValue(1.0, LengthUnit.FEET, LengthUnit.INCH):F6} inches"
            );
            Console.WriteLine(
                $"3.0 YARDS to FEET: {Quantity.ConvertValue(3.0, LengthUnit.YARD, LengthUnit.FEET):F6} feet"
            );
            Console.WriteLine(
                $"36.0 INCHES to YARDS: {Quantity.ConvertValue(36.0, LengthUnit.INCH, LengthUnit.YARD):F6} yards"
            );
            Console.WriteLine(
                $"1.0 CENTIMETERS to INCHES: {Quantity.ConvertValue(1.0, LengthUnit.CENTIMETER, LengthUnit.INCH):F6} inches"
            );
            Console.WriteLine(
                $"30.48 CENTIMETERS to FEET: {Quantity.ConvertValue(30.48, LengthUnit.CENTIMETER, LengthUnit.FEET):F6} feet"
            );
            Console.WriteLine();

            Console.WriteLine("--- Equality Examples ---");
            var quantity1 = new Quantity(1.0, LengthUnit.FEET);
            var quantity2 = new Quantity(12.0, LengthUnit.INCH);
            Console.WriteLine($"1 ft vs 12 in: {quantity1.Equals(quantity2)}");

            var quantity3 = new Quantity(1.0, LengthUnit.YARD);
            var quantity4 = new Quantity(36.0, LengthUnit.INCH);
            Console.WriteLine($"1 yd vs 36 in: {quantity3.Equals(quantity4)}");

            var quantity5 = new Quantity(1.0, LengthUnit.CENTIMETER);
            var quantity6 = new Quantity(0.393701, LengthUnit.INCH);
            Console.WriteLine($"1 cm vs 0.393701 in: {quantity5.Equals(quantity6)}\n");
        }

        /// <summary>
        /// Displays the main menu options.
        /// </summary>
        private void DisplayMainMenu()
        {
            Console.WriteLine("Choose operation:");
            Console.WriteLine("1. Unit Conversion");
            Console.WriteLine("2. Equality Comparison");
            Console.WriteLine("3. Round-trip Conversion Demo");
            Console.WriteLine("4. Batch Conversion Demo");
            Console.WriteLine("5. Backward compatibility (Original Feet/Inch classes)");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice (1-6): ");
        }

        /// <summary>
        /// Processes the user's main menu choice.
        /// </summary>
        /// <param name="userChoice">The user's menu choice.</param>
        private void ProcessUserChoice(string? userChoice)
        {
            switch (userChoice)
            {
                case "1":
                    ShowConversionScreen();
                    break;
                case "2":
                    ShowEqualityComparisonScreen();
                    break;
                case "3":
                    ShowRoundTripConversionDemo();
                    break;
                case "4":
                    ShowBatchConversionDemo();
                    break;
                case "5":
                    ShowBackwardCompatibilityScreen();
                    break;
                default:
                    Console.WriteLine("Invalid choice! Please try again.\n");
                    break;
            }
        }

        /// <summary>
        /// Displays the unit conversion screen.
        /// </summary>
        private void ShowConversionScreen()
        {
            Console.WriteLine("\n--- Unit Conversion ---");

            Console.WriteLine("Select source unit:");
            Console.WriteLine("1. Feet");
            Console.WriteLine("2. Inches");
            Console.WriteLine("3. Yards");
            Console.WriteLine("4. Centimeters");
            Console.Write("Enter choice (1-4): ");

            string? sourceChoice = Console.ReadLine();
            LengthUnit sourceUnit = GetUnitFromChoice(sourceChoice);

            if (sourceUnit == LengthUnit.FEET && sourceChoice != "1")
            {
                DisplayErrorMessage("Invalid source unit choice!");
                return;
            }

            Console.WriteLine("\nSelect target unit:");
            Console.WriteLine("1. Feet");
            Console.WriteLine("2. Inches");
            Console.WriteLine("3. Yards");
            Console.WriteLine("4. Centimeters");
            Console.Write("Enter choice (1-4): ");

            string? targetChoice = Console.ReadLine();
            LengthUnit targetUnit = GetUnitFromChoice(targetChoice);

            if (targetUnit == LengthUnit.FEET && targetChoice != "1")
            {
                DisplayErrorMessage("Invalid target unit choice!");
                return;
            }

            Console.WriteLine($"\nEnter value in {sourceUnit.GetFullName()}:");
            string? valueInput = Console.ReadLine();

            if (double.TryParse(valueInput, out double inputValue))
            {
                try
                {
                    double conversionResult = Quantity.ConvertValue(inputValue, sourceUnit, targetUnit);
                    Console.WriteLine(
                        $"\n{inputValue} {sourceUnit.GetSymbol()} = {conversionResult:F6} {targetUnit.GetSymbol()}"
                    );

                    // Show conversion formula
                    DisplayConversionFormula(inputValue, sourceUnit, targetUnit, conversionResult);
                }
                catch (ArgumentException ex)
                {
                    DisplayErrorMessage($"Conversion error: {ex.Message}");
                }
            }
            else
            {
                DisplayErrorMessage("Invalid numeric value!");
            }

            Console.WriteLine("----------------------------------------\n");
        }

        /// <summary>
        /// Displays the conversion formula.
        /// </summary>
        private void DisplayConversionFormula(
            double originalValue,
            LengthUnit sourceUnit,
            LengthUnit targetUnit,
            double convertedValue
        )
        {
            double sourceToFeet = sourceUnit.GetFeetConversionFactor();
            double targetToFeet = targetUnit.GetFeetConversionFactor();

            Console.WriteLine($"\nConversion formula:");
            Console.WriteLine(
                $"{originalValue} {sourceUnit.GetSymbol()} × ({sourceToFeet:F6} / {targetToFeet:F6}) = {convertedValue:F6} {targetUnit.GetSymbol()}"
            );
        }

        /// <summary>
        /// Displays the equality comparison screen.
        /// </summary>
        private void ShowEqualityComparisonScreen()
        {
            Console.WriteLine("\n--- Equality Comparison ---");

            Console.WriteLine("Select first unit:");
            Console.WriteLine("1. Feet");
            Console.WriteLine("2. Inches");
            Console.WriteLine("3. Yards");
            Console.WriteLine("4. Centimeters");
            Console.Write("Enter choice (1-4): ");

            string? firstUnitChoice = Console.ReadLine();
            LengthUnit firstUnit = GetUnitFromChoice(firstUnitChoice);

            if (firstUnit == LengthUnit.FEET && firstUnitChoice != "1")
            {
                DisplayErrorMessage("Invalid first unit choice!");
                return;
            }

            Console.WriteLine($"\nEnter value in {firstUnit.GetFullName()}:");
            string? firstValueInput = Console.ReadLine();

            Console.WriteLine("\nSelect second unit:");
            Console.WriteLine("1. Feet");
            Console.WriteLine("2. Inches");
            Console.WriteLine("3. Yards");
            Console.WriteLine("4. Centimeters");
            Console.Write("Enter choice (1-4): ");

            string? secondUnitChoice = Console.ReadLine();
            LengthUnit secondUnit = GetUnitFromChoice(secondUnitChoice);

            if (secondUnit == LengthUnit.FEET && secondUnitChoice != "1")
            {
                DisplayErrorMessage("Invalid second unit choice!");
                return;
            }

            Console.WriteLine($"\nEnter value in {secondUnit.GetFullName()}:");
            string? secondValueInput = Console.ReadLine();

            if (
                double.TryParse(firstValueInput, out double firstNumericValue)
                && double.TryParse(secondValueInput, out double secondNumericValue)
            )
            {
                try
                {
                    var quantityFirst = new Quantity(firstNumericValue, firstUnit);
                    var quantitySecond = new Quantity(secondNumericValue, secondUnit);

                    bool quantitiesEqual = quantityFirst.Equals(quantitySecond);

                    Console.WriteLine($"\nFirst: {quantityFirst}");
                    Console.WriteLine($"Second: {quantitySecond}");
                    Console.WriteLine(
                        $"Are they equal? {quantitiesEqual} ({(quantitiesEqual ? "Equal" : "Not Equal")})"
                    );

                    // Show converted values
                    Console.WriteLine($"\nBoth in feet:");
                    Console.WriteLine(
                        $"First in feet: {quantityFirst.ConvertTo(LengthUnit.FEET).NumericValue:F6} ft"
                    );
                    Console.WriteLine(
                        $"Second in feet: {quantitySecond.ConvertTo(LengthUnit.FEET).NumericValue:F6} ft"
                    );
                }
                catch (ArgumentException ex)
                {
                    DisplayErrorMessage($"Error: {ex.Message}");
                }
            }
            else
            {
                DisplayErrorMessage("Invalid numeric values!");
            }

            Console.WriteLine("----------------------------------------\n");
        }

        /// <summary>
        /// Displays round-trip conversion demonstration.
        /// </summary>
        private void ShowRoundTripConversionDemo()
        {
            Console.WriteLine("\n--- Round-trip Conversion Demo ---");
            Console.WriteLine(
                "This demonstrates that converting A→B→A returns the original value.\n"
            );

            double[] testValues = { 1.0, 2.5, 10.0, -3.0 };
            LengthUnit[] availableUnits =
            {
                LengthUnit.FEET,
                LengthUnit.INCH,
                LengthUnit.YARD,
                LengthUnit.CENTIMETER,
            };

            foreach (double originalValue in testValues)
            {
                foreach (LengthUnit sourceUnit in availableUnits)
                {
                    foreach (LengthUnit targetUnit in availableUnits)
                    {
                        if (sourceUnit != targetUnit)
                        {
                            try
                            {
                                double convertedValue = Quantity.ConvertValue(originalValue, sourceUnit, targetUnit);
                                double roundTripValue = Quantity.ConvertValue(convertedValue, targetUnit, sourceUnit);

                                bool preservesOriginalValue =
                                    Math.Abs(originalValue - roundTripValue) < 0.000001;

                                Console.WriteLine(
                                    $"{originalValue} {sourceUnit.GetSymbol()} → {targetUnit.GetSymbol()} → {sourceUnit.GetSymbol()}: "
                                        + $"{convertedValue:F6} → {roundTripValue:F6} {(preservesOriginalValue ? "✓" : "✗")}"
                                );
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine(
                "\nAll round-trip conversions preserve the original value within tolerance.\n"
            );
            Console.WriteLine("----------------------------------------\n");
        }

        /// <summary>
        /// Displays batch conversion demonstration.
        /// </summary>
        private void ShowBatchConversionDemo()
        {
            Console.WriteLine("\n--- Batch Conversion Demo ---");
            Console.WriteLine("Converting common measurements between all units:\n");

            double[] testValues = { 1, 12, 36, 2.54, 30.48, 91.44 };
            LengthUnit[] availableUnits =
            {
                LengthUnit.FEET,
                LengthUnit.INCH,
                LengthUnit.YARD,
                LengthUnit.CENTIMETER,
            };

            foreach (double testValue in testValues)
            {
                foreach (LengthUnit sourceUnit in availableUnits)
                {
                    Console.WriteLine($"\n{testValue} {sourceUnit.GetSymbol()} equals:");
                    foreach (LengthUnit targetUnit in availableUnits)
                    {
                        if (sourceUnit != targetUnit)
                        {
                            try
                            {
                                double resultValue = Quantity.ConvertValue(testValue, sourceUnit, targetUnit);
                                Console.WriteLine($"  {resultValue:F6} {targetUnit.GetSymbol()}");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(
                                    $"  Error converting to {targetUnit.GetSymbol()}: {ex.Message}"
                                );
                            }
                        }
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("----------------------------------------\n");
        }

        /// <summary>
        /// Displays backward compatibility screen using original classes.
        /// </summary>
        private void ShowBackwardCompatibilityScreen()
        {
            Console.WriteLine("\n--- Backward Compatibility (Original Classes) ---");
            Console.WriteLine("Choose measurement type:");
            Console.WriteLine("1. Feet comparison");
            Console.WriteLine("2. Inch comparison");
            Console.Write("Enter your choice (1-2): ");

            string? userChoice = Console.ReadLine();

            if (userChoice == "1")
            {
                ShowFeetComparisonScreen();
            }
            else if (userChoice == "2")
            {
                ShowInchComparisonScreen();
            }
            else
            {
                Console.WriteLine("Invalid choice!\n");
            }
        }

        /// <summary>
        /// Displays feet comparison screen.
        /// </summary>
        private void ShowFeetComparisonScreen()
        {
            Console.WriteLine("\n--- Feet Comparison (Original) ---");

            string? firstInput = GetUserInput("Enter first measurement in feet:");
            string? secondInput = GetUserInput("Enter second measurement in feet:");

            Feet? firstFeet = _measurementService.CreateFeetFromString(firstInput);
            Feet? secondFeet = _measurementService.CreateFeetFromString(secondInput);

            if (firstFeet is null || secondFeet is null)
            {
                DisplayErrorMessage("Invalid input! Please enter valid numeric values.");
                return;
            }

            bool areEqual = _measurementService.CompareFeetMeasurements(firstFeet, secondFeet);
            DisplayComparisonResult(firstFeet.ToString(), secondFeet.ToString(), areEqual);
        }

        /// <summary>
        /// Displays inch comparison screen.
        /// </summary>
        private void ShowInchComparisonScreen()
        {
            Console.WriteLine("\n--- Inch Comparison (Original) ---");

            string? firstInput = GetUserInput("Enter first measurement in inches:");
            string? secondInput = GetUserInput("Enter second measurement in inches:");

            Inch? firstInch = _measurementService.CreateInchFromString(firstInput);
            Inch? secondInch = _measurementService.CreateInchFromString(secondInput);

            if (firstInch is null || secondInch is null)
            {
                DisplayErrorMessage("Invalid input! Please enter valid numeric values.");
                return;
            }

            bool areEqual = _measurementService.CompareInchMeasurements(firstInch, secondInch);
            DisplayComparisonResult(firstInch.ToString(), secondInch.ToString(), areEqual);
        }

        /// <summary>
        /// Gets unit from menu choice.
        /// </summary>
        /// <param name="menuChoice">The menu choice.</param>
        /// <returns>The corresponding LengthUnit.</returns>
        private LengthUnit GetUnitFromChoice(string? menuChoice)
        {
            return menuChoice switch
            {
                "1" => LengthUnit.FEET,
                "2" => LengthUnit.INCH,
                "3" => LengthUnit.YARD,
                "4" => LengthUnit.CENTIMETER,
                _ => LengthUnit.FEET,
            };
        }

        /// <summary>
        /// Gets user input with prompt.
        /// </summary>
        /// <param name="promptMessage">The prompt to display.</param>
        /// <returns>The user's input.</returns>
        private string? GetUserInput(string promptMessage)
        {
            Console.WriteLine(promptMessage);
            return Console.ReadLine();
        }

        /// <summary>
        /// Displays comparison result.
        /// </summary>
        private void DisplayComparisonResult(string firstMeasurement, string secondMeasurement, bool areEqual)
        {
            Console.WriteLine($"\nFirst measurement: {firstMeasurement}");
            Console.WriteLine($"Second measurement: {secondMeasurement}");
            Console.WriteLine(
                $"Are they equal? {areEqual} ({(areEqual ? "Equal" : "Not Equal")})\n"
            );
            Console.WriteLine("----------------------------------------\n");
        }

        /// <summary>
        /// Displays error message.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        private void DisplayErrorMessage(string errorMessage)
        {
            Console.WriteLine($"{errorMessage}\n");
        }
    }
}