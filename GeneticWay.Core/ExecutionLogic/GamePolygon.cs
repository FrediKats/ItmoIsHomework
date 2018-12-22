using System.Collections.Generic;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.ExecutionLogic
{
    public class GamePolygon
    {
        public List<Circle> Zones { get; }

        public GamePolygon(List<Circle> zones)
        {
            Zones = zones;
        }

        public bool IsCoordinateInZone(Coordinate coordinate)
        {
            foreach (Circle zone in Zones)
            {
                if (zone.IsInZone(coordinate))
                    return true;
            }

            return false;
        }

        public bool IsOutOfPolygon(Coordinate coordinate)
        {
            return coordinate.X < 0 || coordinate.X > 1 || coordinate.Y < 0 || coordinate.Y > 1;
        }

        public bool IsCanCreateLine(Segment segment)
        {
            foreach (Circle zone in Zones)
            {
                if (CircleAndLineIntersection(segment, zone))
                    return false;
            }

            return true;
        }

        private static bool CircleAndLineIntersection(Segment segment, Circle circle)
        {
            Segment otherCoordinateSystemSegment = segment - circle.Coordinate;
            Coordinate closestPoint = otherCoordinateSystemSegment.GetSegmentClosestPoint();

            return closestPoint.GetLength() <= circle.Radius;
        }
    }
}