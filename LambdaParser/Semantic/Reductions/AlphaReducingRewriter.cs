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

    protected override ExpressionLambdaSyntaxNode Visit(ParameterLambdaSyntaxNode parameterLambdaSyntaxNode)
    {
        if (_argumentLambda.DependentParameters.Any(dp => dp.Syntax == parameterLambdaSyntaxNode))
        {
            return new ParameterLambdaSyntaxNode(parameterLambdaSyntaxNode.Location, _newName);
        }

        return base.Visit(parameterLambdaSyntaxNode);
    }

    protected override ArgumentLambdaSyntaxNode Visit(ArgumentLambdaSyntaxNode argumentLambdaSyntaxNode)
    {
        if (_argumentLambda.Syntax == argumentLambdaSyntaxNode)
        {
            return new ArgumentLambdaSyntaxNode(argumentLambdaSyntaxNode.Location, _newName);
        }

        return base.Visit(argumentLambdaSyntaxNode);
    }
}