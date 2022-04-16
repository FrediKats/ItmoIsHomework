using LambdaParser.SyntaxNodes;
using Microsoft.Extensions.Primitives;
using LambdaParser.Tools;
using Serilog;
using System.Collections.Immutable;

namespace LambdaParser.Parsers;

public class ParenthesizedSyntaxNodeParser : INodeParser<ParenthesizedSyntaxNode>
{
    public static ParenthesizedSyntaxNodeParser Instance { get; } = new ParenthesizedSyntaxNodeParser();

    public IParseResult<ParenthesizedSyntaxNode> Parse(StringSegment expression)
    {
        if (expression[0] != Constants.StartBracket)
            return ParseResult.Fail<ParenthesizedSyntaxNode>($"Cannot find start bracket", new NodeLocation(expression.Offset));

        IParseResult<ExpressionLambdaSyntaxNode> parseResult = ParseInner(expression.Subsegment(1));
        if (parseResult.HasError)
            return parseResult.As<ParenthesizedSyntaxNode>();
        if (expression.Length < parseResult.Node.Location.End + 1
            || expression[parseResult.Node.Location.End + 1] != Constants.EndBracket)
            return ParseResult.Fail<ParenthesizedSyntaxNode>($"Cannot find end bracket", new NodeLocation(parseResult.Node.Location.End));

        var nodeLocation = new NodeLocation(parseResult.Node.Location.Start - 1, parseResult.Node.Location.End + 1);
        var parenthesizedSyntaxNode = new ParenthesizedSyntaxNode(nodeLocation, parseResult.Node);
        return new ParseResult<ParenthesizedSyntaxNode>(parenthesizedSyntaxNode);
    }

    public IParseResult<ExpressionLambdaSyntaxNode> ParseInner(StringSegment expression)
    {
        int currentIndex = 0;
        ExpressionLambdaSyntaxNode root = null;
        Log.Verbose($"Try parse expression inside parenthesized: {expression}");

        do
        {
            Log.Verbose($"Start founding node inside parenthesize from index {currentIndex} (offset: {expression.Offset + currentIndex})");
            IParseResult<ExpressionLambdaSyntaxNode> result = LambdaSyntaxNodeParser.Instance.Parse(expression.Subsegment(currentIndex, expression.Length - currentIndex));
            if (result.HasError)
                return result;

            ExpressionLambdaSyntaxNode resultNode = result.Node;
            currentIndex = resultNode.Location.End + 1 - expression.Offset;

            if (root is null)
            {
                root = resultNode;
                Log.Verbose($"Found root node inside parenthesize: {root} at {root.Location}");
            }
            else
            {
                root = new ApplicationSyntaxNode(root.Location, root, ImmutableArray<ExpressionLambdaSyntaxNode>.Empty.Add(resultNode));
                Log.Verbose($"Add application node: {resultNode} at {resultNode.Location}");
            }
        } while (currentIndex < expression.Length && expression[currentIndex] != Constants.EndBracket);

        if (root is null)
            throw new LambdaParseException($"Cannot parse any statement from {expression}");

        if (currentIndex >= expression.Length)
            return ParseResult.Fail<ApplicationSyntaxNode>($"Cannot find end bracket", new NodeLocation(expression.Offset + currentIndex));

        return new ParseResult<ExpressionLambdaSyntaxNode>(root);
    }
}