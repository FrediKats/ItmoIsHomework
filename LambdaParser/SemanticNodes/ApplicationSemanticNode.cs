using LambdaParser.SyntaxNodes;

namespace LambdaParser.SemanticNodes;

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

    public override string ToString()
    {
        return $"{Function} {Argument}";
    }
}