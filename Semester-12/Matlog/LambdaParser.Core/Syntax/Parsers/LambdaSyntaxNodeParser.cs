using LambdaParser.Core.Syntax.Nodes;
using LambdaParser.Core.Syntax.Tools;
using LambdaParser.Core.Tools;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Core.Syntax.Parsers;

public class LambdaSyntaxNodeParser : ILambdaSyntaxNodeParser<LambdaSyntaxNode>
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
            default:
                return ParameterLambdaSyntaxNodeParser.Instance.Parse(expression);
        }
    }
}