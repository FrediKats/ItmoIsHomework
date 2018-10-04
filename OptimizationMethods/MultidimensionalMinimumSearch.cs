using System;
using System.Linq;
using Lab1.Models;
using Lab1.Tools;

namespace Lab1
{
    public static class MultidimensionalMinimumSearch
    {
        private static Dimensions Grad(Dimensions point)
        {
            //TODO:
#if DEBUG
            Console.WriteLine(point);
#endif
            var a = new Dimensions(
                2 * (200 * Math.Pow(point[0], 3) - 200 * point[0] * point[1] + point[0] - 1),
                200 * (point[1] - Math.Pow(point[0], 2))
            );

            return a;
        }

        public static Dimensions GradientDescent(CountableMultiDimensionalFunc args)
        {
            if (args.StartPoint.Length != args.ParameterEpsilon.Length)
                throw new ArgumentException();

            bool completed = false;
            var newPoint = args.StartPoint.Copy();

            while (!completed)
            {
                args.IncIteration();

                Dimensions prevPoint = newPoint.Copy();
                Dimensions gradient = Gradient(args, prevPoint);
                double prevValue = args.Function(prevPoint);


                newPoint = DirectSearch(args, prevPoint, gradient);
                double value = args.Function(newPoint);

                bool r1 = Math.Abs(value - prevValue) < args.FunctionEpsilon;
                bool r2 = newPoint.CheckEpsilon(prevPoint, args.ParameterEpsilon);
                completed = r1 && r2;

                // TODO: debug
#if DEBUG
                Console.WriteLine($"Point move: {prevPoint} => {newPoint}");
                Console.WriteLine($"value f(p) = {prevValue:F7} => {value:F7}");
                Console.WriteLine("\n");
#endif


                prevValue = value;

            }
            return newPoint;
        }

        private static Dimensions Gradient(CountableMultiDimensionalFunc args, Dimensions startPoint)
        {
            var coords = new double[startPoint.Length];

            for (int i = 0; i < startPoint.Length; i++)
            {
                var leftPoint = startPoint.Copy();
                leftPoint[i] -= args.FunctionEpsilon;
                var rightPoint = startPoint.Copy();
                rightPoint[i] += args.FunctionEpsilon;
                coords[i] = (args.Function(rightPoint) - args.Function(leftPoint)) / (2 * args.FunctionEpsilon);
            }

            //TODO: debug
#if DEBUG
            Console.WriteLine($"Gradient: {string.Join(' ', coords.Select(v => v.ToString("F8")))}");
#endif
            return new Dimensions(coords);
        }

        private static double NumericalDifferentiation(CountableMultiDimensionalFunc args, Dimensions point, int variable)
        {
            //if (variable >= point.Coords.Length) throw new ArgumentException();

            const int number = 5;
            double[][] coeffs = new double[number][];
            coeffs[0] = new double[number];

            Dimensions tmpPoint = point.Copy();
            tmpPoint[variable] -= number / 2 * args.FunctionEpsilon;
            double left = tmpPoint[variable];

            for (int i = 0; i < coeffs[0].Length; i++)
            {
                tmpPoint[variable] = left + args.FunctionEpsilon * i;
                coeffs[0][i] = args.Function(tmpPoint);
            }

            for (int i = 1; i < coeffs.Length; i++)
            {
                coeffs[i] = new double[number - i];

                for (int j = 0; j < coeffs[i].Length; j++)
                {
                    coeffs[i][j] = coeffs[i - 1][j + 1] - coeffs[i - 1][j];
                }
            }

            double t = (tmpPoint[variable] - left) / args.FunctionEpsilon;

            return (coeffs[1][0]
                    + coeffs[2][0] * (2 * t - 1) / 2
                    + coeffs[3][0] * (3 * Math.Pow(t, 2) - 6 * t + 2) / 6
                    + coeffs[4][0] * (2 * Math.Pow(t, 3) - 9 * Math.Pow(t, 2) + 11 * t - 3) / 12)
                   / args.FunctionEpsilon;
        }

        private static Dimensions UnitVector(Dimensions coords)
        {
            double norm = Math.Sqrt(coords.Coords.Sum(x => x * x));
            return new Dimensions(coords.Coords.Select(x => x / norm));
        }

        private static Dimensions DirectSearch(CountableMultiDimensionalFunc args, Dimensions point, Dimensions direction)
        {
            if (direction.Coords.All(x => x == 0))
                throw new ArgumentException();

            Func<double, double> rotatedField = p => args.Function(ConvertCoordinate(p, point, direction));
            var linearAnswer = MinimumSearch.DirectSearch(new CountableFunc(rotatedField, 0, 0, args.FunctionEpsilon));

            return ConvertCoordinate(linearAnswer, point, direction);
        }

        private static Dimensions ConvertCoordinate(double linearCoord, Dimensions shift, Dimensions direction)
        {
            Dimensions unitDirection = UnitVector(direction);
            return new Dimensions(unitDirection.Coords.Zip(shift.Coords, (x, y) => x * linearCoord + y));
        }
    }
}
