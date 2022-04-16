using System.Collections.Immutable;
using LambdaParser.Syntax.Indexing;
using LambdaParser.Tools;

namespace LambdaParser.Syntax.Nodes;

public class AbstractionLambdaSyntaxNode : ExpressionLambdaSyntaxNode
{
    public LetterLambdaSyntaxNode Argument { get; }
    public ExpressionLambdaSyntaxNode Body { get; }

    public AbstractionLambdaSyntaxNode(NodeLocation location, LetterLambdaSyntaxNode argument, ExpressionLambdaSyntaxNode body) : base(location)
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