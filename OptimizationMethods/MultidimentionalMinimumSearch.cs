using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public delegate T ScalarField<T>(params T[] cordinates);

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public static class MultidimentionalMinimumSearch
    {
        public static Point GradientDescent(ScalarField<double> function, Point start, double functionEpsilon, IEnumerable<double> parameterEpsilon)
        {
            return null;
        }
    }
}
