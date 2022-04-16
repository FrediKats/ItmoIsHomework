namespace LambdaParser.ConsoleIntegration;

public class ConsoleErrorHandler
{
    public static void Handle(string sourceCode, ParserError error)
    {
        Console.WriteLine(sourceCode);
        var space = string.Join("", Enumerable.Repeat(" ", error.Location.Start));
        Console.WriteLine($"{space}^{error.Message}");
    }
}