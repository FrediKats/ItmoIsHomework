using System.Collections.Immutable;
using LambdaParser.Core.Tools;

namespace LambdaParser.Syntax.Nodes;

public class ApplicationSyntaxNode : LambdaSyntaxNode
{
    public LambdaSyntaxNode Function { get; }
    public LambdaSyntaxNode Argument { get; }

    public ApplicationSyntaxNode(NodeLocation location, LambdaSyntaxNode function, LambdaSyntaxNode argument) : base(location)
    {
        Function = function;
        Argument = argument;

        Function.SetParent(this);
        Argument.SetParent(this);
    }

    public override ImmutableArray<LambdaSyntaxNode> GetChildren()
    {
        return ImmutableArray<LambdaSyntaxNode>.Empty
            .Add(Function)
            .Add(Argument);
    }

    public override string ToString()
    {
        return $"{Function} {Argument}";
    }
}