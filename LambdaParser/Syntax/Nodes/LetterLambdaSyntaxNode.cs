using LambdaParser.Syntax.Indexing;

namespace LambdaParser.Syntax.Nodes;

public class LetterLambdaSyntaxNode : ExpressionLambdaSyntaxNode
{
    public string Value { get; }

    public LetterLambdaSyntaxNode(NodeLocation location, string value) : base(location)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}