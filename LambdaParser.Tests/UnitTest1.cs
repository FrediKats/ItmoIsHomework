using LambdaParser.Parsers;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;

namespace LambdaParser.Tests
{
    public class ParseTests
    {
        [Test]
        public void SimplestTestParse_EnsureNoError()
        {
            var simpleTerm = "(λn.(n))";

            var lambdaSyntaxNode = LambdaSyntaxNodeParser.Instance.Parse(new StringSegment(simpleTerm));

            Assert.IsFalse(lambdaSyntaxNode.HasError);
        }
    }
}