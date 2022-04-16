using System.Collections.Immutable;
using LambdaParser.Indexing;

namespace LambdaParser.SyntaxNodes;

public class ParenthesizedSyntaxNode : ExpressionLambdaSyntaxNode
{
    public ExpressionLambdaSyntaxNode Expression { get; }

    public ParenthesizedSyntaxNode(NodeLocation location, ExpressionLambdaSyntaxNode expression) : base(location)
    {
        Expression = expression;
    }

    public override ImmutableArray<LambdaSyntaxNode> GetChildren()
    {
        return ImmutableArray<LambdaSyntaxNode>.Empty
            .Add(Expression);
    }

    public override string ToString()
    {
        return $"({Expression})";
    }
}