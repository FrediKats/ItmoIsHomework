using System.Collections.Immutable;
using LambdaParser.Core.Syntax.Nodes;
using LambdaParser.Core.Tools;

namespace LambdaParser.Core.Semantic.Nodes;

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

    public override LambdaSyntaxNode GetSyntax()
    {
        return Syntax;
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