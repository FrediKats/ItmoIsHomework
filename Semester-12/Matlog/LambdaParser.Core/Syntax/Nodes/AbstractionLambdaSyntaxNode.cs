using System.Collections.Immutable;
using LambdaParser.Core.Tools;

namespace LambdaParser.Core.Syntax.Nodes;

public class AbstractionLambdaSyntaxNode : LambdaSyntaxNode
{
    public ArgumentLambdaSyntaxNode Argument { get; }
    public LambdaSyntaxNode Body { get; }

    public AbstractionLambdaSyntaxNode(NodeLocation location, ArgumentLambdaSyntaxNode argument, LambdaSyntaxNode body) : base(location)
    {
        Argument = argument;
        Body = body;

        Argument.SetParent(this);
        Body.SetParent(this);
    }

    public override ImmutableArray<LambdaSyntaxNode> GetChildren()
    {
        return ImmutableArray<LambdaSyntaxNode>.Empty
            .Add(Argument)
            .Add(Body);
    }

    public override string ToString()
    {
        return $"{Constants.Lambda}{Argument}.{Body}";
    }
}