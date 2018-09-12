using System;
using System.Linq;
using Lab1.Logger;

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

            LabLogger.ExecuteFirstTask(func, -1, 6, 0.001);
            LabLogger.ExecuteFirstTaskMulti(field, new[] { 1.0, 5.0, 2.0 }, 0.1, new[] { 0.1, 0.1, 0.1 });

            Console.WriteLine(MinimumSearch.BinarySearch(func, -1, 6, 0.001));
            Console.WriteLine(MinimumSearch.GoldenRatio(func, -1, 6, 0.001));
            Console.WriteLine(MinimumSearch.FibonacciMethod(func, -1, 6, 0.001));

            MultidimentionalMinimumSearch.GradientDescent(field, new[] { 1.0, 5.0, 2.0 }, 0.1, new[] { 0.1, 0.1, 0.1 })
                                         .ToList()
                                         .ForEach(Console.WriteLine);
        }
    }
}
