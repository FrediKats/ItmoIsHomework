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
        int currentIndex = 0;
        ExpressionLambdaSyntaxNode root = null;
        Log.Verbose($"Start parsing tree: {expression}");

        do
        {
            IParseResult<ExpressionLambdaSyntaxNode> result = LambdaSyntaxNodeParser.Instance.Parse(new StringSegment(expression, currentIndex, expression.Length - currentIndex));
            if (result.HasError)
                return result;

            ExpressionLambdaSyntaxNode resultNode = result.Node;
            currentIndex = resultNode.Location.End + 1;

            if (root is null)
                root = resultNode;
            else
                root = new ApplicationSyntaxNode(root.Location, root, ImmutableArray<ExpressionLambdaSyntaxNode>.Empty.Add(resultNode));
        } while (currentIndex != expression.Length);


        if (root is null)
            throw new LambdaParseException($"Cannot parse any statement from {expression}");

        return new ParseResult<LambdaSyntaxNode>(root);
    }
}