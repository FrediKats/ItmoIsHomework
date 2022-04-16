using System.Text;
using LambdaParser;
using LambdaParser.Parsers;
using LambdaParser.Visualization;
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
    var visualize = LambdaSyntaxTreeVisualization.Visualize(new LambdaSyntaxTree(lambdaSyntaxNode.Node));
    Log.Information($"Tree:\n{visualize}");

    Log.Information("Diff:");
    Log.Information(sourceCode);
    Log.Information(lambdaSyntaxNode.Node.ToString());
}
return;