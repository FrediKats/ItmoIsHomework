using LambdaParser.SyntaxNodes;
using LambdaParser.Tools;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Parsers;

public class LambdaSyntaxNodeParser : INodeParser<ExpressionLambdaSyntaxNode>
{
    public static LambdaSyntaxNodeParser Instance { get; } = new LambdaSyntaxNodeParser();

    public IParseResult<ExpressionLambdaSyntaxNode> Parse(StringSegment expression)
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
                return LetterLambdaSyntaxNodeParser.CreateMaxSequence(expression);
        }
    }
}