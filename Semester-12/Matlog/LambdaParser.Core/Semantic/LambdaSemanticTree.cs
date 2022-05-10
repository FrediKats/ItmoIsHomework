using LambdaParser.Core.Semantic.Nodes;
using LambdaParser.Core.Syntax;

namespace LambdaParser.Core.Semantic;

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