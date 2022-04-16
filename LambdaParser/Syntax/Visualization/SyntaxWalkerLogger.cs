using LambdaParser.Syntax.Indexing;
using LambdaParser.Syntax.Tools;
using LambdaParser.Tools;
using Serilog;

namespace LambdaParser.Syntax.Visualization;

public static class SyntaxWalkerLogger
{
    public static void LogIt(string sourceCode, ParserError error)
    {
        LogIt(sourceCode, error.Message, error.Location);
    }

    public static void LogIt(string sourceCode, string text, NodeLocation location)
    {
        Log.Verbose(sourceCode);
        var space = StringExtensions.FromChar(' ', location.Start.Value);
        Log.Verbose($"{space}^{text}");
    }
}