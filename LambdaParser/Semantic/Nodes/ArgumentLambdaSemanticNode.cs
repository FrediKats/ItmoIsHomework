using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Semantic.Nodes;

public class ArgumentLambdaSemanticNode : ExpressionLambdaSemanticNode
{
    private readonly List<ParameterLambdaSemanticNode> _parameters;

    public LetterLambdaSyntaxNode Syntax { get; }
    public IReadOnlyCollection<ParameterLambdaSemanticNode> DependentParameters => _parameters;

    public ArgumentLambdaSemanticNode(LetterLambdaSyntaxNode syntax)
    {
        Syntax = syntax;
        _parameters = new List<ParameterLambdaSemanticNode>();
    }

    public void AddParameter(ParameterLambdaSemanticNode parameter)
    {
        _parameters.Add(parameter);
    }

    public override string ToString()
    {
        return $"{Syntax}";
    }
}