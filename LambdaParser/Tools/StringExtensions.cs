namespace LambdaParser.Tools;

public class StringExtensions
{
    public static string FromChar(char c, int count)
    {
        return string.Join("", Enumerable.Repeat(c, count));

    }
}