using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Semantic.Nodes;

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