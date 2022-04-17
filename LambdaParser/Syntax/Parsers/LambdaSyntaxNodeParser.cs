using LambdaParser.Syntax.Nodes;
using LambdaParser.Syntax.Tools;
using LambdaParser.Tools;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Syntax.Parsers;

public class LambdaSyntaxNodeParser : INodeParser<LambdaSyntaxNode>
{
    public static LambdaSyntaxNodeParser Instance { get; } = new LambdaSyntaxNodeParser();

    public IParseResult<LambdaSyntaxNode> Parse(StringSegment expression)
    {
        switch (expression[0])
        {
            case Constants.Lambda:
                return AbstractionLambdaSyntaxNodeParser.Instance.Parse(expression);
            case Constants.StartBracket:
                return ParenthesizedSyntaxNodeParser.Instance.Parse(expression);
            //case ' ':
            //    return Parse(expression.Subsegment(1));
            default:
                return ParameterLambdaSyntaxNodeParser.Instance.Parse(expression);
        }
    }
}