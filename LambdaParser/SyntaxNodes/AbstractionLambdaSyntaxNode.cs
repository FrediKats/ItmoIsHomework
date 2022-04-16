using LambdaParser.Indexing;
using LambdaParser.Tools;

namespace LambdaParser.SyntaxNodes;

public class AbstractionLambdaSyntaxNode : ExpressionLambdaSyntaxNode
{
    public LetterLambdaSyntaxNode Argument { get; }
    public LambdaSyntaxNode Body { get; }

    public AbstractionLambdaSyntaxNode(NodeLocation location, LetterLambdaSyntaxNode argument, LambdaSyntaxNode body) : base(location)
    {
        Argument = argument;
        Body = body;
    }

    public override string ToString()
    {
        return $"{Constants.Lambda}{Argument}.({Body})";
    }
}