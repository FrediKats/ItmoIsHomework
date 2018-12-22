using System.Collections.Generic;

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

        public List<Coordinate> ToCoordinatesList()
        {
            //TODO: epsilon
            const double epsilon = Configuration.Epsilon;
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