using Microsoft.Extensions.Primitives;

namespace LambdaParser.Syntax.Indexing;

public readonly record struct SourceCodeIndex(int Value)
{
    public static SourceCodeIndex operator -(SourceCodeIndex a, SourceCodeIndex b) => new SourceCodeIndex(a.Value - b.Value);
    public static SourceCodeIndex operator -(SourceCodeIndex a, int b) => new SourceCodeIndex(a.Value - b);
    public static SourceCodeIndex operator +(SourceCodeIndex a, int b) => new SourceCodeIndex(a.Value + b);
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