public class LetterLambdaSyntaxNode : ExpressionLambdaSyntaxNode
{
    public string Value { get; }

    public LetterLambdaSyntaxNode(NodeLocation location, string value) : base(location)
    {
        Value = value;
    }
}