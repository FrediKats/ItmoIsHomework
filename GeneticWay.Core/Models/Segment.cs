using System.Collections.Generic;
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

            Coordinate center = firstPoint.MidPointWith(secondPoint);
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
                center = firstPoint.MidPointWith(secondPoint);
            }

            return new[] {firstPoint, secondPoint, center}.OrderBy(p => p.GetLength()).First();
        }

        public List<Coordinate> ToCoordinatesList(double epsilon = 1e-4)
        {
            return RecursiveDividing(Start, End, epsilon);
        }

        private static List<Coordinate> RecursiveDividing(Coordinate start, Coordinate end, double epsilon)
        {
            var coordinates = new List<Coordinate>();
            if (start.LengthTo(end) > epsilon)
            {
                Coordinate midPoint = start.MidPointWith(end);
                coordinates.AddRange(RecursiveDividing(start, midPoint, epsilon));
                coordinates.AddRange(RecursiveDividing(midPoint, end, epsilon));
            }
            else
            {
                coordinates.Add(start);
            }
            coordinates.Add(end);
            return coordinates;
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