using LambdaParser.SyntaxNodes;

namespace LambdaParser.SemanticNodes;

public class ArgumentLambdaSemanticNode : ExpressionLambdaSemanticNode
{
    public LetterLambdaSyntaxNode Syntax { get; }

    public ArgumentLambdaSemanticNode(LetterLambdaSyntaxNode syntax)
    {
        Syntax = syntax;
    }

    public override string ToString()
    {
        return $"{Syntax}";
    }
}