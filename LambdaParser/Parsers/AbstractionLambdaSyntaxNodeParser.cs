using LambdaParser.SyntaxNodes;
using LambdaParser.Tools;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace LambdaParser.Parsers;

public class AbstractionLambdaSyntaxNodeParser : INodeParser<AbstractionLambdaSyntaxNode>
{
    public static AbstractionLambdaSyntaxNodeParser Instance = new AbstractionLambdaSyntaxNodeParser();

    public IParseResult<AbstractionLambdaSyntaxNode> Parse(StringSegment expression)
    {
        Log.Verbose($"Parse {expression} via AbstractionLambdaSyntaxNodeParser");

        if (expression.Length == 0 || expression[0] != Constants.Lambda)
            return new ParseResult<AbstractionLambdaSyntaxNode>("Expect lambda letter at AbstractionLambdaSyntaxNode start.", new NodeLocation(expression.Offset));

        var dotIndex = expression.IndexOf(Constants.Dot);
        if (dotIndex == -1)
            return new ParseResult<AbstractionLambdaSyntaxNode>("Cannot find '.' char at AbstractionLambdaSyntaxNode start.", new NodeLocation(expression.Offset));

        var startBracketIndex = expression.IndexOf(Constants.StartBracket);
        if (startBracketIndex == -1)
            return new ParseResult<AbstractionLambdaSyntaxNode>("Cannot find '(' char at AbstractionLambdaSyntaxNode start.", new NodeLocation(expression.Offset));

        if (dotIndex != startBracketIndex - 1)
            return new ParseResult<AbstractionLambdaSyntaxNode>("Cannot find '.(' at AbstractionLambdaSyntaxNode start.", new NodeLocation(expression.Offset));

        var endIndex = expression.IndexOf(Constants.EndBracket);
        if (endIndex == -1)
            return new ParseResult<AbstractionLambdaSyntaxNode>($"Expect char '{Constants.EndBracket}' in AbstractionLambdaSyntaxNode start.", new NodeLocation(expression.Offset));

        LetterLambdaSyntaxNode letterLambdaSyntaxNode = LetterLambdaSyntaxNodeParser.Parse(expression.Subsegment(1, dotIndex - 1));
        Log.Verbose($"Parse lambda argument: {letterLambdaSyntaxNode} at {letterLambdaSyntaxNode.Location}");

        IParseResult<LambdaSyntaxNode> body = LambdaSyntaxNodeParser.Instance.Parse(expression.Subsegment(dotIndex + 1, endIndex - dotIndex + 1));
        Log.Verbose($"Parse lambda body: {body.Node} at {body.Node.Location}");

        var result = new AbstractionLambdaSyntaxNode(new NodeLocation(expression.Offset, expression.Offset + endIndex), letterLambdaSyntaxNode, body.Node);
        return new ParseResult<AbstractionLambdaSyntaxNode>(result);
    }
}