namespace GeneticWay.Models
{
    public class Coordinate
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Coordinate(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Coordinate((double, double) args)
        {
            return new Coordinate(args.Item1, args.Item2);
        }
}
}