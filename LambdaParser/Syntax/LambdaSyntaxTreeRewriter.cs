using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Syntax;

public abstract class LambdaSyntaxTreeRewriter
{
    public LambdaSyntaxTree Visit(LambdaSyntaxNode node)
    {
        return new LambdaSyntaxTree(VisitInternal(node));
    }

    protected ExpressionLambdaSyntaxNode VisitInternal(LambdaSyntaxNode node)
    {
        return node switch
        {
            ApplicationSyntaxNode applicationSyntaxNode => Visit(applicationSyntaxNode),
            AbstractionLambdaSyntaxNode abstractionLambdaSyntaxNode => Visit(abstractionLambdaSyntaxNode),
            ArgumentLambdaSyntaxNode letterLambdaSyntaxNode => Visit(letterLambdaSyntaxNode),
            ParameterLambdaSyntaxNode parameterLambdaSyntaxNode => Visit(parameterLambdaSyntaxNode),
            ParenthesizedSyntaxNode parenthesizedSyntaxNode => Visit(parenthesizedSyntaxNode),
            ExpressionLambdaSyntaxNode expressionLambdaSyntaxNode => Visit(expressionLambdaSyntaxNode),
            _ => throw new ArgumentOutOfRangeException(nameof(node))
        };
    }

    protected virtual ExpressionLambdaSyntaxNode Visit(ApplicationSyntaxNode applicationSyntaxNode)
    {
        ExpressionLambdaSyntaxNode function = VisitInternal(applicationSyntaxNode.Function);
        ExpressionLambdaSyntaxNode argument = VisitInternal(applicationSyntaxNode.Argument);
        return new ApplicationSyntaxNode(applicationSyntaxNode.Location, function, argument);
    }

    protected virtual ExpressionLambdaSyntaxNode Visit(AbstractionLambdaSyntaxNode abstractionLambdaSyntaxNode)
    {
        ArgumentLambdaSyntaxNode argument = Visit(abstractionLambdaSyntaxNode.Argument);
        ExpressionLambdaSyntaxNode body = VisitInternal(abstractionLambdaSyntaxNode.Body);
        return new AbstractionLambdaSyntaxNode(abstractionLambdaSyntaxNode.Location, argument, body);
    }

    protected virtual ArgumentLambdaSyntaxNode Visit(ArgumentLambdaSyntaxNode argumentLambdaSyntaxNode)
    {
        return argumentLambdaSyntaxNode;
    }

    protected virtual ExpressionLambdaSyntaxNode Visit(ParenthesizedSyntaxNode parenthesizedSyntaxNode)
    {
        return new ParenthesizedSyntaxNode(parenthesizedSyntaxNode.Location, VisitInternal(parenthesizedSyntaxNode.Expression));
    }

    protected virtual ExpressionLambdaSyntaxNode Visit(ExpressionLambdaSyntaxNode expressionLambdaSyntaxNode)
    {
        return expressionLambdaSyntaxNode;
    }

    protected virtual ExpressionLambdaSyntaxNode Visit(ParameterLambdaSyntaxNode parameterLambdaSyntaxNode)
    {
        return VisitInternal(parameterLambdaSyntaxNode);
    }
}