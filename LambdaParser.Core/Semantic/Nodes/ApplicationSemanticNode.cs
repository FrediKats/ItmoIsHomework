using System.Collections.Immutable;
using LambdaParser.Core.Syntax.Nodes;

namespace LambdaParser.Core.Semantic.Nodes;

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

    public override LambdaSyntaxNode GetSyntax()
    {
        return Syntax;
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