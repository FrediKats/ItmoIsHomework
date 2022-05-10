using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace AppliedMath.Lab2
{
    public static class MathHelper
    {
        public static double LengthToVector(Vector<double> first, Vector<double> other)
        {
            double sInSquare = first
                .Subtract(other)
                .Select(v => Math.Pow(v, 2))
                .Average();

            return Math.Sqrt(sInSquare);
        }
    }
}