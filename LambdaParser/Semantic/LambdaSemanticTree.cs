using LambdaParser.Semantic.Nodes;

namespace LambdaParser.Semantic;

public class LambdaSemanticTree
{
    public ExpressionLambdaSemanticNode Root { get; }

    public LambdaSemanticTree(ExpressionLambdaSemanticNode root)
    {
        Root = root;
    }
}