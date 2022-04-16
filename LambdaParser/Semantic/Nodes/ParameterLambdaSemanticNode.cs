using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Semantic.Nodes;

public class ParameterLambdaSemanticNode : ExpressionLambdaSemanticNode
{
    public ParameterLambdaSyntaxNode Syntax { get; }

    public ExpressionLambdaSemanticNode? Declaration { get; }

    public ParameterLambdaSemanticNode(ParameterLambdaSyntaxNode syntax, ExpressionLambdaSemanticNode? declaration)
    {
        Syntax = syntax;
        Declaration = declaration;
    }

    public override ExpressionLambdaSyntaxNode GetSyntax()
    {
        return Syntax;
    }

    public override string ToString()
    {
        return $"{Syntax}";
    }
}