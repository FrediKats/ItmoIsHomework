using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Syntax;

public class LambdaSyntaxTree
{
    public LambdaSyntaxNode Root { get; }

    public LambdaSyntaxTree(LambdaSyntaxNode root)
    {
        Root = root;
    }
}