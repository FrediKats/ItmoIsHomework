// See https://aka.ms/new-console-template for more information

using LambdaParser.LambdaSyntaxNodes;
using LambdaParser.Parsers;
using Microsoft.Extensions.Primitives;
using Serilog;

var simpleTerm = "(λn.(n))";
var λnΛfΛxFNFX = "λn.λf.λx.f (n f x)";
var λfΛxFFXSqr = "(((λf . (λx . (f (f x)))) sqr) 3)";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File("parser-log")
    .CreateLogger();

var lambdaSyntaxNode = LambdaSyntaxNodeParser.Instance.Parse(new StringSegment(simpleTerm));
if (lambdaSyntaxNode.HasError)
    Log.Error(lambdaSyntaxNode.Error.Message);
else
{
    Log.Information($"Result - {lambdaSyntaxNode.Node}");
}
return;