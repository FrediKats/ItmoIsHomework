using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Lab1.Models;
using Lab1.Tools;

namespace Lab1
{
    public static class MultidimensionalMinimumSearch
    {
        private static Dimensions Grad(Dimensions point)
        {
            Console.WriteLine(point);
            var a = new Dimensions(
                2 * (200 * Math.Pow(point[0], 3) - 200 * point[0] * point[1] + point[0] - 1),
                200 * (point[1] - Math.Pow(point[0], 2))
                );

            Console.WriteLine(a);

            return a;
        }

        public static Dimensions GradientDescent(CountableMultiDimensionalFunc args)
        {
            if (args.StartPoint.Length != args.ParameterEpsilon.Length)
                throw new ArgumentException();

            double prevValue = args.Function(args.StartPoint);
            bool completed = false;

            var currentPoint = args.StartPoint.Copy();

            while (!completed)
            {
                args.IncIteration();

                Dimensions gradient = Gradient(args.Function, currentPoint, args.FunctionEpsilon);
                //gradient = Grad(currentPoint);
                Dimensions prevPoint = currentPoint;

                currentPoint = DirectSearch(args.Function, currentPoint, gradient, args.FunctionEpsilon);
                double value = args.Function(currentPoint);
                
                completed = Math.Abs(value - prevValue) < args.FunctionEpsilon
                            && currentPoint.CheckEpsilon(prevPoint, args.ParameterEpsilon);

                //// TODO: debug
                //Console.WriteLine($"GD: {prevPoint} => {currentPoint}");
                //Console.WriteLine($"value f(p) = {prevValue:F15} => {value:F15}");
                //Console.WriteLine("\n");

                prevValue = value;

            }
            return currentPoint;
        }

        private static Dimensions Gradient(Func<Dimensions, double> function, Dimensions startPoint, double functionEpsilon)
        {
            var coords = new double[startPoint.Length];

            for (int i = 0; i < startPoint.Length; i++)
            {
                var leftPoint = startPoint.Copy();
                leftPoint[i] -= functionEpsilon;
                var rightPoint = startPoint.Copy();
                rightPoint[i] += functionEpsilon;
                coords[i] = (function(rightPoint) - function(leftPoint)) / (2 * functionEpsilon);
            }

            //TODO:
            //Console.WriteLine($"Gradient: {string.Join(' ', coords.Select(v => v.ToString("F8")))}");

            return new Dimensions(coords);
        }

        //private static Dimensions Gradient(Func<Dimensions, double> function, Dimensions startPoint, double functionEpsilon)
        //{
        //    double[] gradient = startPoint
        //        .Coords
        //        .Select((v, i) => NumericalDifferentiation(function, startPoint, i, functionEpsilon))
        //        .ToArray();
        //    //TODO: debug
        //    Console.WriteLine($"Gradient: {string.Join(' ', gradient.Select(v => v.ToString("F4")))}");
        //    return new Dimensions(gradient);
        //}

        private static double NumericalDifferentiation(Func<Dimensions, double> field, Dimensions point, int variable, double epsilon)
        {
            //if (variable >= point.Coords.Length) throw new ArgumentException();

            const int number = 5;
            double[][] coeffs = new double[number][];
            coeffs[0] = new double[number];

            Dimensions tmpPoint = point.Copy();
            tmpPoint[variable] -= number / 2 * epsilon;
            double left = tmpPoint[variable];

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

            double t = (tmpPoint[variable] - left) / epsilon;

            return (coeffs[1][0]
                    + coeffs[2][0] * (2 * t - 1) / 2
                    + coeffs[3][0] * (3 * Math.Pow(t, 2) - 6 * t + 2) / 6
                    + coeffs[4][0] * (2 * Math.Pow(t, 3) - 9 * Math.Pow(t, 2) + 11 * t - 3) / 12)
                   / epsilon;
        }

        private static Dimensions UnitVector(Dimensions coords)
        {
            double norm = Math.Sqrt(coords.Coords.Sum(x => x * x));
            return new Dimensions(coords.Coords.Select(x => x / norm));
        }

        private static Dimensions DirectSearch(Func<Dimensions, double> field, Dimensions point, Dimensions direction, double epsilon)
        {
            if (direction.Coords.All(x => x == 0))
                throw new ArgumentException();

            Func<double, double> rotatedField = p => field(ConvertCoordinate(p, point, direction));
            var linearAnswer = MinimumSearch.DirectSearch(new CountableFunc(rotatedField, 0, 0, epsilon));

            return ConvertCoordinate(linearAnswer, point, direction);
        }

        private static Dimensions ConvertCoordinate(double linearCoord, Dimensions shift, Dimensions direction)
        {
            Dimensions unitDirection = UnitVector(direction);
            return new Dimensions(unitDirection.Coords.Zip(shift.Coords, (x, y) => x * linearCoord + y));
        }
    }
}
