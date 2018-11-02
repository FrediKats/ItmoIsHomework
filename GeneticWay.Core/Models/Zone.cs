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

        //TODO: read from config file
        public static List<Zone> GetZones()
        {
            return new List<Zone>
            {
                new Zone((0.5, 0.25), 0.1),
                new Zone((0.75, 0.5), 0.07),
                new Zone((0.75, 0.65), 0.05),
                new Zone((0.25, 0.5), 0.05),
                new Zone((0.8, 0.9), 0.05),
                new Zone((0.92, 0.92), 0.05),
                new Zone((0.92, 0.92), 0.05),
                new Zone((0.6, 0.65), 0.15),
                new Zone((0.9, 0.8), 0.1)
            };
        }
    }
}