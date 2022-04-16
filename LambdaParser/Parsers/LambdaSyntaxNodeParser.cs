using LambdaParser.LambdaSyntaxNodes;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Parsers;

public class LambdaSyntaxNodeParser
{
    public static LambdaSyntaxNode Parse(StringSegment expression)
    {
        while (true)
        {
            LambdaSyntaxNode result = ParseOneNode(expression);
            var nextSymbolIndex = result.Location.End - expression.Offset;
            if (expression[nextSymbolIndex] == Constants.EndBracket)
                return result;

            throw new NotImplementedException($"Cannot parse expression after {result.Location.End}");
        }
    }

    public static LambdaSyntaxNode ParseOneNode(StringSegment expression)
    {
        var index = 0;
        switch (expression[index])
        {
            case Constants.Lambda:
                return AbstractionLambdaSyntaxNodeParser.Parse(expression);
            case '(':
                return ParseOneNode(expression.Subsegment(1));
            default:
                return LetterLambdaSyntaxNodeParser.CreateMaxSequence(expression);
        }
    }
}