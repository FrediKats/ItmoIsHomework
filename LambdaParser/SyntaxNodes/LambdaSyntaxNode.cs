using System.Collections.Immutable;
using LambdaParser.Indexing;

namespace LambdaParser.SyntaxNodes;

public abstract class LambdaSyntaxNode
{
    public NodeLocation Location { get; }

    protected LambdaSyntaxNode(NodeLocation location)
    {
        Location = location;
    }

    public virtual ImmutableArray<LambdaSyntaxNode> GetChildren()
    {
        return ImmutableArray<LambdaSyntaxNode>.Empty;
    }
}