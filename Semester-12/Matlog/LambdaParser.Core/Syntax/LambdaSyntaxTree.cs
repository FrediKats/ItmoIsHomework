using LambdaParser.Core.Syntax.Nodes;

namespace LambdaParser.Core.Syntax;

public class LambdaSyntaxTree
{
    public LambdaSyntaxNode Root { get; }

    public LambdaSyntaxTree(LambdaSyntaxNode root)
    {
        Root = root;
    }
}