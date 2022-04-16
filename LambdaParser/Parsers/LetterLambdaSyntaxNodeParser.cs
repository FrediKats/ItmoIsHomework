using LambdaParser.Indexing;
using LambdaParser.SyntaxNodes;
using LambdaParser.Tools;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Parsers;

public class LetterLambdaSyntaxNodeParser : INodeParser<LetterLambdaSyntaxNode>
{
    public static LetterLambdaSyntaxNodeParser Instance = new LetterLambdaSyntaxNodeParser();

    public IParseResult<LetterLambdaSyntaxNode> Parse(StringSegment expression)
    {
        expression = expression.Trim();
        for (var index = 0; index < expression.Length; index++)
        {
            var symbol = expression[index];
            if (!char.IsLetter(symbol))
            {
                var nodeLocation = new NodeLocation(new SourceCodeIndex(expression) + index);
                return ParseResult.Fail<LetterLambdaSyntaxNode>("Found non letter symbol while letter is expected", nodeLocation);
            }
        }

        var result = new LetterLambdaSyntaxNode(NodeLocation.FromSegment(expression), expression.Value);
        return new ParseResult<LetterLambdaSyntaxNode>(result);
    }

    public static IParseResult<LetterLambdaSyntaxNode> CreateMaxSequence(StringSegment expression)
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
        var lambdaSyntaxNode = new LetterLambdaSyntaxNode(nodeLocation, expression.Substring(0, i));
        return new ParseResult<LetterLambdaSyntaxNode>(lambdaSyntaxNode);
    }
}