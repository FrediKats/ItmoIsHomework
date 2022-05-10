using LambdaParser.Core.Tools;

namespace LambdaParser.Core.Syntax.Nodes;

public class ArgumentLambdaSyntaxNode : LambdaSyntaxNode
{
    public string Value { get; }

    public ArgumentLambdaSyntaxNode(NodeLocation location, string value) : base(location)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}