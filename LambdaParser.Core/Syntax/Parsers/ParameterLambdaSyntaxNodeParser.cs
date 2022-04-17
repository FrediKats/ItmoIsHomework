using LambdaParser.Core.Syntax.Nodes;
using LambdaParser.Core.Syntax.Tools;
using LambdaParser.Core.Tools;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Core.Syntax.Parsers;

public class ParameterLambdaSyntaxNodeParser : ILambdaSyntaxNodeParser<ParameterLambdaSyntaxNode>
{
    public static ParameterLambdaSyntaxNodeParser Instance = new ParameterLambdaSyntaxNodeParser();

    public IParseResult<ParameterLambdaSyntaxNode> Parse(StringSegment expression)
    {
        int i = 0;
        //TODO: remove
        expression = expression.Trim();
        while (i < expression.Length && Char.IsLetter(expression[i]) && i < expression.Length)
        {
            i++;
        }

        if (i == 0)
            throw new LambdaParseException($"Cannot create Letter term at {expression.Offset}");

        var nodeLocation = NodeLocation.FromSegment(expression, i - 1);
        var lambdaSyntaxNode = new ParameterLambdaSyntaxNode(nodeLocation, expression.Substring(0, i));
        return new ParseResult<ParameterLambdaSyntaxNode>(lambdaSyntaxNode);
    }
}