using System.Collections.Immutable;
using LambdaParser.Core.Syntax.Nodes;

namespace LambdaParser.Core.Semantic.Nodes;

public abstract class ExpressionLambdaSemanticNode
{
    public abstract LambdaSyntaxNode GetSyntax();

    public virtual ImmutableArray<ExpressionLambdaSemanticNode> GetChildren()
    {
        return ImmutableArray<ExpressionLambdaSemanticNode>.Empty;
    }
}