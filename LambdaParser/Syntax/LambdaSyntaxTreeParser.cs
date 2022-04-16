using LambdaParser.Syntax.Nodes;
using LambdaParser.Syntax.Parsers;
using LambdaParser.Syntax.Tools;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace LambdaParser.Syntax;

public class LambdaSyntaxTreeParser
{
    public static IParseResult<ExpressionLambdaSyntaxNode> Parse(string expression)
    {
        expression = SpaceFixer.FixSpaces(expression);

        int currentIndex = 0;
        ExpressionLambdaSyntaxNode? root = null;
        Log.Verbose($"Start parsing tree: {expression}");

        do
        {
            IParseResult<ExpressionLambdaSyntaxNode> result = LambdaSyntaxNodeParser.Instance.Parse(new StringSegment(expression, currentIndex, expression.Length - currentIndex));
            if (result.HasError)
                return result;

            ExpressionLambdaSyntaxNode resultNode = result.Node;
            currentIndex = resultNode.Location.End.Value + 1;

            if (root is null)
                root = resultNode;
            else
                root = new ApplicationSyntaxNode(root.Location, root, resultNode);
        } while (currentIndex != expression.Length);


        if (root is null)
            throw new LambdaParseException($"Cannot parse any statement from {expression}");

        return new ParseResult<ExpressionLambdaSyntaxNode>(root);
    }
}