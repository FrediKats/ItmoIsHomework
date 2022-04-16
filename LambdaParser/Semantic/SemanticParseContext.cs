using LambdaParser.Semantic.Nodes;
using LambdaParser.Semantic.Tools;
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