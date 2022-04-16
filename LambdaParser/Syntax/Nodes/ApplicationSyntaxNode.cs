using System.Collections.Immutable;
using LambdaParser.Syntax.Indexing;

namespace LambdaParser.Syntax.Nodes;

public class ApplicationSyntaxNode : ExpressionLambdaSyntaxNode
{
    public ExpressionLambdaSyntaxNode Function { get; }
    public ExpressionLambdaSyntaxNode Argument { get; }

    public ApplicationSyntaxNode(NodeLocation location, ExpressionLambdaSyntaxNode function, ExpressionLambdaSyntaxNode argument) : base(location)
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