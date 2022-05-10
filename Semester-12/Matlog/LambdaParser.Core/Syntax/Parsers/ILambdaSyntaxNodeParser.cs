using LambdaParser.Core.Syntax.Nodes;
using LambdaParser.Core.Syntax.Tools;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Core.Syntax.Parsers;

public interface ILambdaSyntaxNodeParser<T> where T : LambdaSyntaxNode
{
    IParseResult<T> Parse(StringSegment expression);
}