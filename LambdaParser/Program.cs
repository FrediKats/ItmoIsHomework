using System.Text;
using LambdaParser.ConsoleIntegration;
using LambdaParser.Parsers;
using Serilog;

var simpleTerm = "(λn.(n))";
var trueTerm = "(λx.(λy.(x)))";
var termWIthApplication = "λx.((λx.(x))x)";
var termWIthApplication2 = "(λx.(λx.x))x";

var λnΛfΛxFNFX = "λn.λf.λx.f (n f x)";
var λfΛxFFXSqr = "(((λf . (λx . (f (f x)))) sqr) 3)";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File("parser-log")
    .CreateLogger();

Console.OutputEncoding = Encoding.Unicode;

var sourceCode = termWIthApplication2;
var lambdaSyntaxNode = LambdaSyntaxTreeParser.Parse(sourceCode);
if (lambdaSyntaxNode.HasError)
{
    ConsoleErrorHandler.Handle(sourceCode, lambdaSyntaxNode.Error);
}
else
{
    Log.Information($"Result - {lambdaSyntaxNode.Node}");
}
return;