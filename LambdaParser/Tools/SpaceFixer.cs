namespace LambdaParser.Tools;

public class SpaceFixer
{
    public static string FixSpaces(string text)
    {
        return text.Replace(" (", "(");
    }
}