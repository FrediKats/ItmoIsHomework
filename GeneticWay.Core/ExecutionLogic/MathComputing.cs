using System;
using System.Linq;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.ExecutionLogic
{
    public static class MathComputing
    {
        public static Coordinate PointOnCircle(double radius, double angle)
        {
            (double, double) radiusShift = (radius * Math.Sin(angle), radius * Math.Cos(angle));
            return radiusShift;
        }

        public static Coordinate PointOnCircle(double radius, double angle, Coordinate center)
        {
            return PointOnCircle(radius, angle) + center;
        }

        //TODO: epsilon
        public static Coordinate GetSegmentClosestPoint(this Segment segment, double epsilon = 0.01)
        {
            Coordinate firstPoint = segment.Start;
            Coordinate secondPoint = segment.End;

            Coordinate center = firstPoint.MidPointWith(secondPoint);
            while ((firstPoint - secondPoint).GetLength() > epsilon)
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

            return new[] { firstPoint, secondPoint, center }.OrderBy(p => p.GetLength()).First();
        }

        public static Coordinate GetClosestToPointCoordinate(this Zone zone, Coordinate zeroCoordinate)
        {
            Coordinate otherCoordinateSystemPoint = zone.Coordinate - zeroCoordinate;
            double scale = otherCoordinateSystemPoint.GetLength() / zone.R;

            return otherCoordinateSystemPoint * (1 / scale) + zeroCoordinate;
        }

        public static Segment BuildCirclesConnectingSegment(Zone first, Zone second)
        {
            if ((first.Coordinate - second.Coordinate).GetLength() < (first.R + second.R))
                throw new ArgumentException("Zones are intersect");

            Coordinate closestPointFrom = first.GetClosestToPointCoordinate(second.Coordinate);
            Coordinate closestPointTo = second.GetClosestToPointCoordinate(first.Coordinate);

            return new Segment(closestPointFrom, closestPointTo);
        }

        public static Segment BuildFromPointToCircleSegment(Coordinate coordinate, Zone circle)
        {
            Coordinate closestPoint = circle.GetClosestToPointCoordinate(coordinate);
            return new Segment(coordinate, closestPoint);
        }

        public static Segment BuildFromCircleToPointSegment(Zone circle, Coordinate coordinate)
        {
            Coordinate closestPoint = circle.GetClosestToPointCoordinate((1, 1));
            return new Segment(closestPoint, circle.Coordinate);
        }
    }
}