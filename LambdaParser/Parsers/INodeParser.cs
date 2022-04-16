using LambdaParser.LambdaSyntaxNodes;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Parsers;

public interface INodeParser<T> where T : LambdaSyntaxNode
{
    IParseResult<T> Parse(StringSegment segment);
}