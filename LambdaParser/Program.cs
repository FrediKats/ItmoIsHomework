using System.Text;
using LambdaParser;
using LambdaParser.Parsers;
using LambdaParser.Tools;
using LambdaParser.Visualization;
using Serilog;

var predecessor= "λn.λf.λx.n (λg.λh.h (g f)) (λu.x) (λu.u)";
var λnΛfΛxFNFX = "(λn.λf.λx.f (n f x)";
var λfΛxFFXSqr = "(((λf . (λx . (f (f x)))) sqr) 3)";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File("parser-log")
    .CreateLogger();

Console.OutputEncoding = Encoding.Unicode;

var lambdaSyntaxNode = LambdaSyntaxTreeParser.Parse(predecessor);
if (lambdaSyntaxNode.HasError)
{
    SyntaxWalkerLogger.LogIt(predecessor, lambdaSyntaxNode.Error);
}
else
{
    Log.Information($"Result - {lambdaSyntaxNode.Node}");
    var visualize = LambdaSyntaxTreeVisualization.Visualize(new LambdaSyntaxTree(lambdaSyntaxNode.Node));
    Log.Information(visualize);
}
return;