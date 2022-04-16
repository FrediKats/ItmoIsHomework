using LambdaParser.LambdaSyntaxNodes;

namespace LambdaParser;

public class LambdaSyntaxTree
{
    public LambdaSyntaxNode Root { get; }

    public LambdaSyntaxTree(LambdaSyntaxNode root)
    {
        Root = root;
        
    }
}