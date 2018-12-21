namespace GeneticWay.Core.Models
{
    public struct Segment
    {
        public Coordinate Start { get; }
        public Coordinate End { get; }
        public SegmentType Type { get; }

        public Segment(Coordinate start, Coordinate end, SegmentType type = SegmentType.None)
        {
            Start = start;
            End = end;
            Type = type;
        }

        public static Segment operator +(Segment self, Coordinate right)
        {
            return new Segment(self.Start + right, self.End + right, self.Type);
        }

        public static Segment operator -(Segment self, Coordinate right)
        {
            return new Segment(self.Start - right, self.End - right, self.Type);
        }
    }
}