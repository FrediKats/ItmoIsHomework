using LambdaParser.Syntax.Nodes;
using System.Collections.Immutable;

namespace LambdaParser.Semantic.Nodes;

public class ApplicationSemanticNode : ExpressionLambdaSemanticNode
{
    public ApplicationSyntaxNode Syntax { get; }

    public ExpressionLambdaSemanticNode Function { get; }
    public ExpressionLambdaSemanticNode Argument { get; }

    public ApplicationSemanticNode(ApplicationSyntaxNode syntax, ExpressionLambdaSemanticNode function, ExpressionLambdaSemanticNode argument)
    {
        Syntax = syntax;
        Function = function;
        Argument = argument;
    }

    public override ImmutableArray<ExpressionLambdaSemanticNode> GetChildren()
    {
        return ImmutableArray<ExpressionLambdaSemanticNode>.Empty
            .Add(Function)
            .Add(Argument);
    }

    public override string ToString()
    {
        return $"{Function} {Argument}";
    }
}