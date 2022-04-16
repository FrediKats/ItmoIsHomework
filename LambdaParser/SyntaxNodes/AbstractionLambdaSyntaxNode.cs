using System.Collections.Immutable;
using LambdaParser.Indexing;
using LambdaParser.Tools;

namespace LambdaParser.SyntaxNodes;

public class AbstractionLambdaSyntaxNode : ExpressionLambdaSyntaxNode
{
    public LetterLambdaSyntaxNode Argument { get; }
    public LambdaSyntaxNode Body { get; }

    public AbstractionLambdaSyntaxNode(NodeLocation location, LetterLambdaSyntaxNode argument, LambdaSyntaxNode body) : base(location)
    {
        Argument = argument;
        Body = body;
    }

    public override ImmutableArray<LambdaSyntaxNode> GetChildren()
    {
        return ImmutableArray<LambdaSyntaxNode>.Empty
            .Add(Argument)
            .Add(Body);
    }

    public override string ToString()
    {
        return $"{Constants.Lambda}{Argument}.({Body})";
    }
}