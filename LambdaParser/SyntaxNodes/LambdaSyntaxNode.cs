using LambdaParser.Indexing;

namespace LambdaParser.SyntaxNodes;

public abstract class LambdaSyntaxNode
{
    public NodeLocation Location { get; }

    protected LambdaSyntaxNode(NodeLocation location)
    {
        Location = location;
    }
}