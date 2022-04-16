using LambdaParser.SyntaxNodes;
using LambdaParser.Tools;

namespace LambdaParser.SemanticNodes;

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

    public override string ToString()
    {
        return $"{Constants.Lambda}{Argument}.{Body}";
    }
}