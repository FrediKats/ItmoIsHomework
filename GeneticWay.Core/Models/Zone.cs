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