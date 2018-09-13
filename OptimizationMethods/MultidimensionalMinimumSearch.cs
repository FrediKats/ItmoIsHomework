using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Lab1.Models;
using Lab1.Tools;

namespace Lab1
{
    public static class MultidimensionalMinimumSearch
    {
        public static Dimensions GradientDescent(CountableMultiDimensionalFunc funcData)
        {
            if (funcData.StartPoint.Coords.Length != funcData.ParameterEpsilon.Coords.Length)
                throw new ArgumentException();

            var point = funcData.StartPoint;

            double lambda = Math.Sqrt(3) * funcData.ParameterEpsilon.Coords.Min();
            double prevFunc = funcData.Function(point);
            bool completed = false;

            while (!completed)
            {
                funcData.IncIteration();

                Dimensions gradient = Gradient(funcData.Function, point, funcData.ParameterEpsilon);
                Dimensions direction = UnitVector(gradient);

                Dimensions prevPoint = point;

                point = point.NewGradientPoint(direction, lambda);

                double func = funcData.Function(point);

                completed = Math.Abs(func - prevFunc) < funcData.FunctionEpsilon ||
                            point.CheckEpsilon(prevPoint, funcData.ParameterEpsilon);

                prevFunc = func;
            }

            return point;
        }

        public static Dimensions GradientDescent(Func<Dimensions, double> field, Dimensions point, double functionEpsilon, Dimensions parameterEpsilons)
        {
            if (point.Coords.Length != parameterEpsilons.Coords.Length) throw new ArgumentException();

            double lambda = Math.Sqrt(3) * parameterEpsilons.Coords.Min();
            double prevFunc = field(point);
            bool completed = false;

            while (!completed)
            {
                Dimensions gradient = Gradient(field, point, parameterEpsilons);
                Dimensions direction = UnitVector(gradient);
                Dimensions prevPoint = point;

                point = point.NewGradientPoint(direction, lambda);
                double func = field(point);

                completed = Math.Abs(func - prevFunc) < functionEpsilon
                            || point.CheckEpsilon(prevPoint, parameterEpsilons);

                prevFunc = func;
            }

            return point;
        }

        private static Dimensions Gradient(Func<Dimensions, double> field, Dimensions point, Dimensions parameterEpsilons)
        {
            if (point.Coords.Length != parameterEpsilons.Coords.Length) throw new ArgumentException();

            double[] gradient = new double[point.Coords.Length];

            for (int i = 0; i < point.Coords.Length; i++)
            {
                gradient[i] = NumericalDifferentiation(field, point, i, parameterEpsilons[i]);
            }

            return new Dimensions(gradient);
        }

        private static double NumericalDifferentiation(Func<Dimensions, double> field, Dimensions point, int variable, double epsilon)
        {
            if (variable >= point.Coords.Length) throw new ArgumentException();

            int number = 5;
            double[][] coeffs = new double[number][];
            coeffs[0] = new double[number];

            Dimensions tmpPoint = point.Copy();
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

        private static Dimensions UnitVector(Dimensions coords)
        {
            double norm = Math.Sqrt(coords.Coords.Sum(x => x * x));
            return new Dimensions(coords.Coords.Select(x => x / norm));
        }

        public static Dimensions DirectSearch(Func<Dimensions, double> field, Dimensions point, Dimensions direction, double epsilon)
        {
            //TODO:
            //if (point.EqualsValues(direction))
            //    throw new ArgumentException();

            Dimensions unitDirection = UnitVector(direction);
            Func<double, double> rotatedField = p => field(ConvertCoordinate(p, unitDirection));

            double newPoint = point.GetDirection(direction) * point.Sqrt();
            Console.WriteLine($"old = {field(point)}, new = {rotatedField(newPoint)}");

            var linearAnswer = MinimumSearch.DirectSearch(rotatedField, newPoint, epsilon);
            Console.WriteLine(linearAnswer);
            return ConvertCoordinate(linearAnswer, unitDirection);
        }

        private static Dimensions ConvertCoordinate(double linearCoord, Dimensions direction)
        {
            direction.Coords = direction.Coords.Select(x => x * linearCoord).ToArray();
            return direction;
        }
    }
}
