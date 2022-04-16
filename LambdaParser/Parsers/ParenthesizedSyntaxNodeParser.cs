using LambdaParser.SyntaxNodes;
using Microsoft.Extensions.Primitives;
using LambdaParser.Tools;

namespace LambdaParser.Parsers;

public class ParenthesizedSyntaxNodeParser : INodeParser<ParenthesizedSyntaxNode>
{
    public static ParenthesizedSyntaxNodeParser Instance { get; } = new ParenthesizedSyntaxNodeParser();

    public IParseResult<ParenthesizedSyntaxNode> Parse(StringSegment expression)
    {
        if (expression[0] != Constants.StartBracket)
            return ParseResult.Fail<ParenthesizedSyntaxNode>($"Cannot find start bracket", new NodeLocation(expression.Offset));

        IParseResult<ExpressionLambdaSyntaxNode> parseResult = LambdaSyntaxNodeParser.Instance.Parse(expression.Subsegment(1));
        if (parseResult.HasError)
            return parseResult.As<ParenthesizedSyntaxNode>();
        if (expression.Length < parseResult.Node.Location.End + 1
            || expression[parseResult.Node.Location.End + 1] != Constants.EndBracket)
            return ParseResult.Fail<ParenthesizedSyntaxNode>($"Cannot find end bracket", new NodeLocation(parseResult.Node.Location.End));

        var nodeLocation = new NodeLocation(parseResult.Node.Location.Start - 1, parseResult.Node.Location.End + 1);
        var parenthesizedSyntaxNode = new ParenthesizedSyntaxNode(nodeLocation, parseResult.Node);
        return new ParseResult<ParenthesizedSyntaxNode>(parenthesizedSyntaxNode);
    }
}