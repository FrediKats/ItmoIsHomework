namespace GeneticWay.Core.Models
{
    public struct Circle
    {
        public Circle(Coordinate coordinate, double radius)
        {
            Coordinate = coordinate;
            Radius = radius;
        }

        public Coordinate Coordinate { get; }
        public double Radius { get; }

        //TODO: remove
        public bool IsInZone(Coordinate coordinate)
        {
            return coordinate.LengthTo(Coordinate) < Radius;
        }

        public static bool operator ==(Circle first, Circle second)
        {
            return first.Coordinate == second.Coordinate && first.Radius == second.Radius;
        }

        public static bool operator !=(Circle left, Circle right)
        {
            return !(left == right);
        }
    }
}