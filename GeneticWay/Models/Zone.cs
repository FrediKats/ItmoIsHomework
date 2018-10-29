namespace GeneticWay.Models
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
    }
}