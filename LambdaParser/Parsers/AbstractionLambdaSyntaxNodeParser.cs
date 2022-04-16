using LambdaParser.Indexing;
using LambdaParser.SyntaxNodes;
using LambdaParser.Tools;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace LambdaParser.Parsers;

public class AbstractionLambdaSyntaxNodeParser : INodeParser<AbstractionLambdaSyntaxNode>
{
    public static AbstractionLambdaSyntaxNodeParser Instance = new();

    public IParseResult<AbstractionLambdaSyntaxNode> Parse(StringSegment expression)
    {
        Log.Verbose($"Parse {expression} via AbstractionLambdaSyntaxNodeParser");

        if (expression.Length == 0 || expression[0] != Constants.Lambda)
        {
            var nodeLocation = NodeLocation.ForSegmentStart(expression);
            return ParseResult.Fail<AbstractionLambdaSyntaxNode>((string) "Expect lambda letter at AbstractionLambdaSyntaxNode start.", nodeLocation);
        }

        int dotIndex = expression.IndexOf(Constants.Dot);
        if (dotIndex == -1)
        {
            var nodeLocation = NodeLocation.ForSegmentStart(expression);
            return ParseResult.Fail<AbstractionLambdaSyntaxNode>((string) "Cannot find '.' char at AbstractionLambdaSyntaxNode start.", nodeLocation);
        }

        IParseResult<LetterLambdaSyntaxNode> argument = ParseArgument(expression.Subsegment(1, dotIndex - 1));
        if (argument.HasError)
        {
            return argument.As<AbstractionLambdaSyntaxNode>();
        }

        IParseResult<LambdaSyntaxNode> body = ParseBody(expression.Subsegment(dotIndex + 1));
        if (body.HasError)
        {
            return body.As<AbstractionLambdaSyntaxNode>();
        }

        var location = new NodeLocation(new SourceCodeIndex(expression), body.Node.Location.End);
        var result = new AbstractionLambdaSyntaxNode(location, argument.Node, body.Node);
        return new ParseResult<AbstractionLambdaSyntaxNode>(result);
    }

    public IParseResult<LetterLambdaSyntaxNode> ParseArgument(StringSegment expression)
    {
        IParseResult<LetterLambdaSyntaxNode> argument = LetterLambdaSyntaxNodeParser.Instance.Parse(expression);
        if (!argument.HasError)
            Log.Verbose($"Parse lambda argument: {argument.Node} at {argument.Node.Location}");

        return argument;
    }

    public IParseResult<ExpressionLambdaSyntaxNode> ParseBody(StringSegment expression)
    {
        IParseResult<ExpressionLambdaSyntaxNode> body = LambdaSyntaxNodeParser.Instance.Parse(expression);
        if (body.HasError)
        {
            Log.Error(body.Error.ToString());
            return body;
        }

        LambdaSyntaxNode bodyNode = body.Node;
        Log.Verbose($"Parse lambda body: {bodyNode} at {bodyNode.Location}");
        return body;
    }
}