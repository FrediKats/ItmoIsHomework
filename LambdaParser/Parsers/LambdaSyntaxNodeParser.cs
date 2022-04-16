using LambdaParser.LambdaSyntaxNodes;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Parsers;

public class LambdaSyntaxNodeParser : INodeParser<LambdaSyntaxNode>
{
    public static LambdaSyntaxNodeParser Instance { get; } = new LambdaSyntaxNodeParser();

    public IParseResult<LambdaSyntaxNode> Parse(StringSegment expression)
    {
        while (true)
        {
            var result = ParseOneNode(expression);
            var nextSymbolIndex = result.Node.Location.End - expression.Offset;
            if (expression[nextSymbolIndex] == Constants.EndBracket)
                return result;

            throw new NotImplementedException($"Cannot parse expression after {result.Node.Location.End}");
        }
    }

    public static IParseResult<LambdaSyntaxNode> ParseOneNode(StringSegment expression)
    {
        var index = 0;
        switch (expression[index])
        {
            case Constants.Lambda:
                return AbstractionLambdaSyntaxNodeParser.Instance.Parse(expression);
            case '(':
                return ParseOneNode(expression.Subsegment(1));
            default:
                return LetterLambdaSyntaxNodeParser.CreateMaxSequence(expression);
        }
    }
}