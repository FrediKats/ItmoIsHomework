using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Tools;

public static class SyntaxNodeExtensions
{
    public static ExpressionLambdaSyntaxNode UnwrapParenthesized(this ExpressionLambdaSyntaxNode expression)
    {
        if (expression is ParenthesizedSyntaxNode parenthesizedSyntaxNode)
            return parenthesizedSyntaxNode.Expression;

        return expression;
    }
}