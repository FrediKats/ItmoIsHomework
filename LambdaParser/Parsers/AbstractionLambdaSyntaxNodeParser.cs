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
        {
            NodeLocation nodeLocation = new NodeLocation(expression.Offset);
            return ParseResult.Fail<AbstractionLambdaSyntaxNode>((string) "Expect lambda letter at AbstractionLambdaSyntaxNode start.", nodeLocation);
        }

        var dotIndex = expression.IndexOf(Constants.Dot);
        if (dotIndex == -1)
        {
            NodeLocation nodeLocation = new NodeLocation(expression.Offset);
            return ParseResult.Fail<AbstractionLambdaSyntaxNode>((string) "Cannot find '.' char at AbstractionLambdaSyntaxNode start.", nodeLocation);
        }

        var startBracketIndex = expression.IndexOf(Constants.StartBracket);
        if (startBracketIndex == -1)
        {
            NodeLocation nodeLocation = new NodeLocation(expression.Offset);
            return ParseResult.Fail<AbstractionLambdaSyntaxNode>((string) "Cannot find '(' char at AbstractionLambdaSyntaxNode start.", nodeLocation);
        }

        if (dotIndex != startBracketIndex - 1)
        {
            NodeLocation nodeLocation = new NodeLocation(expression.Offset);
            return ParseResult.Fail<AbstractionLambdaSyntaxNode>((string) "Cannot find '.(' at AbstractionLambdaSyntaxNode start.", nodeLocation);
        }

        var endIndex = expression.IndexOf(Constants.EndBracket);
        if (endIndex == -1)
        {
            NodeLocation nodeLocation = new NodeLocation(expression.Offset);
            return ParseResult.Fail<AbstractionLambdaSyntaxNode>((string) $"Expect char '{Constants.EndBracket}' in AbstractionLambdaSyntaxNode start.", nodeLocation);
        }

        IParseResult<LetterLambdaSyntaxNode> argument = LetterLambdaSyntaxNodeParser.Instance.Parse(expression.Subsegment(1, dotIndex - 1));
        if (argument.HasError)
        {
            Log.Error(argument.Error.ToString());
            return argument.As<AbstractionLambdaSyntaxNode>();
        }

        LetterLambdaSyntaxNode argumentNode = argument.Node;
        Log.Verbose($"Parse lambda argument: {argumentNode} at {argumentNode.Location}");

        IParseResult<LambdaSyntaxNode> body = LambdaSyntaxNodeParser.Instance.Parse(expression.Subsegment(dotIndex + 1, endIndex - dotIndex + 1));
        if (body.HasError)
        {
            Log.Error(body.Error.ToString());
            return body.As<AbstractionLambdaSyntaxNode>();
        }

        LambdaSyntaxNode bodyNode = body.Node;
        Log.Verbose($"Parse lambda body: {bodyNode} at {bodyNode.Location}");

        var result = new AbstractionLambdaSyntaxNode(new NodeLocation(expression.Offset, expression.Offset + endIndex), argumentNode, bodyNode);
        return new ParseResult<AbstractionLambdaSyntaxNode>(result);
    }
}