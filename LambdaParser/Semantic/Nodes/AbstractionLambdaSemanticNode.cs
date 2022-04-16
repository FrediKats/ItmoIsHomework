using LambdaParser.Syntax.Nodes;
using LambdaParser.Tools;
using System.Collections.Immutable;

namespace LambdaParser.Semantic.Nodes;

public class AbstractionLambdaSemanticNode : ExpressionLambdaSemanticNode
{
    public AbstractionLambdaSyntaxNode Syntax { get; }

    public ArgumentLambdaSemanticNode Argument { get; }
    public ExpressionLambdaSemanticNode Body { get; }

    public AbstractionLambdaSemanticNode(AbstractionLambdaSyntaxNode syntax, ArgumentLambdaSemanticNode argument, ExpressionLambdaSemanticNode body)
    {
        Syntax = syntax;
        Argument = argument;
        Body = body;
    }

    public override ImmutableArray<ExpressionLambdaSemanticNode> GetChildren()
    {
        return ImmutableArray<ExpressionLambdaSemanticNode>.Empty
            .Add(Argument)
            .Add(Body);
    }

    public override string ToString()
    {
        return $"{Constants.Lambda}{Argument}.{Body}";
    }
}