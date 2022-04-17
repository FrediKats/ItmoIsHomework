﻿using LambdaParser.Core.Tools;

namespace LambdaParser.Syntax.Tools;

public record ParserError(string Message, NodeLocation Location)
{
    public override string ToString()
    {
        return $"{Message} at {Location}";
    }
}