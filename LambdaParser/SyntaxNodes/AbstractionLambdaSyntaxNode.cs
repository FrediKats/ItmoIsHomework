namespace LambdaParser.LambdaSyntaxNodes;

public class AbstractionLambdaSyntaxNode : LambdaSyntaxNode
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
        return $"({Constants.Lambda}) {Argument}.({Body})";
    }
}