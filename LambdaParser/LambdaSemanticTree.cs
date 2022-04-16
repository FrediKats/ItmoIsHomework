using LambdaParser.SemanticNodes;

namespace LambdaParser;

public class LambdaSemanticTree
{
    public ExpressionLambdaSemanticNode Root { get; }

    public LambdaSemanticTree(ExpressionLambdaSemanticNode root)
    {
        Root = root;
    }
}