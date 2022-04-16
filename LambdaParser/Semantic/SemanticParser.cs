using LambdaParser.Semantic.Nodes;
using LambdaParser.Syntax;
using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Semantic;

public class SemanticParser
{
    public LambdaSemanticTree Parse(LambdaSyntaxTree tree)
    {
        LambdaSyntaxNode lambdaSyntaxNode = tree.Root;
        var semanticParseContext = new SemanticParseContext();
        return new LambdaSemanticTree(tree, Parse(lambdaSyntaxNode, semanticParseContext));
    }

    public ExpressionLambdaSemanticNode Parse(LambdaSyntaxNode node, SemanticParseContext context)
    {
        switch (node)
        {
            case ApplicationSyntaxNode applicationSyntaxNode:
                ExpressionLambdaSemanticNode function = Parse(applicationSyntaxNode.Function, context);
                ExpressionLambdaSemanticNode argument = Parse(applicationSyntaxNode.Argument, context);
                return new ApplicationSemanticNode(applicationSyntaxNode, function, argument);

            case AbstractionLambdaSyntaxNode abstractionLambdaSyntaxNode:
                var argumentLambdaSemanticNode = new ArgumentLambdaSemanticNode(abstractionLambdaSyntaxNode.Argument);
                context.RegisterParameter(argumentLambdaSemanticNode);
                ExpressionLambdaSemanticNode expressionLambdaSemanticNode = Parse(abstractionLambdaSyntaxNode.Body, context);
                context.UnRegisterParameter(argumentLambdaSemanticNode);
                return new AbstractionLambdaSemanticNode(abstractionLambdaSyntaxNode, argumentLambdaSemanticNode, expressionLambdaSemanticNode);

            case LetterLambdaSyntaxNode letterLambdaSyntaxNode:
                context.TryResolve(letterLambdaSyntaxNode, out ArgumentLambdaSemanticNode? result);
                var parameterLambdaSemanticNode = new ParameterLambdaSemanticNode(letterLambdaSyntaxNode, result);
                if (result is not null)
                    result.AddParameter(parameterLambdaSemanticNode);
                return parameterLambdaSemanticNode;

            case ParenthesizedSyntaxNode parenthesizedSyntaxNode:
                return Parse(parenthesizedSyntaxNode.Expression, context);

            default:
                throw new ArgumentOutOfRangeException(nameof(node));
        }
    }
}