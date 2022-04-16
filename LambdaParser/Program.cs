using System.Text;
using LambdaParser.Semantic;
using LambdaParser.Semantic.Nodes;
using LambdaParser.Semantic.Reductions;
using LambdaParser.Syntax;
using LambdaParser.Syntax.Visualization;
using Serilog;


Console.OutputEncoding = Encoding.Unicode;
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File("parser-log")
    .CreateLogger();

AlphaReduction();
BetaReduction();

void AlphaReduction()
{
    var λfΛxFFXSqr = "(((λf . (λx . (f (f x)))) sqr) a)";
    var sourceCode = λfΛxFFXSqr;
    var lambdaSyntaxNode = LambdaSyntaxTreeParser.Parse(sourceCode);
    if (lambdaSyntaxNode.HasError)
    {
        SyntaxWalkerLogger.LogIt(sourceCode, lambdaSyntaxNode.Error);
    }
    else
    {
        Log.Information($"Result - {lambdaSyntaxNode.Node}");
        var lambdaSyntaxTree = new LambdaSyntaxTree(lambdaSyntaxNode.Node);

        LambdaSemanticTree lambdaSemanticTree = new SemanticParser().Parse(lambdaSyntaxTree);
        LambdaSemanticTree semanticTree = new LambdaSemanticTreeAlphaReducing().Rename(lambdaSemanticTree, FindArgument(lambdaSemanticTree.Root), "z");

        Log.Information("Diff:");
        Log.Information(lambdaSyntaxNode.Node.ToString());
        Log.Information(semanticTree.Syntax.Root.ToString());
    }

    ArgumentLambdaSemanticNode? FindArgument(ExpressionLambdaSemanticNode lambdaSemanticNode)
    {
        if (lambdaSemanticNode is ArgumentLambdaSemanticNode argumentLambdaNode)
            return argumentLambdaNode;

        foreach (var child in lambdaSemanticNode.GetChildren())
        {
            ArgumentLambdaSemanticNode? argumentLambdaSemanticNode = FindArgument(child);
            if (argumentLambdaSemanticNode is not null)
                return argumentLambdaSemanticNode;
        }

        return null;
    }
}

void BetaReduction()
{
    var orTrueFalse = "λp.λq.(p p q)(λx.λy.x)(λx.λy.y)";
    var sourceCode = orTrueFalse;
    var lambdaSyntaxNode = LambdaSyntaxTreeParser.Parse(sourceCode);
    if (lambdaSyntaxNode.HasError)
    {
        SyntaxWalkerLogger.LogIt(sourceCode, lambdaSyntaxNode.Error);
    }
    else
    {
        Log.Information($"Result - {lambdaSyntaxNode.Node}");
        var lambdaSyntaxTree = new LambdaSyntaxTree(lambdaSyntaxNode.Node);
        var visualize = LambdaSyntaxTreeVisualization.Visualize(lambdaSyntaxTree);
        Log.Information($"Tree:\n{visualize}");

        LambdaSemanticTree lambdaSemanticTree = new SemanticParser().Parse(lambdaSyntaxTree);
        Log.Information("Diff:");
        Log.Information(lambdaSyntaxNode.Node.ToString());
        return;
    }
    return;
}