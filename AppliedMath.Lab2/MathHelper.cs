using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace AppliedMath.Lab2
{
    public class MathHelper
    {
        public static double LengthToVector(Vector<double> first, Vector<double> other)
        {
            Vector<double> delta = first.Subtract(other);
            return delta.Select(v => Math.Pow(v, 2)).Sum();
        }
    }
}