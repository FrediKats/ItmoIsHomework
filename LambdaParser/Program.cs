using System.Text;
using LambdaParser.Parsers;
using LambdaParser.Tools;
using Serilog;

var simpleTerm = "(λn.(n))";
var trueTerm = "(λx.(λy.(x)))";
var termWIthApplication = "λx.((λx.(x))x)";
var termWIthApplication2 = "(λx.(λx.(x))x)";

var numberDefinitionSimple = "λf.λx.(f (f (f x)))";
var numberDefinition = "λf.λx.f (f (f x))";

var λnΛfΛxFNFX = "(λn.λf.λx.f (n f x)";
var λfΛxFFXSqr = "(((λf . (λx . (f (f x)))) sqr) 3)";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .WriteTo.File("parser-log")
    .CreateLogger();

Console.OutputEncoding = Encoding.Unicode;

var sourceCode = SpaceFixer.FixSpaces(numberDefinitionSimple);
var lambdaSyntaxNode = LambdaSyntaxTreeParser.Parse(sourceCode);
if (lambdaSyntaxNode.HasError)
{
    SyntaxWalkerLogger.LogIt((string) sourceCode, (ParserError) lambdaSyntaxNode.Error);
}
else
{
    Log.Information($"Result - {lambdaSyntaxNode.Node}");
}
return;