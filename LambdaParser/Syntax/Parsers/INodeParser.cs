using LambdaParser.Syntax.Nodes;
using LambdaParser.Syntax.Tools;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Syntax.Parsers;

public interface INodeParser<T> where T : LambdaSyntaxNode
{
    IParseResult<T> Parse(StringSegment expression);
}