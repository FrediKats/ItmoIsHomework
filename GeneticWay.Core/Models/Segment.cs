using System.Collections.Generic;
using GeneticWay.Core.Tools;

namespace GeneticWay.Core.Models
{
    public struct Segment
    {
        public Coordinate Start { get; }
        public Coordinate End { get; }

        private Segment(Coordinate start, Coordinate end)
        {
            Start = start;
            End = end;
        }

        public static Segment Of(Coordinate start, Coordinate end)
        {
            return new Segment(start, end);
        }

        public List<Coordinate> ToCoordinatesList()
        {
            //TODO: epsilon
            return RecursiveDividing(Start, End);
        }

        private static List<Coordinate> RecursiveDividing(Coordinate start, Coordinate end)
        {
            var coordinates = new List<Coordinate>();
            if (start.LengthTo(end) > Configuration.Epsilon)
            {
                Coordinate midPoint = start.MidPointWith(end);
                coordinates.AddRange(RecursiveDividing(start, midPoint));
                coordinates.AddRange(RecursiveDividing(midPoint, end));
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
            return Segment.Of(self.Start + right, self.End + right);
        }

        public static Segment operator -(Segment self, Coordinate right)
        {
            return new Segment(self.Start - right, self.End - right);
        }
    }
}