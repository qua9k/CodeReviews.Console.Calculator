using CalculatorLibrary;

class Program
{
    static void Main()
    {
        bool endApp = false;
        Calculator calc = new();
        Dictionary<string, string> operatorMap = new()
        {
            { "+", "Add" },
            { "-", "Subtract" },
            { "*", "Multiply" },
            { "/", "Divide" },
            { "^", "Exponentiate" },
            { "log", "Logarithm" },
        };

        double validOperand1 = double.MinValue;
        string? postCalcChoice = "";

        while (!endApp)
        {
            Console.Clear();
            UserInterface.PrintHeader();

            if (postCalcChoice == "p")
            {
                Console.WriteLine($"Previous result: {validOperand1}");
            }
            else
            {
                Console.Write("Enter the first operand: ");
                GetOperand(out validOperand1);
            }

            Console.Write("Enter the second operand: ");
            GetOperand(out double validOperand2);

            Console.WriteLine("Choose an operator:");
            UserInterface.PrintOperations(operatorMap);

            Console.Write("Your option? ");
            string? op = Console.ReadLine();
            double result = double.MinValue;

            if (op == null || !operatorMap.ContainsKey(op))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    result = calc.DoOperation(validOperand1, validOperand2, op);

                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else
                    {
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("An exception occurred.\n - Details: " + e.Message);
                }
            }

            UserInterface.Pause();
            UserInterface.PrintMenuOptions();

            postCalcChoice = Console.ReadLine();

            while (postCalcChoice == "h" || postCalcChoice == "d")
            {
                if (postCalcChoice == "h")
                {
                    Console.Clear();
                    UserInterface.PrintHeader();
                    calc.PrintHistory();
                }
                else
                {
                    Console.Clear();
                    UserInterface.PrintHeader();
                    calc.ClearHistory();
                }
                UserInterface.Pause();
                UserInterface.PrintMenuOptions();
                postCalcChoice = Console.ReadLine();
            }

            switch (postCalcChoice)
            {
                case "p":
                    validOperand1 = result;
                    break;
                case "n":
                    Console.WriteLine("Goodbye!");
                    endApp = true;
                    break;
                default:
                    break;
            }
        }

        calc.Finish();

        return;
    }

    public static void GetOperand(out double validNum)
    {
        string? userInput = Console.ReadLine();

        while (!double.TryParse(userInput, out validNum))
        {
            Console.Write("Invalid input. Enter a numeric value: ");
            userInput = Console.ReadLine();
        }
    }
}

static class UserInterface
{
    public static void PrintOperations(Dictionary<string, string> operatorMap)
    {
        foreach (KeyValuePair<string, string> kvp in operatorMap)
        {
            Console.WriteLine($"  {kvp.Key} : {kvp.Value}");
        }
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.Write("Press any key to continue.");
        Console.ReadKey();
    }

    public static void PrintMenuOptions()
    {
        Console.Write(
            """
            - 'n' + Enter : close the app.
            - 'p' + Enter : use the previous result as the first operand.
            - 'h' + Enter : view operation history.
            - 'd' + Enter : delete operation history.
            - Any other key to perform a new calculation.

            """
        );
    }

    public static void PrintHeader()
    {
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
    }
}
