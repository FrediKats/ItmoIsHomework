using System;
using System.Collections.Generic;

namespace GeneticWay.Core.Models
{
    public struct Zone
    {
        public Zone(Coordinate coordinate, double r)
        {
            Coordinate = coordinate;
            R = r;
        }

        public Coordinate Coordinate { get; }
        public double R { get; }

        public bool IsInZone(Coordinate coordinate)
        {
            return coordinate.LengthTo(Coordinate) < R;
        }

        public Coordinate GetClosestCoordinate(Coordinate zeroCoordinate)
        {
            Coordinate otherCoordinateSystemPoint = Coordinate - zeroCoordinate;
            double scale = otherCoordinateSystemPoint.GetLength() / R;

            return otherCoordinateSystemPoint * (1 / scale) + zeroCoordinate;
        }

        public static bool operator ==(Zone first, Zone second)
        {
            return first.Coordinate == second.Coordinate && first.R == second.R;
        }

        public static bool operator !=(Zone left, Zone right)
        {
            return !(left == right);
        }
    }
}