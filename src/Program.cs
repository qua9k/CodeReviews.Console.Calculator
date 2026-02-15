using calculator.qua9k.lib;

namespace calculator.qua9k;

internal class Program
{
    private static void Main()
    {
        Calculator calc = new();
        Dictionary<string, string> operatorMap = new()
        {
            { "+", "Add" },
            { "-", "Subtract" },
            { "*", "Multiply" },
            { "/", "Divide" },
            { "^", "Exponentiate" },
            { "log", "Logarithm" }
        };

        var validOperand1 = double.MinValue;
        var postCalcChoice = "";
        var endApp = false;

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
            GetOperand(out var validOperand2);

            Console.WriteLine("Choose an operator:");
            UserInterface.PrintOperations(operatorMap);

            Console.Write("Your option? ");
            var op = Console.ReadLine();
            var result = double.MinValue;

            if (op == null || !operatorMap.ContainsKey(op))
                Console.WriteLine("Error: Unrecognized input.");
            else
                try
                {
                    result = calc.DoOperation(validOperand1, validOperand2, op);

                    if (double.IsNaN(result))
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    else
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("An exception occurred.\n - Details: " + e.Message);
                }

            UserInterface.Pause();
            UserInterface.PrintMenuOptions();

            postCalcChoice = Console.ReadLine();

            while (postCalcChoice is "h" or "d")
            {
                Console.Clear();
                UserInterface.PrintHeader();

                if (postCalcChoice == "h")
                    calc.PrintHistory();
                else
                    calc.ClearHistory();

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
            }
        }

        calc.Finish();
    }

    private static void GetOperand(out double validNum)
    {
        var userInput = Console.ReadLine();

        while (!double.TryParse(userInput, out validNum))
        {
            Console.Write("Invalid input. Enter a numeric value: ");
            userInput = Console.ReadLine();
        }
    }
}