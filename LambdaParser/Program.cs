using System.Text;
using LambdaParser.Semantic;
using LambdaParser.Semantic.Nodes;
using LambdaParser.Syntax;
using LambdaParser.Syntax.Parsers;
using LambdaParser.Syntax.Visualization;
using Serilog;

var predecessor = "λn.λf.λx.n (λg.λh.h (g f)) (λu.x) (λu.u)";
var λnΛfΛxFNFX = "λn.λf.λx.f (n f x)";
var λfΛxFFXSqr = "(((λf . (λx . (f (f x)))) sqr) a)";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File("parser-log")
    .CreateLogger();

Console.OutputEncoding = Encoding.Unicode;

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
    var visualize = LambdaSyntaxTreeVisualization.Visualize(lambdaSyntaxTree);
    Log.Information($"Tree:\n{visualize}");

    LambdaSemanticTree lambdaSemanticTree = new SemanticParser().Parse(lambdaSyntaxTree);
    LambdaSemanticTree semanticTree = new LambdaSemanticTreeRenamer().Rename(lambdaSemanticTree, FindArgument(lambdaSemanticTree.Root), "z");

    Log.Information("Diff:");
    Log.Information(lambdaSyntaxNode.Node.ToString());
    Log.Information(semanticTree.Syntax.Root.ToString());
    return;
}
return;

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