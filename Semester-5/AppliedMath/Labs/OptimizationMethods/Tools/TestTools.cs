using System;
using System.Linq;
using Lab1.Models;

namespace Lab1.Tools
{
    public static class TestTools
    {
        public static void OneDimensionalTest()
        {
            Console.WriteLine("Test 1: f (x) = sin (x), x ∈ [-pi/2, pi/2]");
            Console.WriteLine("Expected: -pi/2");
            Console.WriteLine("Result:");
            PrintResult(x => Math.Sin(x), -Math.PI / 2, Math.PI / 2);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 2: f (x) = cos (x), x ∈ [0, pi]");
            Console.WriteLine("Expected: pi");
            Console.WriteLine("Result:");
            PrintResult(x => Math.Cos(x), 0, Math.PI);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 3: (x - 2)^2, x ∈ [-2, 20]");
            Console.WriteLine("Expected: 2");
            Console.WriteLine("Result:");
            PrintResult(x => Math.Pow(x - 2, 2), -2, 20);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 4: (x - 15)^2 + 5, x ∈ [2, 200]");
            Console.WriteLine("Expected: 15");
            Console.WriteLine("Result:");
            PrintResult(x => Math.Pow(x - 15, 2) + 5, 2, 200);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 5: (x + 5)^4, x ∈ [-10, 15]");
            Console.WriteLine("Expected: -5");
            Console.WriteLine("Result:");
            PrintResult(x => Math.Pow(x + 5, 4), -10, 15);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 6: exp (x), x ∈ [0, 100]");
            Console.WriteLine("Expected: 0");
            Console.WriteLine("Result:");
            PrintResult(x => Math.Exp(x), 0, 100);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 7: x^2 + 2x - 4, x ∈ [-10, 20]");
            Console.WriteLine("Expected: -1");
            Console.WriteLine("Result:");
            PrintResult(x => Math.Pow(x, 2) + 2 * x - 4, -10, 20);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 8: x^3 - x, x ∈ [0, 1]");
            Console.WriteLine("Expected: ~0,57-0,58");
            Console.WriteLine("Result:");
            PrintResult(x => Math.Pow(x, 3) - x, 0, 1);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();
        }

        private static void PrintResult(Func<double, double> fun, double min, double max)
        {
            Console.WriteLine("BinarySearch:   " + MinimumSearch.BinarySearch(new CountableFunc(fun, min, max, 0.001)));
            Console.WriteLine("GoldenRatio:   " + MinimumSearch.GoldenRatio(new CountableFunc(fun, min, max, 0.001)));
            Console.WriteLine("FibonacciMethod:   " +
                              MinimumSearch.FibonacciMethod(new CountableFunc(fun, min, max, 0.001)));
            Console.WriteLine("DirectSearch:   " + MinimumSearch.DirectSearch(new CountableFunc(fun, min, max, 0.001)));
        }

        public static void MultiDimensionalTest()
        {
            Console.WriteLine("Test 1: f(x) = 100(x2 - x1^2)^2 + (1 - x1)^2");
            Console.WriteLine("Expected: x1 = 1, x2 = 1");
            Console.WriteLine("Result:");
            PrintResult(x => 100 * Math.Pow(x[1] - Math.Pow(x[0], 2), 2) + Math.Pow(1 - x[0], 2));
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 2:  f(x) = (x2 - x1^2)^2 + (1 - x1)^2");
            Console.WriteLine("Expected: x1 = 1, x2 = 1");
            Console.WriteLine("Result:");
            PrintResult(x => Math.Pow(x[1] - Math.Pow(x[0], 2), 2) + Math.Pow(1 - x[0], 2));
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 3: f(x) = (x2 - x1)^2 + 100(1 - x1)^2");
            Console.WriteLine("Expected: x1 = 1, x2 = 1");
            Console.WriteLine("Result:");
            PrintResult(x => 100 * Math.Pow(x[1] - x[0], 2) + 100 * Math.Pow(1 - x[0], 2));
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 4: f(x) = 100(x2 - x1^3)^2 + (1 - x1)^2");
            Console.WriteLine("Expected: x1 = 1, x2 = 1");
            Console.WriteLine("Result:");
            PrintResult(x => 100 * Math.Pow(x[1] - Math.Pow(x[0], 3), 2) + Math.Pow(1 - x[0], 2));
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 5: f(x) =  (1.5 - x(1 - y))^2 + (2.25 - x(1 - y^2))^2 + (2.625 - x(1 - y^3))^2");
            Console.WriteLine("Expected: x = 3, y = .5");
            Console.WriteLine("Result:");
            PrintResult(x =>
                Math.Pow(1.5 - x[0] * (1 - x[1]), 2) + Math.Pow(2.25 - x[0] * (1 - Math.Pow(x[1], 2)), 2) +
                Math.Pow(2.625 - x[0] * (1 - Math.Pow(x[1], 3)), 2));
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();
        }

        private static void PrintResult(Func<Dimensions, double> fun)
        {
            MultidimensionalMinimumSearch.GradientDescent(
                    new CountableMultiDimensionalFunc(fun,
                        new Dimensions(1.0, 5.0),
                        0.1,
                        new Dimensions(1.0, 1.0)))
                .Coords
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}