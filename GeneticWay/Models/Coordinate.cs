using System;

namespace GeneticWay.Models
{
    public class Coordinate
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Coordinate(float x, float y)
        {
            X = x;
            Y = y;
        }

        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public double LengthTo(Coordinate coordinate)
        {
            return Math.Sqrt(Math.Pow(X - coordinate.X, 2) + Math.Pow(Y - coordinate.Y, 2));
        }

        public static implicit operator Coordinate((float, float) args)
        {
            return new Coordinate(args.Item1, args.Item2);
        }

        public static Coordinate operator +(Coordinate left, Coordinate right)
        {
            return (left.X + right.X, left.Y + right.Y);
        }

        public static Coordinate operator *(Coordinate left, float ratio)
        {
            return (left.X * ratio, left.Y * ratio);
        }

        public static bool operator==(Coordinate left, (float, float) right)
        {
            return Math.Abs(left.X - right.Item1) < 1e-5 && Math.Abs(left.Y - right.Item2) < 1e-5;
        }
            
        public static bool operator !=(Coordinate left, (float, float) right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"({X:F5}; {Y:F5}): {GetLength()} | {LengthTo((1, 1))}";
        }
    }
}