using LambdaParser.LambdaSyntaxNodes;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Parsers;

public class LetterLambdaSyntaxNodeParser
{
    public static LetterLambdaSyntaxNode Parse(StringSegment expression)
    {
        for (var index = 0; index < expression.ToString().Length; index++)
        {
            var symbol = expression.ToString()[index];
            if (!Char.IsLetter(symbol))
                throw new LambdaParseException($"Expect char symbols at {expression.Offset + index}");
        }

        return new LetterLambdaSyntaxNode(NodeLocation.FromSegment(expression), expression.Value);
    }
}