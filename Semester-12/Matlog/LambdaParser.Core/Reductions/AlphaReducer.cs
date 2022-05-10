using LambdaParser.Core.Semantic.Nodes;
using LambdaParser.Core.Semantic;
using LambdaParser.Core.TreeInteraction;
using LambdaParser.Core.Syntax;

namespace LambdaParser.Core.Reductions;

public class AlphaReducer
{
    public IReadOnlyCollection<ArgumentLambdaSemanticNode> FindAcceptableNodes(LambdaSemanticTree tree)
    {
        return LambdaSemanticTreeFinder.FindAll<ArgumentLambdaSemanticNode>(tree.Root, _ => true);
    }

    public LambdaSemanticTree AlphaReducing(LambdaSemanticTree tree, ArgumentLambdaSemanticNode argument, string newName)
    {
        var alphaReducing = new AlphaReducingRewriter(argument, newName);
        LambdaSyntaxTree lambdaSyntaxTree = alphaReducing.Visit(tree.Syntax.Root);
        return new SemanticParser().Parse(lambdaSyntaxTree);
    }
}