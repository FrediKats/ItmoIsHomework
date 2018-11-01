using System;

namespace GeneticWay.Core.Models
{
    public struct Coordinate
    {
        public double X { get; }
        public double Y { get; }

        public Coordinate(double x, double y)
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
            Coordinate d = this - coordinate;
            return Math.Sqrt(d.X * d.X + d.Y * d.Y);
        }

        public Coordinate WithEpsilon(int epsilon)
        {
            return (Math.Round(X, epsilon), Math.Round(Y, epsilon));
        }

        #region Operators

        public static implicit operator Coordinate((double, double) args)
        {
            return new Coordinate(args.Item1, args.Item2);
        }

        public static Coordinate operator +(Coordinate left, Coordinate right)
        {
            return (left.X + right.X, left.Y + right.Y);
        }

        public static Coordinate operator -(Coordinate left, Coordinate right)
        {
            return (left.X - right.X, left.Y - right.Y);
        }

        public static Coordinate operator *(Coordinate left, double ratio)
        {
            return (left.X * ratio, left.Y * ratio);
        }

        public static Coordinate operator *(Coordinate left, int ratio)
        {
            return (left.X * ratio, left.Y * ratio);
        }

        public static bool operator ==(Coordinate left, (double, double) right)
        {
            return Math.Abs(left.X - right.Item1) < Configuration.Epsilon
                   && Math.Abs(left.Y - right.Item2) < Configuration.Epsilon;
        }

        public static bool operator !=(Coordinate left, (double, double) right)
        {
            return !(left == right);
        }

        #endregion

        public override string ToString()
        {
            Coordinate d = WithEpsilon(Configuration.EpsilonInt);
            return $"({d.X}; {d.Y}): {GetLength()} | {LengthTo((1, 1))}";
        }
    }
}