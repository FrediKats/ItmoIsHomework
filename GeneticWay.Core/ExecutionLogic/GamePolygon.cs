using System;
using System.Collections.Generic;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.ExecutionLogic
{
    public class GamePolygon
    {
        public List<Zone> Zones { get; }

        public GamePolygon(List<Zone> zones)
        {
            Zones = zones;
        }

        public bool IsCoordinateInZone(Coordinate coordinate)
        {
            foreach (Zone zone in Zones)
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
            if (segment.Type == SegmentType.Circle)
                throw new ArgumentException(nameof(segment));

            foreach (Zone zone in Zones)
            {
                if (CircleAndLineIntersection(segment, zone))
                    return false;
            }

            return true;
        }

        private static bool CircleAndLineIntersection(Segment segment, Zone zone)
        {
            Segment otherCoordinateSystemSegment = segment - zone.Coordinate;
            Coordinate closestPoint = otherCoordinateSystemSegment.GetSegmentClosestPoint();

            return closestPoint.GetLength() <= zone.R;
        }
    }
}