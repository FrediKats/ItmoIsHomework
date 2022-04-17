using LambdaParser.Core.Syntax;
using LambdaParser.Semantic.Nodes;

namespace LambdaParser.Semantic;

public class LambdaSemanticTree
{
    public LambdaSyntaxTree Syntax { get; }

    public ExpressionLambdaSemanticNode Root { get; }

    public LambdaSemanticTree(LambdaSyntaxTree syntax, ExpressionLambdaSemanticNode root)
    {
        Syntax = syntax;
        Root = root;
    }
}