using LambdaParser.Core.Syntax.Nodes;

namespace LambdaParser.Core.Syntax;

public static class SyntaxNodeExtensions
{
    public static LambdaSyntaxNode UnwrapParenthesized(this LambdaSyntaxNode expression)
    {
        if (expression is ParenthesizedSyntaxNode parenthesizedSyntaxNode)
            return parenthesizedSyntaxNode.Expression;

        return expression;
    }
}