namespace LambdaParser.Core.Semantic.Tools;

public class LambdaSemanticParseException : Exception
{
    public LambdaSemanticParseException()
    {
    }

    public LambdaSemanticParseException(string? message) : base(message)
    {
    }

    public LambdaSemanticParseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}