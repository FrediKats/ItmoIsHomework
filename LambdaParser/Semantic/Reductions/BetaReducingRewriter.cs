using LambdaParser.Semantic.Nodes;
using LambdaParser.Syntax;
using LambdaParser.Syntax.Nodes;
using LambdaParser.Tools;

namespace LambdaParser.Semantic.Reductions;

public class BetaReducingRewriter : LambdaSyntaxTreeRewriter
{
    private readonly ApplicationSemanticNode _application;
    private readonly AbstractionLambdaSyntaxNode _syntaxNode;

    public BetaReducingRewriter(ApplicationSemanticNode application)
    {
        _application = application;
        _syntaxNode = (AbstractionLambdaSyntaxNode)_application.Syntax.Function.UnwrapParenthesized();
    }

    protected override ExpressionLambdaSyntaxNode Visit(ApplicationSyntaxNode applicationSyntaxNode)
    {
        if (applicationSyntaxNode != _application.Syntax)
            return base.Visit(applicationSyntaxNode);

        return VisitInternal(_syntaxNode.Body);
    }

    protected override ExpressionLambdaSyntaxNode Visit(ParameterLambdaSyntaxNode parameterLambdaSyntaxNode)
    {
        var abstractionLambdaSemanticNode = (AbstractionLambdaSemanticNode)_application.Function;
        if (abstractionLambdaSemanticNode.Argument.DependentParameters.Any(dp => dp.Syntax == parameterLambdaSyntaxNode))
        {
            return _application.Argument.GetSyntax();
        }

        return base.Visit(parameterLambdaSyntaxNode);
    }
}