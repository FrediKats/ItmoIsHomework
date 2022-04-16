using LambdaParser.Semantic.Nodes;
using LambdaParser.Syntax;

namespace LambdaParser.Semantic.Reductions;

public class LambdaSemanticTreeAlphaReducing
{
    public LambdaSemanticTree Rename(LambdaSemanticTree tree, ArgumentLambdaSemanticNode argument, string newName)
    {
        var alphaReducing = new AlphaReducingRewriter(argument, newName);
        LambdaSyntaxTree lambdaSyntaxTree = alphaReducing.Visit(tree.Syntax);
        return new SemanticParser().Parse(lambdaSyntaxTree);
    }

}