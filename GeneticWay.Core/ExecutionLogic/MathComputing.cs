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
    }
}