using System.Collections.Immutable;
using LambdaParser.Core.Tools;

namespace LambdaParser.Core.Syntax.Nodes;

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

    public virtual string ToDetailedString()
    {
        return $"{ToString()} <- {this.GetType().Name}";
    }

    public LambdaSyntaxNode SetParent(LambdaSyntaxNode? parent)
    {
        Parent = parent;
        return this;
    }
}