using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Syntax;

public abstract class LambdaSyntaxTreeRewriter
{
    public LambdaSyntaxTree Visit(LambdaSyntaxTree tree)
    {
        return new LambdaSyntaxTree(VisitInternal(tree.Root));
    }

    private ExpressionLambdaSyntaxNode VisitInternal(LambdaSyntaxNode node)
    {
        return node switch
        {
            ApplicationSyntaxNode applicationSyntaxNode => Visit(applicationSyntaxNode),
            AbstractionLambdaSyntaxNode abstractionLambdaSyntaxNode => Visit(abstractionLambdaSyntaxNode),
            LetterLambdaSyntaxNode letterLambdaSyntaxNode => Visit(letterLambdaSyntaxNode),
            ParenthesizedSyntaxNode parenthesizedSyntaxNode => Visit(parenthesizedSyntaxNode),
            ExpressionLambdaSyntaxNode expressionLambdaSyntaxNode => Visit(expressionLambdaSyntaxNode),
            _ => throw new ArgumentOutOfRangeException(nameof(node))
        };
    }

    protected virtual ApplicationSyntaxNode Visit(ApplicationSyntaxNode applicationSyntaxNode)
    {
        ExpressionLambdaSyntaxNode function = VisitInternal(applicationSyntaxNode.Function);
        ExpressionLambdaSyntaxNode argument = VisitInternal(applicationSyntaxNode.Argument);
        return new ApplicationSyntaxNode(applicationSyntaxNode.Location, function, argument);
    }

    protected virtual AbstractionLambdaSyntaxNode Visit(AbstractionLambdaSyntaxNode abstractionLambdaSyntaxNode)
    {
        LetterLambdaSyntaxNode argument = Visit(abstractionLambdaSyntaxNode.Argument);
        ExpressionLambdaSyntaxNode body = VisitInternal(abstractionLambdaSyntaxNode.Body);
        return new AbstractionLambdaSyntaxNode(abstractionLambdaSyntaxNode.Location, argument, body);
    }

    protected virtual LetterLambdaSyntaxNode Visit(LetterLambdaSyntaxNode letterLambdaSyntaxNode)
    {
        return letterLambdaSyntaxNode;
    }

    protected virtual ParenthesizedSyntaxNode Visit(ParenthesizedSyntaxNode parenthesizedSyntaxNode)
    {
        return new ParenthesizedSyntaxNode(parenthesizedSyntaxNode.Location, VisitInternal(parenthesizedSyntaxNode.Expression));
    }

    protected virtual ExpressionLambdaSyntaxNode Visit(ExpressionLambdaSyntaxNode expressionLambdaSyntaxNode)
    {
        return expressionLambdaSyntaxNode;
    }
}