using System.Collections.Immutable;
using LambdaParser.Core.Tools;

namespace LambdaParser.Syntax.Nodes;

public class ParenthesizedSyntaxNode : LambdaSyntaxNode
{
    public LambdaSyntaxNode Expression { get; }

    public ParenthesizedSyntaxNode(NodeLocation location, LambdaSyntaxNode expression) : base(location)
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