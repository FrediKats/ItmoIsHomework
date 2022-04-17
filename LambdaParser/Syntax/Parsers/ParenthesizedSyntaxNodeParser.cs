using LambdaParser.Syntax.Indexing;
using LambdaParser.Syntax.Nodes;
using LambdaParser.Syntax.Tools;
using LambdaParser.Tools;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace LambdaParser.Syntax.Parsers;

public class ParenthesizedSyntaxNodeParser : INodeParser<ParenthesizedSyntaxNode>
{
    public static ParenthesizedSyntaxNodeParser Instance { get; } = new ParenthesizedSyntaxNodeParser();

    public IParseResult<ParenthesizedSyntaxNode> Parse(StringSegment expression)
    {
        if (expression[0] != Constants.StartBracket)
            return ParseResult.Fail<ParenthesizedSyntaxNode>($"Cannot find start bracket", NodeLocation.ForSegmentStart(expression));

        IParseResult<LambdaSyntaxNode> parseResult = ParseInner(expression.Subsegment(1));
        if (parseResult.HasError)
            return parseResult.As<ParenthesizedSyntaxNode>();

        var endBracket = parseResult.Node.Location.End.ToLocalIndex(expression) + 1;
        var nodeLocation = NodeLocation.FromSegment(expression, endBracket);
        if (endBracket >= expression.Length)
            return ParseResult.Fail<ParenthesizedSyntaxNode>($"Raise string segment end but cannot find end bracket", nodeLocation);

        if (expression[endBracket] != Constants.EndBracket)
            return ParseResult.Fail<ParenthesizedSyntaxNode>($"Expect end bracket in parenthesize expression end.", nodeLocation);

        var parenthesizedSyntaxNode = new ParenthesizedSyntaxNode(nodeLocation, parseResult.Node);
        return new ParseResult<ParenthesizedSyntaxNode>(parenthesizedSyntaxNode);
    }

    public IParseResult<LambdaSyntaxNode> ParseInner(StringSegment expression)
    {
        int currentIndex = 0;
        LambdaSyntaxNode? root = null;
        Log.Verbose($"Try parse expression inside parenthesized: {expression}");

        do
        {
            Log.Verbose($"Start founding node inside parenthesize from index {currentIndex} (offset: {expression.Offset + currentIndex})");
            IParseResult<LambdaSyntaxNode> result = LambdaSyntaxNodeParser.Instance.Parse(expression.Subsegment(currentIndex, expression.Length - currentIndex));
            if (result.HasError)
                return result;

            LambdaSyntaxNode resultNode = result.Node;
            currentIndex = resultNode.Location.End.ToLocalIndex(expression) + 1;

            if (root is null)
            {
                root = resultNode;
                Log.Verbose($"Found root node inside parenthesize: {root} at {root.Location}");
            }
            else
            {
                root = new ApplicationSyntaxNode(new NodeLocation(root.Location.Start, resultNode.Location.End), root, resultNode);
                Log.Verbose($"Add application node: {resultNode} at {resultNode.Location}");
            }
        } while (currentIndex < expression.Length && expression[currentIndex] != Constants.EndBracket);

        if (root is null)
            throw new LambdaParseException($"Cannot parse any statement from {expression}");

        if (currentIndex >= expression.Length)
            return ParseResult.Fail<ApplicationSyntaxNode>($"Cannot find end bracket", new NodeLocation(new SourceCodeIndex(expression) + currentIndex));

        return new ParseResult<LambdaSyntaxNode>(root);
    }
}