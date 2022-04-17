using LambdaParser.Core.Syntax;
using LambdaParser.Core.Syntax.Nodes;

namespace LambdaParser.Core.TreeInteraction;

public abstract class LambdaSyntaxTreeRewriter
{
    public LambdaSyntaxTree Visit(LambdaSyntaxNode node)
    {
        return new LambdaSyntaxTree(VisitInternal(node));
    }

    protected LambdaSyntaxNode VisitInternal(LambdaSyntaxNode node)
    {
        return node switch
        {
            ApplicationSyntaxNode applicationSyntaxNode => Visit(applicationSyntaxNode),
            AbstractionLambdaSyntaxNode abstractionLambdaSyntaxNode => Visit(abstractionLambdaSyntaxNode),
            ArgumentLambdaSyntaxNode letterLambdaSyntaxNode => Visit(letterLambdaSyntaxNode),
            ParameterLambdaSyntaxNode parameterLambdaSyntaxNode => Visit(parameterLambdaSyntaxNode),
            ParenthesizedSyntaxNode parenthesizedSyntaxNode => Visit(parenthesizedSyntaxNode),
            _ => throw new ArgumentOutOfRangeException(nameof(node))
        };
    }

    protected virtual LambdaSyntaxNode Visit(ApplicationSyntaxNode applicationSyntaxNode)
    {
        LambdaSyntaxNode function = VisitInternal(applicationSyntaxNode.Function);
        LambdaSyntaxNode argument = VisitInternal(applicationSyntaxNode.Argument);
        return new ApplicationSyntaxNode(applicationSyntaxNode.Location, function, argument);
    }

    protected virtual LambdaSyntaxNode Visit(AbstractionLambdaSyntaxNode abstractionLambdaSyntaxNode)
    {
        ArgumentLambdaSyntaxNode argument = Visit(abstractionLambdaSyntaxNode.Argument);
        LambdaSyntaxNode body = VisitInternal(abstractionLambdaSyntaxNode.Body);
        return new AbstractionLambdaSyntaxNode(abstractionLambdaSyntaxNode.Location, argument, body);
    }

    protected virtual ArgumentLambdaSyntaxNode Visit(ArgumentLambdaSyntaxNode argumentLambdaSyntaxNode)
    {
        return argumentLambdaSyntaxNode;
    }

    protected virtual LambdaSyntaxNode Visit(ParenthesizedSyntaxNode parenthesizedSyntaxNode)
    {
        return new ParenthesizedSyntaxNode(parenthesizedSyntaxNode.Location, VisitInternal(parenthesizedSyntaxNode.Expression));
    }

    protected virtual LambdaSyntaxNode Visit(ParameterLambdaSyntaxNode parameterLambdaSyntaxNode)
    {
        return VisitInternal(parameterLambdaSyntaxNode);
    }
}