using System.Text;
using LambdaParser.Core.Syntax;
using LambdaParser.Core.Syntax.Nodes;
using LambdaParser.Core.Tools;

namespace LambdaParser;

public class LambdaSyntaxTreeVisualization
{
    public static string Visualize(LambdaSyntaxTree tree)
    {
        var stringBuilder = new StringBuilder();

        Add(tree.Root, 0, stringBuilder);
        return stringBuilder.ToString();
    }

    private static void Add(LambdaSyntaxNode node, int level, StringBuilder stringBuilder)
    {
        var space = StringExtensions.FromChar('\t', level);
        stringBuilder.Append(space).Append(node.ToDetailedString()).AppendLine();
        foreach (LambdaSyntaxNode lambdaSyntaxNode in node.GetChildren())
        {
            Add(lambdaSyntaxNode, level + 1, stringBuilder);
        }
    }
}