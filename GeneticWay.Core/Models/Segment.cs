using System.Linq;

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

        //TODO: epsilon
        public Coordinate GetSegmentClosestPoint(double epsilon = 0.01)
        {
            Coordinate firstPoint = Start;
            Coordinate secondPoint = End;

            Coordinate center = (firstPoint + secondPoint) * 0.5;
            while ((End - Start).GetLength() > epsilon)
            {
                if (firstPoint.GetLength() > secondPoint.GetLength())
                {
                    firstPoint = center;
                }
                else
                {
                    secondPoint = center;
                }
                center = (firstPoint + secondPoint) * 0.5;
            }

            return new[] {firstPoint, secondPoint, center}.OrderBy(p => p.GetLength()).First();
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