using LambdaParser.Semantic.Nodes;
using LambdaParser.Syntax;
using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Semantic;

public class AlphaReducing : LambdaSyntaxTreeRewriter
{
    private readonly ArgumentLambdaSemanticNode _argumentLambda;
    private string _newName;

    public AlphaReducing(ArgumentLambdaSemanticNode argumentLambda, string newName)
    {
        _argumentLambda = argumentLambda;
        _newName = newName;
    }

    protected override LetterLambdaSyntaxNode Visit(LetterLambdaSyntaxNode letterLambdaSyntaxNode)
    {
        if (_argumentLambda.DependentParameters.Any(dp => dp.Syntax == letterLambdaSyntaxNode)
            || _argumentLambda.Syntax == letterLambdaSyntaxNode)
        {
            return new LetterLambdaSyntaxNode(letterLambdaSyntaxNode.Location, _newName);
        }

        return base.Visit(letterLambdaSyntaxNode);
    }
}

public class LambdaSemanticTreeRenamer
{
    public LambdaSemanticTree Rename(LambdaSemanticTree tree, ArgumentLambdaSemanticNode argument, string newName)
    {
        var alphaReducing = new AlphaReducing(argument, newName);
        LambdaSyntaxTree lambdaSyntaxTree = alphaReducing.Visit(tree.Syntax);
        return new SemanticParser().Parse(lambdaSyntaxTree);
    }

}