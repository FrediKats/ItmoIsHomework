using LambdaParser.Core.Semantic.Nodes;

namespace LambdaParser;

public class LambdaSemanticTreeFinder
{
    public static T? Find<T>(ExpressionLambdaSemanticNode root, Func<T, bool> predicate) where T : ExpressionLambdaSemanticNode
    {
        if (root is T value && predicate(value))
            return value;

        foreach (ExpressionLambdaSemanticNode expressionLambdaSemanticNode in root.GetChildren())
        {
            if (expressionLambdaSemanticNode is T innerValue && predicate(innerValue))
                return innerValue;
        }

        return null;
    }
}