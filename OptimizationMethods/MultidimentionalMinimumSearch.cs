using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lab1
{
    public static class MultidimentionalMinimumSearch
    {
        public static IEnumerable<double> GradientDescent(Func<double[], double> field, double[] point, double functionEpsilon, double[] parameterEpsilons)
        {
            if (point.Length != parameterEpsilons.Length) throw new ArgumentException();

            double[] gradient = Gradient(field, point, parameterEpsilons);
            double[] direction = UnitVector(gradient);

            return null;
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

            double value = point[variable];
            double left = point[variable] -= (number / 2) * epsilon;

            for (int i = 0; i < coeffs[0].Length; i++)
            {
                point[variable] = left + epsilon * i;
                coeffs[0][i] = field(point);
            }

            for (int i = 1; i < coeffs.Length; i++)
            {
                coeffs[i] = new double[number - i];

                for (int j = 0; j < coeffs[i].Length; j++)
                {
                    coeffs[i][j] = coeffs[i - 1][j + 1] - coeffs[i - 1][j];
                }
            }

            double t = (value - left) / epsilon;

            return (coeffs[1][0] + coeffs[2][0] * (2 * t - 1) / 2 + coeffs[3][0] * (3 * Math.Pow(t, 2) - 6 * t + 2) / 6 + coeffs[4][0] * (2 * Math.Pow(t, 3) - 9 * Math.Pow(t, 2) + 11 * t - 3) / 12) / epsilon;
        }

        private static double[] UnitVector(double[] coords)
        {
            double norm = Math.Sqrt(coords.Sum(x => x * x));
            return coords.Select(x => x / norm).ToArray();
        }
    }
}
