using System.Collections.Immutable;

namespace LambdaParser.Semantic.Nodes;

public class ExpressionLambdaSemanticNode
{
    public virtual ImmutableArray<ExpressionLambdaSemanticNode> GetChildren()
    {
        return ImmutableArray<ExpressionLambdaSemanticNode>.Empty;
    }
}