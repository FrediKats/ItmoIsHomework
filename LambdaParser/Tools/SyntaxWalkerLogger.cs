using LambdaParser.Indexing;
using Serilog;

namespace LambdaParser.Tools;

public class SyntaxWalkerLogger
{
    public static void LogIt(string sourceCode, ParserError error)
    {
        LogIt(sourceCode, error.Message, error.Location);
    }

    public static void LogIt(string sourceCode, string text, NodeLocation location)
    {
        Log.Verbose(sourceCode);
        var space = string.Join("", Enumerable.Repeat(" ", location.Start.Value));
        Log.Verbose($"{space}^{text}");
    }
}