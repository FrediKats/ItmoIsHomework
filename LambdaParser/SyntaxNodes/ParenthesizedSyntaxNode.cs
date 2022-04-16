using LambdaParser.Indexing;

namespace LambdaParser.SyntaxNodes;

public class ParenthesizedSyntaxNode : ExpressionLambdaSyntaxNode
{
    public ExpressionLambdaSyntaxNode Expression { get; }

    public ParenthesizedSyntaxNode(NodeLocation location, ExpressionLambdaSyntaxNode expression) : base(location)
    {
        Expression = expression;
    }

    public override string ToString()
    {
        return $"({Expression})";
    }
}