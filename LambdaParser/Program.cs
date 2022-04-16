// See https://aka.ms/new-console-template for more information

using LambdaParser;
using LambdaParser.LambdaSyntaxNodes;
using LambdaParser.Parsers;
using Microsoft.Extensions.Primitives;

Console.WriteLine("Hello, World!");

var simpleTerm = "(λn.(n))";
var λnΛfΛxFNFX = "λn.λf.λx.f (n f x)";
var λfΛxFFXSqr = "(((λf . (λx . (f (f x)))) sqr) 3)";

LambdaSyntaxNode lambdaSyntaxNode = LambdaSyntaxNodeParser.Parse(new StringSegment(simpleTerm));

Console.WriteLine(lambdaSyntaxNode.Location);