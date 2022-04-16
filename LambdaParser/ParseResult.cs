using LambdaParser.LambdaSyntaxNodes;

namespace LambdaParser;

public interface IParseResult<out T> where T : LambdaSyntaxNode
{
    T Node { get; }
    bool HasError { get; }
    ParserError Error { get; }
}

public record ParseResult<T> : IParseResult<T> where T : LambdaSyntaxNode
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

    public ParseResult(string error, NodeLocation nodeLocation) : this(new ParserError(error, nodeLocation))
    {
    }
}