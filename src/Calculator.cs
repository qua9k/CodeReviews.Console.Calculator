using System.Text;
using System.Text.Json;

namespace calculator.qua9k.lib;

public class Calculator
{
    private readonly Utf8JsonWriter _writer;
    private readonly List<string> _history = [];

    public Calculator()
    {
        var logFile = File.OpenWrite(path: "calculator_log.json");
        _writer = new Utf8JsonWriter(logFile);
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operations");
        _writer.WriteStartArray();
    }

    public double DoOperation(double num1, double num2, string op)
    {
        var result = double.NaN;
        StringBuilder operation = new();

        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteNumberValue(num1);
        _writer.WritePropertyName("Operand2");
        _writer.WriteNumberValue(num2);
        _writer.WritePropertyName("Operation");

        switch (op)
        {
            case "+":
                result = num1 + num2;
                _writer.WriteStringValue("Add");
                break;
            case "-":
                result = num1 - num2;
                _writer.WriteStringValue("Subtract");
                break;
            case "*":
                result = num1 * num2;
                _writer.WriteStringValue("Multiply");
                break;
            case "/":
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                _writer.WriteStringValue("Divide");
                break;
            case "^":
                result = Math.Pow(num1, num2);
                _writer.WriteStringValue("Exponentiate");
                break;
            case "log":
                result = Math.Log(num1, num2);
                _writer.WriteStringValue("Logarithm");
                break;
        }
        result = Math.Round(result, 2);
        _writer.WritePropertyName("Result");
        _writer.WriteNumberValue(result);
        _writer.WriteEndObject();

        operation.Append($"{num1} {op} {num2} = {result}");
        _history.Add(operation.ToString());

        return result;
    }

    public void PrintHistory()
    {
        if (_history.Count <= 0)
        {
            Console.WriteLine("The history is empty.");
            return;
        }

        Console.WriteLine(
            """
            History 

            """
        );

        for (var i = 0; i < _history.Count; i++)
        {
            Console.WriteLine($"{i}: {_history[i]}");
        }
    }

    public void ClearHistory()
    {
        Console.WriteLine(
            """
            History cleared.

            """
        );
        _history.Clear();
    }

    public void Finish()
    {
        _writer.WriteEndArray();
        _writer.WriteEndObject();
        _writer.Flush();
        _writer.Dispose();
    }
}