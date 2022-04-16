using System.Collections.Immutable;
using LambdaParser.Syntax.Indexing;

namespace LambdaParser.Syntax.Nodes;

public abstract class LambdaSyntaxNode
{
    public LambdaSyntaxNode? Parent { get; set; }
    public NodeLocation Location { get; }

    protected LambdaSyntaxNode(LambdaSyntaxNode? parent, NodeLocation location)
    {
        Parent = parent;
        Location = location;
    }

    protected LambdaSyntaxNode(NodeLocation location)
    {
        Parent = null;
        Location = location;
    }

    public virtual ImmutableArray<LambdaSyntaxNode> GetChildren()
    {
        return ImmutableArray<LambdaSyntaxNode>.Empty;
    }

    public LambdaSyntaxNode SetParent(LambdaSyntaxNode? parent)
    {
        Parent = parent;
        return this;
    }
}