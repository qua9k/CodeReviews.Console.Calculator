namespace calculator.qua9k;

internal static class UserInterface
{
    public static void PrintOperations(Dictionary<string, string> operatorMap)
    {
        foreach (var kvp in operatorMap) Console.WriteLine($"  {kvp.Key} : {kvp.Value}");
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