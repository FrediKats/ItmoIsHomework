using Microsoft.Extensions.Primitives;

namespace LambdaParser;

public class NodeLocation
{
    public int Start { get; }
    public int End { get; }
    public int Length => End - Start + 1;

    public NodeLocation()
    {
        
    }

    public NodeLocation(int start, int end)
    {
        if (start < end)
            throw new ArgumentException($"Location end is lower that start. [{start}..{end}]");

        Start = start;
        End = end;
    }

    public static NodeLocation FromSegment(StringSegment segment)
    {
        return new NodeLocation(segment.Offset, segment.Offset + segment.Length - 1);
    }
}