﻿using LambdaParser.Syntax.Indexing;
using LambdaParser.Syntax.Nodes;
using LambdaParser.Syntax.Tools;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Syntax.Parsers;

public class LetterLambdaSyntaxNodeParser : INodeParser<ArgumentLambdaSyntaxNode>
{
    public static LetterLambdaSyntaxNodeParser Instance = new LetterLambdaSyntaxNodeParser();

    public IParseResult<ArgumentLambdaSyntaxNode> Parse(StringSegment expression)
    {
        expression = expression.Trim();
        for (var index = 0; index < expression.Length; index++)
        {
            var symbol = expression[index];
            if (!char.IsLetter(symbol))
            {
                var nodeLocation = new NodeLocation(new SourceCodeIndex(expression) + index);
                return ParseResult.Fail<ArgumentLambdaSyntaxNode>("Found non letter symbol while letter is expected", nodeLocation);
            }
        }

        var result = new ArgumentLambdaSyntaxNode(NodeLocation.FromSegment(expression), expression.Value);
        return new ParseResult<ArgumentLambdaSyntaxNode>(result);
    }

    public static IParseResult<ArgumentLambdaSyntaxNode> CreateMaxSequence(StringSegment expression)
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
        var lambdaSyntaxNode = new ArgumentLambdaSyntaxNode(nodeLocation, expression.Substring(0, i));
        return new ParseResult<ArgumentLambdaSyntaxNode>(lambdaSyntaxNode);
    }
}