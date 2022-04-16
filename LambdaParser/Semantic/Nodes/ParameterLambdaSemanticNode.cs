using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Semantic.Nodes;

public class ParameterLambdaSemanticNode : ExpressionLambdaSemanticNode
{
    public LetterLambdaSyntaxNode Syntax { get; }
    public ExpressionLambdaSemanticNode? Declaration { get; }

    public ParameterLambdaSemanticNode(LetterLambdaSyntaxNode syntax, ExpressionLambdaSemanticNode? declaration)
    {
        Syntax = syntax;
        Declaration = declaration;
    }

    public override string ToString()
    {
        return $"{Syntax}";
    }
}