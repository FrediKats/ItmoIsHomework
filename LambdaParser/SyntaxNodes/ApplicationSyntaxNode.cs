using System.Collections.Immutable;
using System.Text;
using LambdaParser.Indexing;

namespace LambdaParser.SyntaxNodes;

public class ApplicationSyntaxNode : ExpressionLambdaSyntaxNode
{
    public ExpressionLambdaSyntaxNode Function { get; }
    public ImmutableArray<ExpressionLambdaSyntaxNode> Arguments { get; }

    public ApplicationSyntaxNode(NodeLocation location, ExpressionLambdaSyntaxNode function, ImmutableArray<ExpressionLambdaSyntaxNode> arguments) : base(location)
    {
        Function = function;
        Arguments = arguments;
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append($"{Function} ")
            .AppendJoin(' ', Arguments)
            .ToString();
    }
}