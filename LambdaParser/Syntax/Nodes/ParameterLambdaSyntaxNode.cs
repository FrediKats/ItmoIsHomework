using LambdaParser.Syntax.Indexing;

namespace LambdaParser.Syntax.Nodes;

public class ParameterLambdaSyntaxNode : LambdaSyntaxNode
{
    public string Value { get; }

    public ParameterLambdaSyntaxNode(NodeLocation location, string value) : base(location)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}