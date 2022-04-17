using System.Text;
using LambdaParser.Semantic;
using LambdaParser.Semantic.Nodes;
using LambdaParser.Semantic.Reductions;
using LambdaParser.Syntax;
using LambdaParser.Syntax.Tools;
using LambdaParser.Visualization;
using Serilog;

Console.OutputEncoding = Encoding.Unicode;
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File("parser-log")
    .CreateLogger();

//AlphaReduction();
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
        ArgumentLambdaSemanticNode argumentLambdaSemanticNode = LambdaSemanticTreeFinder.Find<ArgumentLambdaSemanticNode>(lambdaSemanticTree.Root, _ => true) ?? throw new LambdaParseException("Cannot find node");
        LambdaSemanticTree semanticTree = new LambdaSemanticTreeReducing().AlphaReducing(lambdaSemanticTree, argumentLambdaSemanticNode, "z");

        Log.Information("Diff:");
        Log.Information(lambdaSyntaxNode.Node.ToString());
        Log.Information(semanticTree.Syntax.Root.ToString());
    }
}

void BetaReduction()
{
    var orTrueFalse = "λp.λq.(p p q)(λx.λy.x)(λx.λy.y)";
    var sourceCode = "(λx.x) ((λa.λb.a b) (λy.y) (λz.z))";
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

        var lambdaSemanticTreeReducing = new LambdaSemanticTreeReducing();
        LambdaSemanticTree lambdaSemanticTree = new SemanticParser().Parse(lambdaSyntaxTree);
        ApplicationSemanticNode argumentLambdaSemanticNode = LambdaSemanticTreeFinder.Find<ApplicationSemanticNode>(lambdaSemanticTree.Root, a => lambdaSemanticTreeReducing.IsCanBeReduced(a)) ?? throw new LambdaParseException("Cannot find node");
        LambdaSemanticTree betaReduced = lambdaSemanticTreeReducing.BetaReducing(lambdaSemanticTree, argumentLambdaSemanticNode);
        Log.Information("Diff:");
        Log.Information(lambdaSyntaxNode.Node.ToString());
        Log.Information(betaReduced.Syntax.Root.ToString());
    }
}