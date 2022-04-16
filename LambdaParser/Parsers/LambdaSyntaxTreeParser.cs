using System.Collections.Immutable;
using LambdaParser.SyntaxNodes;
using LambdaParser.Tools;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace LambdaParser.Parsers;

public class LambdaSyntaxTreeParser
{
    public static IParseResult<LambdaSyntaxNode> Parse(string expression)
    {
        int startIndex = 0;
        ExpressionLambdaSyntaxNode root = null;
        Log.Verbose($"Start parsing tree: {expression}");

        do
        {
            var result = LambdaSyntaxNodeParser.Instance.Parse(new StringSegment(expression, startIndex, expression.Length - startIndex));
            var nextSymbolIndex = result.Node.Location.End + 1;
            if (expression[nextSymbolIndex] != Constants.EndBracket)
                throw new NotImplementedException($"Cannot parse expression after {result.Node.Location.End}");

            startIndex = nextSymbolIndex + 1;

            if (root is null)
                root = result.Node;
            else
                root = new ApplicationSyntaxNode(root.Location, root, ImmutableArray<ExpressionLambdaSyntaxNode>.Empty.Add(result.Node));
        } while (startIndex != expression.Length);


        if (root is null)
            throw new LambdaParseException($"Cannot parse any statement from {expression}");

        return new ParseResult<LambdaSyntaxNode>(root);
    }
}