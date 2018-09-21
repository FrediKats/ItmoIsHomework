using System;
using System.Linq;
using Lab1.Models;

namespace Lab1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Func<Dimensions, double> func = x => Math.Pow(1 - x[0], 2) + 100 * Math.Pow(x[1] - Math.Pow(x[0], 2), 2);

            Console.WriteLine("Test 1: f(x) = 100(x2 - x1^2)^2 + (1 - x1)^2");
            Console.WriteLine("Expected: x1 = 1, x2 = 1");
            Console.WriteLine("Result:");

            Dimensions res = MultidimensionalMinimumSearch.GradientDescent(
                   new CountableMultiDimensionalFunc(func,
                       new Dimensions(5.0, 5.0 ),
                       1e-5,
                       new Dimensions(1e-5, 1e-5)));

            res.Coords
               .ToList()
               .ForEach(Console.WriteLine);

            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();
            Console.WriteLine(func(res));

            //Console.WriteLine(MultidimensionalMinimumSearch.ConvertCoordinate(4.024, new Dimensions(-2, 3), new Dimensions(5, 1)));
            //Console.WriteLine(MultidimensionalMinimumSearch.DirectSearch(func,
            //                                                            new Dimensions(-3, 2),
            //                                                            new Dimensions(5, 1),
            //                                                            1e-6));
        }
    }
}