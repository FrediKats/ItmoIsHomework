using System;
using System.Linq;
using Lab1.Models;
using Lab1.Tools;

namespace Lab1
{
    public static class MultidimensionalMinimumSearch
    {
        public static Dimensions GradientDescent(CountableMultiDimensionalFunc args)
        {
            if (args.StartPoint.Length != args.ParameterEpsilon.Length)
                throw new ArgumentException();

            double prevValue = args.Function(args.StartPoint);
            bool completed = false;

            while (!completed)
            {
                args.IncIteration();

                Dimensions gradient = Gradient(args);
                Dimensions prevPoint = args.StartPoint;
                
                args.StartPoint = DirectSearch(args.Function, args.StartPoint, gradient, args.FunctionEpsilon);
                double value = args.Function(args.StartPoint);

                completed = Math.Abs(value - prevValue) < args.FunctionEpsilon
                            || args.StartPoint.CheckEpsilon(prevPoint, args.ParameterEpsilon);

                Console.WriteLine($"point = {args.StartPoint}, prevPoint = {prevPoint}");

                prevValue = value;
            }

            return args.StartPoint;
        }

        private static Dimensions Gradient(CountableMultiDimensionalFunc args)
        {
            double[] gradient = args.StartPoint
                .Coords
                .Select((v, i) => NumericalDifferentiation(args.Function, args.StartPoint, i, args.ParameterEpsilon[i]))
                .ToArray();

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
            if (direction.Coords.All(x => x == 0))
                throw new ArgumentException();

            Func<double, double> rotatedField = p => field(ConvertCoordinate(p, point, direction));
            var linearAnswer = MinimumSearch.DirectSearch(new CountableFunc(rotatedField, 0, 0, epsilon));

            return ConvertCoordinate(linearAnswer, point, direction);
        }

        public static Dimensions ConvertCoordinate(double linearCoord, Dimensions shift, Dimensions direction)
        {
            Dimensions unitDirection = UnitVector(direction);
            return new Dimensions(unitDirection.Coords.Zip(shift.Coords, (x, y) => x * linearCoord + y));
        }
    }
}
