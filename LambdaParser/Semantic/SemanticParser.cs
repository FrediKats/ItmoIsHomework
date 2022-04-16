using LambdaParser.Semantic.Nodes;
using LambdaParser.Semantic.Tools;
using LambdaParser.Syntax;
using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Semantic;

public class SemanticParseContext
{
    private readonly Dictionary<string, ArgumentLambdaSemanticNode> _parameters = new Dictionary<string, ArgumentLambdaSemanticNode>();

    public void RegisterParameter(ArgumentLambdaSemanticNode argument)
    {
        if (_parameters.ContainsKey(argument.Syntax.Value))
        {
            throw new LambdaSemanticParseException($"Parameter {argument.Syntax.Value} already exists in semantic parse context");
        }

        _parameters[argument.Syntax.Value] = argument;
    }

    public void UnRegisterParameter(ArgumentLambdaSemanticNode argument)
    {
        if (!_parameters.ContainsKey(argument.Syntax.Value))
        {
            throw new LambdaSemanticParseException($"Parameter {argument.Syntax.Value} was not found in semantic parse context");
        }

        _parameters.Remove(argument.Syntax.Value);
    }

    public bool TryResolve(LetterLambdaSyntaxNode parameter, out ArgumentLambdaSemanticNode? argument)
    {
        return _parameters.TryGetValue(parameter.Value, out argument);
    }
}

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