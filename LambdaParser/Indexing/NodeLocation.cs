using Microsoft.Extensions.Primitives;

namespace LambdaParser.Indexing;

public class NodeLocation
{
    public SourceCodeIndex Start { get; }
    public SourceCodeIndex End { get; }
    public int Length => End - Start + 1;

    public NodeLocation(int position) : this(position, position) {}
    private NodeLocation(StringSegment segment) : this(new SourceCodeIndex(segment)) { }
    
    public NodeLocation(SourceCodeIndex position) : this(position, position) {}

    public NodeLocation(int start, int end) : this(new SourceCodeIndex(start), new SourceCodeIndex(end))
    {
    }

    public NodeLocation(SourceCodeIndex start, SourceCodeIndex end)
    {
        if (start > end)
            throw new ArgumentException($"Location end is lower that start. [{start}..{end}]");

        Start = start;
        End = end;
    }

    public static NodeLocation FromSegment(StringSegment segment)
    {
        return new NodeLocation(segment.Offset, segment.Offset + segment.Length - 1);
    }

    public static NodeLocation ForSegmentStart(StringSegment segment)
    {
        return new NodeLocation(segment);
    }

    public override string ToString()
    {
        return $"({Start.Value}, {End.Value})";
    }
}