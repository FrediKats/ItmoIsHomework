using LambdaParser.Semantic.Nodes;
using LambdaParser.Syntax;
using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Semantic.Reductions;

public class AlphaReducingRewriter : LambdaSyntaxTreeRewriter
{
    private readonly ArgumentLambdaSemanticNode _argumentLambda;
    private string _newName;

    public AlphaReducingRewriter(ArgumentLambdaSemanticNode argumentLambda, string newName)
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