using LambdaParser.Core.Semantic.Nodes;
using LambdaParser.Core.Semantic;
using LambdaParser.Core.Syntax;
using LambdaParser.Core.TreeInteraction;

namespace LambdaParser.Core.Reductions;

public class BetaReducer
{
    public IReadOnlyCollection<ApplicationSemanticNode> FindAcceptableNodes(LambdaSemanticTree tree)
    {
        return LambdaSemanticTreeFinder.FindAll<ApplicationSemanticNode>(tree.Root, a => a.Function is AbstractionLambdaSemanticNode);
    }

    public LambdaSemanticTree BetaReducing(LambdaSemanticTree tree, ApplicationSemanticNode application)
    {
        var betaReducingRewriter = new BetaReducingRewriter(application);
        LambdaSyntaxTree lambdaSyntaxTree = betaReducingRewriter.Visit(tree.Syntax.Root);
        return new SemanticParser().Parse(lambdaSyntaxTree);
    }
}