﻿using LambdaParser.LambdaSyntaxNodes;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace LambdaParser.Parsers;

public class AbstractionLambdaSyntaxNodeParser
{
    public static AbstractionLambdaSyntaxNode Parse(StringSegment expression)
    {
        Log.Verbose($"AbstractionLambdaSyntaxNodeParser parse {expression}");

        if (expression.Length == 0 || expression[0] != Constants.Lambda)
            throw new LambdaParseException($"Unexpected char at {expression.Offset + 0}. Expect lambda letter.");

        var dotIndex = expression.IndexOf(Constants.Dot);
        if (dotIndex == -1)
            throw new LambdaParseException($"Invalid AbstractionLambdaSyntaxNode expression at {expression.Offset + 0}. Cannot find '.' char.");

        var endIndex = expression.IndexOf(Constants.EndBracket);
        if (endIndex == -1)
            throw new LambdaParseException($"Expect char '{Constants.EndBracket}' in expression at {expression.Offset}");

        LetterLambdaSyntaxNode letterLambdaSyntaxNode = LetterLambdaSyntaxNodeParser.Parse(expression.Subsegment(1, dotIndex - 1));
        Log.Verbose($"Parse {letterLambdaSyntaxNode} at {letterLambdaSyntaxNode.Location}");

        LambdaSyntaxNode body = LambdaSyntaxNodeParser.Parse(expression.Subsegment(dotIndex + 1, endIndex - dotIndex + 1));
        Log.Verbose($"Parse {body} at {body.Location}");

        return new AbstractionLambdaSyntaxNode(new NodeLocation(expression.Offset, expression.Offset + endIndex), letterLambdaSyntaxNode, body);
    }
}