using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lab1
{
    public static class MultidimentionalMinimumSearch
    {
        public static double[] GradientDescent(Func<double[], double> field, double[] point, double functionEpsilon, double[] parameterEpsilons)
        {
            if (point.Length != parameterEpsilons.Length) throw new ArgumentException();

            double lambda = Math.Sqrt(3) * parameterEpsilons.Min();
            double prevFunc = field(point);
            bool completed = false;

            while (!completed)
            {
                double[] gradient = Gradient(field, point, parameterEpsilons);
                double[] direction = UnitVector(gradient);

                double[] prevPoint = point;

                point = point.Zip(direction, (x, y) => x - y * lambda).ToArray();

                double func = field(point);

                completed = Math.Abs(func - prevFunc) < functionEpsilon ||
                            point.Zip(prevPoint, (po, pr) => Math.Abs(po - pr))
                                 .Zip(parameterEpsilons, (diff, eps) => diff < eps)
                                 .All(i => i);

                prevFunc = func;
            }

            return point;
        }

        private static double[] Gradient(Func<double[], double> field, double[] point, double[] parameterEpsilons)
        {
            if (point.Length != parameterEpsilons.Length) throw new ArgumentException();

            double[] gradient = new double[point.Length];

            for (int i = 0; i < point.Length; i++)
            {
                gradient[i] = NumericalDifferentiation(field, point, i, parameterEpsilons[i]);
            }

            return gradient;
        }

        private static double NumericalDifferentiation(Func<double[], double> field, double[] point, int variable, double epsilon)
        {
            if (variable >= point.Length) throw new ArgumentException();

            int number = 5;
            double[][] coeffs = new double[number][];
            coeffs[0] = new double[number];

            double[] tmpPoint = point.Select(x => x).ToArray();
            double left = tmpPoint[variable] -= (number / 2) * epsilon;

            for (int i = 0; i < coeffs[0].Length; i++)
            {
                tmpPoint[variable] = left + epsilon * i;
                coeffs[0][i] = field(tmpPoint);
            }

            for (int i = 1; i < coeffs.Length; i++)
            {
                coeffs[i] = new double[number - i];

                for (int j = 0; j < coeffs[i].Length; j++)
                {
                    coeffs[i][j] = coeffs[i - 1][j + 1] - coeffs[i - 1][j];
                }
            }

            double t = (point[variable] - left) / epsilon;

            return (coeffs[1][0] + coeffs[2][0] * (2 * t - 1) / 2 + coeffs[3][0] * (3 * Math.Pow(t, 2) - 6 * t + 2) / 6 + coeffs[4][0] * (2 * Math.Pow(t, 3) - 9 * Math.Pow(t, 2) + 11 * t - 3) / 12) / epsilon;
        }

        private static double[] UnitVector(double[] coords)
        {
            double norm = Math.Sqrt(coords.Sum(x => x * x));
            return coords.Select(x => x / norm).ToArray();
        }

        public static double[] DirectSearch(Func<double[], double> field, double[] point, double[] direction, double epsilon)
        {
            var div = point.Zip(direction, (p, d) => p / d);
            if (!div.All(x => x == div.First())) throw new ArgumentException();

            double[] unitDirection = UnitVector(direction);
            Func<double, double> rotatedfield = p => field(ConvertCoordinate(p, unitDirection));
            double newPoint = Math.Sqrt(point.Select(x => x * x).Sum());
            Console.WriteLine($"old = {field(point)}, new = {rotatedfield(newPoint)}");

            var linearAnswer = MinimumSearch.DirectSearch(rotatedfield, newPoint, epsilon);
            return ConvertCoordinate(linearAnswer, unitDirection);
        }

        private static double[] ConvertCoordinate(double linearCoord, double[] direction)
        {
            return direction.Select(x => x * linearCoord).ToArray();
        }
    }
}
