using System;
using System.Linq;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<double, double> func = x => Math.Pow(x - 5, 2);
            Func<double[], double> field = x =>
                {
                    double c = 0;
                    foreach (var a in x)
                    {
                        c += a * a;
                    }
                    return c;
                };

            Console.WriteLine(MinimumSearch.BinarySearch(func, -1, 6, 0.001));
            Console.WriteLine(MinimumSearch.GoldenRatio(func, -1, 6, 0.001));
            Console.WriteLine(MinimumSearch.FibonacciMethod(func, -1, 6, 0.001));

            MultidimentionalMinimumSearch.GradientDescent(field, new double[] { 1, 5, 2 }, 0.1, new double[] { 0.1, 0.1, 0.1 })
                                         .ToList()
                                         .ForEach(Console.WriteLine);
        }
    }
}
