using LambdaParser.Core.Syntax.Nodes;
using LambdaParser.Core.Syntax.Parsers;
using LambdaParser.Core.Syntax.Tools;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace LambdaParser.Core.Syntax;

public class LambdaSyntaxTreeParser
{
    public static IParseResult<LambdaSyntaxNode> Parse(string expression)
    {
        expression = SpaceFixer.FixSpaces(expression);

        int currentIndex = 0;
        LambdaSyntaxNode? root = null;
        Log.Verbose($"Start parsing tree: {expression}");

        do
        {
            IParseResult<LambdaSyntaxNode> result = LambdaSyntaxNodeParser.Instance.Parse(new StringSegment(expression, currentIndex, expression.Length - currentIndex));
            if (result.HasError)
                return result;

            LambdaSyntaxNode resultNode = result.Node;
            currentIndex = resultNode.Location.End.Value + 1;

            if (root is null)
                root = resultNode;
            else
                root = new ApplicationSyntaxNode(root.Location, root, resultNode);
        } while (currentIndex != expression.Length);


        if (root is null)
            throw new LambdaParseException($"Cannot parse any statement from {expression}");

        return new ParseResult<LambdaSyntaxNode>(root);
    }
}