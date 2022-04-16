using Microsoft.Extensions.Primitives;

namespace LambdaParser.Indexing;

public record struct SourceCodeIndex(int Value)
{
    public static int operator-(SourceCodeIndex a, SourceCodeIndex b) => a.Value - b.Value;
    public static int operator-(SourceCodeIndex a, int b) => a.Value - b;
    public static int operator+(SourceCodeIndex a, int b) => a.Value + b;
    public static bool operator>(SourceCodeIndex a, SourceCodeIndex b) => a.Value > b.Value;
    public static bool operator <(SourceCodeIndex a, SourceCodeIndex b) => a.Value < b.Value;

    public SourceCodeIndex(StringSegment segment) : this(segment.Offset)
    {
    }

    public SourceCodeIndex(StringSegment segment, int index) : this(segment.Offset + index)
    {
    }

    public int ToLocalIndex(StringSegment segment)
    {
        return Value - segment.Offset;
    }
}