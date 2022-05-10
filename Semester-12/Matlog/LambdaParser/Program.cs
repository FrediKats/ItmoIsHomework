using System.Text;
using LambdaParser;
using LambdaParser.Core.Reductions;
using LambdaParser.Core.Semantic;
using LambdaParser.Core.Semantic.Nodes;
using LambdaParser.Core.Syntax;
using LambdaParser.Core.Syntax.Tools;
using LambdaParser.Core.TreeInteraction;
using Serilog;

Console.OutputEncoding = Encoding.Unicode;
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File("parser-log")
    .CreateLogger();

//Parse();
//AlphaReduction();
BetaReduction();

void Parse()
{
    var sourceCode = "λn.λm.λs.λz.n s (m s z)";
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

        Log.Information("Diff:");
        Log.Information(sourceCode);
        Log.Information(lambdaSyntaxNode.Node.ToString());
    }
}

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
        Log.Information("Diff:");
        Log.Information(sourceCode);
        Log.Information(lambdaSyntaxNode.Node.ToString());

        LambdaSemanticTree lambdaSemanticTree = new SemanticParser().Parse(lambdaSyntaxTree);
        var alphaReducer = new AlphaReducer();
        ArgumentLambdaSemanticNode argumentLambdaSemanticNode = alphaReducer.FindAcceptableNodes(lambdaSemanticTree).FirstOrDefault() ?? throw new LambdaParseException("Cannot find node");
        LambdaSemanticTree semanticTree = alphaReducer.AlphaReducing(lambdaSemanticTree, argumentLambdaSemanticNode, "z");

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

        Log.Information("Diff:");
        Log.Information(sourceCode);
        Log.Information(lambdaSyntaxNode.Node.ToString());

        var betaReducer = new BetaReducer();
        LambdaSemanticTree lambdaSemanticTree = new SemanticParser().Parse(lambdaSyntaxTree);
        ApplicationSemanticNode argumentLambdaSemanticNode = betaReducer.FindAcceptableNodes(lambdaSemanticTree).FirstOrDefault() ?? throw new LambdaParseException("Cannot find node");
        LambdaSemanticTree betaReduced = betaReducer.BetaReducing(lambdaSemanticTree, argumentLambdaSemanticNode);
        Log.Information("Diff:");
        Log.Information(lambdaSyntaxNode.Node.ToString());
        Log.Information(betaReduced.Syntax.Root.ToString());
    }
}