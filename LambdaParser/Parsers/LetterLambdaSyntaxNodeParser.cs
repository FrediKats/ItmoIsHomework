using LambdaParser.SyntaxNodes;
using LambdaParser.Tools;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Parsers;

public class LetterLambdaSyntaxNodeParser : INodeParser<LetterLambdaSyntaxNode>
{
    public static LetterLambdaSyntaxNodeParser Instance = new LetterLambdaSyntaxNodeParser();

    public IParseResult<LetterLambdaSyntaxNode> Parse(StringSegment expression)
    {
        for (var index = 0; index < expression.ToString().Length; index++)
        {
            var symbol = expression[index];
            if (!char.IsLetter(symbol))
            {
                return ParseResult.Fail<LetterLambdaSyntaxNode>("Found non letter symbol while letter is expected", new NodeLocation(expression.Offset + index));
            }
        }

        var result = new LetterLambdaSyntaxNode(NodeLocation.FromSegment(expression), expression.Value);
        return new ParseResult<LetterLambdaSyntaxNode>(result);
    }

    public static IParseResult<LetterLambdaSyntaxNode> CreateMaxSequence(StringSegment expression)
    {
        int i = 0;
        while (i < expression.Length && Char.IsLetter(expression[i]) && i < expression.Length)
        {
            i++;
        }

        if (i == 0)
            throw new LambdaParseException($"Cannot create Letter term at {expression.Offset}");

        var lambdaSyntaxNode = new LetterLambdaSyntaxNode(new NodeLocation(expression.Offset, expression.Offset + i - 1), expression.Substring(0, i));
        return new ParseResult<LetterLambdaSyntaxNode>(lambdaSyntaxNode);
    }
}