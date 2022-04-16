namespace LambdaParser;

public class LambdaParseException : Exception
{
    public LambdaParseException()
    {
    }

    public LambdaParseException(string? message) : base(message)
    {
    }

    public LambdaParseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}