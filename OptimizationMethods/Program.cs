using System;
using System.Linq;
using Lab1.Logger;
using Lab1.Models;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double, double> func = x => Math.Pow(x - 5, 2);
            Func<Dimensions, double> field = x =>
            {
                double c = 0;
                foreach (var a in x.Coords)
                {
                    c += a * a;
                }

                return c;
            };

            LabLogger.ExecuteFirstTask(func, -1, 6, 0.001);
            LabLogger.ExecuteFirstTaskMulti(field,
                new Dimensions(new[] { 1.0, 5.0, 2.0 }),
                0.1,
                new Dimensions(new[] { 0.1, 0.1, 0.1 }));

            Console.WriteLine(MinimumSearch.BinarySearch(new CountableFunc(func, -1, 6, 0.001)));
            Console.WriteLine(MinimumSearch.GoldenRatio(new CountableFunc(func, -1, 6, 0.001)));
            Console.WriteLine(MinimumSearch.FibonacciMethod(new CountableFunc(func, -1, 6, 0.001)));

            Console.WriteLine(MinimumSearch.DirectSearch(func, 3, 0.001));

            MultidimensionalMinimumSearch.GradientDescent(
                    new CountableMultiDimensionalFunc(field,
                    new Dimensions(new[] { 1.0, 5.0, 2.0 }), 
                    0.1,
                    new Dimensions(new[] { 0.1, 0.1, 0.1 })))
                .Coords
                .ToList()
                .ForEach(Console.WriteLine);

            MultidimensionalMinimumSearch.DirectSearch(
                    x => x[0] * x[1] + 5,
                    new Dimensions(new double[] { -2, -1 }),
                    new Dimensions(new double[] { 2, 1 }),
                    0.1)
                .Coords
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}
