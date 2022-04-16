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

    public static LetterLambdaSyntaxNode CreateMaxSequence(StringSegment expression)
    {
        int i = 0;
        while (Char.IsLetter(expression[i]) && i < expression.Length)
        {
            i++;
        }

        if (i == 0)
            throw new LambdaParseException($"Cannot create Letter term at {expression.Offset}");

        return new LetterLambdaSyntaxNode(new NodeLocation(expression.Offset, expression.Offset + i), expression.Substring(0, i));
    }
}