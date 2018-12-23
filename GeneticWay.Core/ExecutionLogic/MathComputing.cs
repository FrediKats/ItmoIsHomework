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

        public static Coordinate GetSegmentClosestPoint(this Segment segment)
        {
            //TODO: epsilon
            const double epsilon = Configuration.Epsilon;

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

        public static Coordinate GetClosestToPointCoordinate(this Circle circle, Coordinate zeroCoordinate)
        {
            Coordinate otherCoordinateSystemPoint = circle.Coordinate - zeroCoordinate;
            double scale = circle.Radius / otherCoordinateSystemPoint.GetLength();

            return otherCoordinateSystemPoint * (1 - scale) + zeroCoordinate;
        }

        public static Segment BuildCirclesConnectingSegment(Circle first, Circle second)
        {
            if ((first.Coordinate - second.Coordinate).GetLength() < first.Radius + second.Radius)
            {
                throw new ArgumentException("Zones are intersect");
            }

            Coordinate closestPointFrom = first.GetClosestToPointCoordinate(second.Coordinate);
            Coordinate closestPointTo = second.GetClosestToPointCoordinate(first.Coordinate);

            return new Segment(closestPointFrom, closestPointTo);
        }

        public static Segment BuildFromPointToCircleSegment(Coordinate coordinate, Circle circle)
        {
            Coordinate closestPoint = circle.GetClosestToPointCoordinate(coordinate);
            return new Segment(coordinate, closestPoint);
        }

        public static Segment BuildFromCircleToPointSegment(Circle circle, Coordinate coordinate)
        {
            Coordinate closestPoint = circle.GetClosestToPointCoordinate(coordinate);
            return new Segment(closestPoint, coordinate);
        }

        public static double ChooseOptimalAcceleration(double length, double velocity, double time)
        {
            if (Math.Abs(length) < Configuration.Epsilon / 10)
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