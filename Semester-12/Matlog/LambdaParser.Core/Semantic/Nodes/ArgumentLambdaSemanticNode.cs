using LambdaParser.Core.Syntax.Nodes;

namespace LambdaParser.Core.Semantic.Nodes;

public class ArgumentLambdaSemanticNode : ExpressionLambdaSemanticNode
{
    private readonly List<ParameterLambdaSemanticNode> _parameters;

    public ArgumentLambdaSyntaxNode Syntax { get; }

    public IReadOnlyCollection<ParameterLambdaSemanticNode> DependentParameters => _parameters;

    public ArgumentLambdaSemanticNode(ArgumentLambdaSyntaxNode syntax)
    {
        Syntax = syntax;
        _parameters = new List<ParameterLambdaSemanticNode>();
    }

    public override LambdaSyntaxNode GetSyntax()
    {
        return Syntax;
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