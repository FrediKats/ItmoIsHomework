﻿namespace LambdaParser.Core.Syntax.Tools;

public static class SpaceFixer
{
    public static string FixSpaces(string text)
    {
        return text.Replace(" (", "(");
    }
}