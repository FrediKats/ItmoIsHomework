using System.Collections.Immutable;

public class FuncInvokeSyntaxNode : ExpressionLambdaSyntaxNode
{
    public LetterLambdaSyntaxNode FunctionName { get; }
    public ImmutableArray<ExpressionLambdaSyntaxNode> Arguments { get; }

    public FuncInvokeSyntaxNode(NodeLocation location, LetterLambdaSyntaxNode functionName, ImmutableArray<ExpressionLambdaSyntaxNode> arguments) : base(location)
    {
        FunctionName = functionName;
        Arguments = arguments;
    }
}