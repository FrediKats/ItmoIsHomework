using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Syntax;

public static class SyntaxNodeExtensions
{
    public static LambdaSyntaxNode UnwrapParenthesized(this LambdaSyntaxNode expression)
    {
        if (expression is ParenthesizedSyntaxNode parenthesizedSyntaxNode)
            return parenthesizedSyntaxNode.Expression;

        return expression;
    }
}