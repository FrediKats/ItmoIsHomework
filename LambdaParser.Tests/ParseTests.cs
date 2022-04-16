using LambdaParser.Syntax.Nodes;
using LambdaParser.Syntax.Parsers;
using LambdaParser.Syntax.Tools;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;

namespace LambdaParser.Tests;

public class ParseTests
{
    [Test]
    public void SimplestTestParse_EnsureNoError()
    {
        var simpleTerm = "(λn.(n))";

        IParseResult<ExpressionLambdaSyntaxNode> lambdaSyntaxNode = LambdaSyntaxNodeParser.Instance.Parse(new StringSegment(simpleTerm));

        Assert.IsFalse(lambdaSyntaxNode.HasError);
    }

    [Test]
    public void TrueTermParse_EnsureNoError()
    {
        var trueTerm = "(λx.(λy.(x)))";

        IParseResult<ExpressionLambdaSyntaxNode> lambdaSyntaxNode = LambdaSyntaxNodeParser.Instance.Parse(new StringSegment(trueTerm));

        Assert.IsFalse(lambdaSyntaxNode.HasError);
    }

    [Test]
    public void ApplicationTermParse_EnsureNoError()
    {
        var termWIthApplication2 = "(λx.(λx.(x))x)";

        IParseResult<ExpressionLambdaSyntaxNode> lambdaSyntaxNode = LambdaSyntaxNodeParser.Instance.Parse(new StringSegment(termWIthApplication2));

        Assert.IsFalse(lambdaSyntaxNode.HasError);
    }

    [Test]
    public void NumberTermParse_EnsureNoError()
    {
        var numberDefinition = "λf.λx.f (f (f x))";

        IParseResult<ExpressionLambdaSyntaxNode> lambdaSyntaxNode = LambdaSyntaxNodeParser.Instance.Parse(new StringSegment(numberDefinition));

        Assert.IsFalse(lambdaSyntaxNode.HasError);
    }
}