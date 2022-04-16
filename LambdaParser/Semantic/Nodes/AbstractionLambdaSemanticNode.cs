using LambdaParser.Syntax.Nodes;
using LambdaParser.Tools;

namespace LambdaParser.Semantic.Nodes;

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