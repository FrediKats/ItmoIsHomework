using LambdaParser.Core.Semantic;
using LambdaParser.Core.Semantic.Nodes;
using LambdaParser.Core.Syntax;

namespace LambdaParser.Core.Reductions;

public class LambdaSemanticTreeReducing
{
    public LambdaSemanticTree AlphaReducing(LambdaSemanticTree tree, ArgumentLambdaSemanticNode argument, string newName)
    {
        var alphaReducing = new AlphaReducingRewriter(argument, newName);
        LambdaSyntaxTree lambdaSyntaxTree = alphaReducing.Visit(tree.Syntax.Root);
        return new SemanticParser().Parse(lambdaSyntaxTree);
    }

    public LambdaSemanticTree BetaReducing(LambdaSemanticTree tree, ApplicationSemanticNode application)
    {
        var betaReducingRewriter = new BetaReducingRewriter(application);
        LambdaSyntaxTree lambdaSyntaxTree = betaReducingRewriter.Visit(tree.Syntax.Root);
        return new SemanticParser().Parse(lambdaSyntaxTree);
    }

    public bool IsCanBeReduced(ApplicationSemanticNode application)
    {
        return application.Function is AbstractionLambdaSemanticNode;
    }
}