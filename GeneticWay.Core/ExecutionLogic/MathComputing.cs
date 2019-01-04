using System;
using System.Linq;
using GeneticWay.Core.Models;
using GeneticWay.Core.Tools;

namespace GeneticWay.Core.ExecutionLogic
{
    public static class MathComputing
    {
        public static Coordinate GetPointOnCircleCoordinate(double radius, double angle)
        {
            //TODO: one more dirty hack
            double newRadius = radius + Configuration.Epsilon;
            return (newRadius * Math.Cos(angle), newRadius * Math.Sin(angle));
        }

        public static Coordinate GetPointOnCircleCoordinate(double radius, double angle, Coordinate center)
        {
            return GetPointOnCircleCoordinate(radius, angle) + center;
        }

        //TODO: Can be replace with each-point checker
        public static Coordinate GetSegmentClosestToCenterPoint(this Segment segment)
        {
            Coordinate firstPoint = segment.Start;
            Coordinate secondPoint = segment.End;

            Coordinate center = firstPoint.MidPointWith(secondPoint);
            while ((firstPoint - secondPoint).GetLength() > Configuration.Epsilon)
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

        public static Coordinate GetClosestPointFromCircleToPoint(this Circle circle, Coordinate zeroCoordinate)
        {
            Coordinate otherCoordinateSystemPoint = circle.Coordinate - zeroCoordinate;
            //TODO: one more dirty hack
            double scale = circle.Radius / otherCoordinateSystemPoint.GetLength() * 1.05;

            return otherCoordinateSystemPoint * (1 - scale) + zeroCoordinate;
        }

        public static Segment BuildCirclesConnectingSegment(Circle first, Circle second)
        {
            if (first.Coordinate.LengthTo(second.Coordinate) < first.Radius + second.Radius)
            {
                throw new ArgumentException("Zones are intersect");
            }

            Coordinate closestPointFrom = first.GetClosestPointFromCircleToPoint(second.Coordinate);
            Coordinate closestPointTo = second.GetClosestPointFromCircleToPoint(first.Coordinate);

            return Segment.Of(closestPointFrom, closestPointTo);
        }

        public static Segment BuildSegmentFromPointToCircle(Coordinate coordinate, Circle circle)
        {
            Coordinate closestPoint = circle.GetClosestPointFromCircleToPoint(coordinate);
            return Segment.Of(coordinate, closestPoint);
        }

        public static Segment BuildSegmentFromCircleToPoint(Circle circle, Coordinate coordinate)
        {
            Coordinate closestPoint = circle.GetClosestPointFromCircleToPoint(coordinate);
            return Segment.Of(closestPoint, coordinate);
        }
    }
}