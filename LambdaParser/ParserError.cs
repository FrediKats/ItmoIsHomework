using LambdaParser.Indexing;

namespace LambdaParser;

public record ParserError(string Message, NodeLocation Location)
{
    public override string ToString()
    {
        return $"{Message} at {Location}";
    }
}