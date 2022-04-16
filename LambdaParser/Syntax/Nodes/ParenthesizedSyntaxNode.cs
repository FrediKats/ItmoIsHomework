using System.Collections.Immutable;
using LambdaParser.Syntax.Indexing;

namespace LambdaParser.Syntax.Nodes;

public class ParenthesizedSyntaxNode : ExpressionLambdaSyntaxNode
{
    public ExpressionLambdaSyntaxNode Expression { get; }

    public ParenthesizedSyntaxNode(NodeLocation location, ExpressionLambdaSyntaxNode expression) : base(location)
    {
        Expression = expression;

        Expression.SetParent(this);
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