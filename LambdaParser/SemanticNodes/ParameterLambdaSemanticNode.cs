using LambdaParser.SyntaxNodes;

namespace LambdaParser.SemanticNodes;

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