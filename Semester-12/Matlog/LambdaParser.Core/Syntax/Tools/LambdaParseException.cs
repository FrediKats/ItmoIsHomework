﻿namespace LambdaParser.Core.Syntax.Tools;

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