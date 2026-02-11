using System.Text;
using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator
{
    readonly JsonWriter writer;

    readonly List<string> history = [];

    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile) { Formatting = Formatting.Indented };
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN;
        StringBuilder operation = new();

        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");

        switch (op)
        {
            case "+":
                result = num1 + num2;
                writer.WriteValue("Add");
                break;
            case "-":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                break;
            case "*":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                break;
            case "/":
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
                break;
            case "^":
                result = Math.Pow(num1, num2);
                writer.WriteValue("Exponentiate");
                break;
            case "log":
                result = Math.Log(num1, num2);
                writer.WriteValue("Logarithm");
                break;
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        operation.Append($"{num1} {op} {num2} = {result}");
        history.Add(operation.ToString());

        return result;
    }

    public void PrintHistory()
    {
        int i = 1;

        Console.WriteLine(
            """
            History 

            """
        );
        foreach (string operation in history)
        {
            Console.WriteLine($"{i}: {operation}");
            i += 1;
        }
    }

    public void ClearHistory()
    {
        Console.WriteLine(
            """
            History cleared.

            """
        );
        history.Clear();
    }

    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}
