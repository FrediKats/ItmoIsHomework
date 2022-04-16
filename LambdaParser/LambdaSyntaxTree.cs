using LambdaParser.SyntaxNodes;

namespace LambdaParser;

public class LambdaSyntaxTree
{
    public LambdaSyntaxNode Root { get; }

    public LambdaSyntaxTree(LambdaSyntaxNode root)
    {
        Root = root;
    }
}