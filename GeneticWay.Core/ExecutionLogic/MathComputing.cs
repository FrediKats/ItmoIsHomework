using System;
using System.Linq;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.ExecutionLogic
{
    public static class MathComputing
    {
        public static Coordinate PointOnCircle(double radius, double angle)
        {
            (double, double) radiusShift = (radius * Math.Cos(angle), radius * Math.Sin(angle));
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

            return new[] {firstPoint, secondPoint, center}.OrderBy(p => p.GetLength()).First();
        }

        public static Coordinate GetClosestToPointCoordinate(this Zone zone, Coordinate zeroCoordinate)
        {
            Coordinate otherCoordinateSystemPoint = zone.Coordinate - zeroCoordinate;
            double scale = zone.R / otherCoordinateSystemPoint.GetLength();

            return otherCoordinateSystemPoint * (1 - scale) + zeroCoordinate;
        }

        public static Segment BuildCirclesConnectingSegment(Zone first, Zone second)
        {
            if ((first.Coordinate - second.Coordinate).GetLength() < first.R + second.R)
            {
                throw new ArgumentException("Zones are intersect");
            }

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
            Coordinate closestPoint = circle.GetClosestToPointCoordinate(coordinate);
            return new Segment(closestPoint, coordinate);
        }

        public static double ChooseOptimalAcceleration(double length, double velocity, double time)
        {
            if (length == 0)
                return 0;
            return (length - velocity * time) / (time * time);
        }

        public static Coordinate ChooseOptimalAcceleration(Coordinate length, Coordinate velocity, double time)
        {
            return new Coordinate(ChooseOptimalAcceleration(length.X, velocity.X, time),
                ChooseOptimalAcceleration(length.Y, velocity.Y, time));
        }
    }
}