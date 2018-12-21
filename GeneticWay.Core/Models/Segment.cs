using System.Linq;

namespace GeneticWay.Core.Models
{
    public struct Segment
    {
        public Coordinate Start { get; }
        public Coordinate End { get; }

        public Segment(Coordinate start, Coordinate end)
        {
            Start = start;
            End = end;
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
            return new Segment(self.Start + right, self.End + right);
        }

        public static Segment operator -(Segment self, Coordinate right)
        {
            return new Segment(self.Start - right, self.End - right);
        }
    }
}