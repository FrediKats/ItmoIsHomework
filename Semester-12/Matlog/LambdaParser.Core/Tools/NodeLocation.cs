using Microsoft.Extensions.Primitives;

namespace LambdaParser.Core.Tools;

public readonly record struct NodeLocation
{
    public SourceCodeIndex Start { get; }
    public SourceCodeIndex End { get; }

    public NodeLocation(SourceCodeIndex position) : this(position, position) {}

    public NodeLocation(SourceCodeIndex start, SourceCodeIndex end)
    {
        if (start > end)
            throw new ArgumentException($"Location end is lower that start. [{start}..{end}]");

        Start = start;
        End = end;
    }

    public static NodeLocation FromSegment(StringSegment segment)
    {
        return new NodeLocation(new SourceCodeIndex(segment), new SourceCodeIndex(segment.Offset + segment.Length - 1));
    }

    public static NodeLocation FromSegment(StringSegment segment, int index)
    {
        return new NodeLocation(new SourceCodeIndex(segment), new SourceCodeIndex(segment.Offset + index));
    }

    public static NodeLocation ForSegmentStart(StringSegment segment)
    {
        return new NodeLocation(new SourceCodeIndex(segment));
    }

    public override string ToString()
    {
        return $"({Start.Value}, {End.Value})";
    }
}