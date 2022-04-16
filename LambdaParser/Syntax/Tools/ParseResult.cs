using System.Diagnostics;
using LambdaParser.Syntax.Indexing;
using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Syntax.Tools;

public interface IParseResult<out T> where T : LambdaSyntaxNode
{
    T Node { get; }
    bool HasError { get; }
    ParserError Error { get; }

    [DebuggerStepThrough]
    IParseResult<TOther> As<TOther>() where TOther : LambdaSyntaxNode
    {
        if (!HasError)
            throw new LambdaParseException("Cannot cast non-error parse result.");
        return ParseResult.Fail<TOther>(Error);
    }
}

public class ParseResult
{
    public static ParseResult<T> Fail<T>(ParserError error) where T : LambdaSyntaxNode => new ParseResult<T>(error);
    public static ParseResult<T> Fail<T>(string error, NodeLocation nodeLocation) where T : LambdaSyntaxNode => new ParseResult<T>(new ParserError(error, nodeLocation));
}

public class ParseResult<T> : IParseResult<T> where T : LambdaSyntaxNode
{
    private readonly T? _node;
    private readonly ParserError? _errorMessage;

    public T Node => _node ?? throw new LambdaParseException(_errorMessage.ToString());
    public bool HasError => _errorMessage != null;
    public ParserError Error => _errorMessage ?? throw new LambdaParseException("Cannot get error from result.");

    public ParseResult(T node)
    {
        _node = node;
        _errorMessage = null;
    }

    public ParseResult(ParserError error)
    {
        _node = null;
        _errorMessage = error;
    }
}