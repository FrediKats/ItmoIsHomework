using LambdaParser.Syntax.Indexing;

namespace LambdaParser.Syntax.Nodes;

public abstract class ExpressionLambdaSyntaxNode : LambdaSyntaxNode
{
    protected ExpressionLambdaSyntaxNode(LambdaSyntaxNode? parent, NodeLocation location) : base(parent, location)
    {
    }

    protected ExpressionLambdaSyntaxNode(NodeLocation location) : base(location)
    {
    }
}