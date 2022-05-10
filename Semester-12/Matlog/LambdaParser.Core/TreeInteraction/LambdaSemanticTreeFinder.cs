using LambdaParser.Core.Semantic.Nodes;

namespace LambdaParser.Core.TreeInteraction;

public static class LambdaSemanticTreeFinder
{
    public static IReadOnlyCollection<T> FindAll<T>(ExpressionLambdaSemanticNode root, Func<T, bool> predicate) where T : ExpressionLambdaSemanticNode
    {
        var result = new List<T>();
        FindAllRecursive(root);
        return result;

        void FindAllRecursive(ExpressionLambdaSemanticNode currentNode)
        {
            if (currentNode is T value && predicate(value))
            {
                result.Add(value);
            }

            foreach (ExpressionLambdaSemanticNode childNode in currentNode.GetChildren())
            {
                FindAllRecursive(childNode);
            }
        }
    }
}