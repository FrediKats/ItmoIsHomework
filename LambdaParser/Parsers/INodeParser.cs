using LambdaParser.SyntaxNodes;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Parsers;

public interface INodeParser<T> where T : LambdaSyntaxNode
{
    IParseResult<T> Parse(StringSegment segment);
}