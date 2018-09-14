using System;
using System.Linq;
using Lab1.Logger;
using Lab1.Models;

namespace Lab1
{
    internal class Program
    {
        private static void Main(string[] args)
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
            LabLogger.ExecuteFirstTask(func, -1, 6, 0.001, "interval_f1.xlsx");
            LabLogger.ExecuteFirstTask(Math.Sin, -Math.PI / 2, Math.PI / 2, 0.001, "interval_f2.xlsx");
            LabLogger.ExecuteFirstTask(Math.Cos, 0, Math.PI, 0.001, "interval_f3.xlsx");
            LabLogger.ExecuteFirstTask(x => Math.Pow(x - 2, 2), -2, 20, 0.001, "interval_f4.xlsx");
            LabLogger.ExecuteFirstTask(x => Math.Pow(x - 15, 2) + 5, 2, 200, 0.001, "interval_f5.xlsx");
            LabLogger.ExecuteFirstTask(Math.Exp, 0, 100, 0.001, "interval_f6.xlsx");
            LabLogger.ExecuteFirstTask(x => Math.Pow(x, 2) + 2 * x - 4, -10, -20, 0.001, "interval_f7.xlsx");
            LabLogger.ExecuteFirstTask(x => Math.Pow(x, 3) - x, 0, 1, 0.001, "interval_f8.xlsx");



            LabLogger.ExecuteFirstTaskMulti(field,
                new Dimensions(new[] {1.0, 5.0, 2.0}),
                0.1,
                new Dimensions(new[] {0.1, 0.1, 0.1}));

            //Console.WriteLine(MinimumSearch.BinarySearch(new CountableFunc(func, -1, 6, 0.001)));
            //Console.WriteLine(MinimumSearch.GoldenRatio(new CountableFunc(func, -1, 6, 0.001)));
            //Console.WriteLine(MinimumSearch.FibonacciMethod(new CountableFunc(func, -1, 6, 0.001)));
            //Console.WriteLine(MinimumSearch.DirectSearch(new CountableFunc(func, 3, 3, 0.001)));

            //MultidimensionalMinimumSearch.GradientDescent(
            //        new CountableMultiDimensionalFunc(field,
            //            new Dimensions(new[] {1.0, 5.0, 2.0}),
            //            0.1,
            //            new Dimensions(new[] {0.1, 0.1, 0.1})))
            //    .Coords
            //    .ToList()
            //    .ForEach(Console.WriteLine);

            //MultidimensionalMinimumSearch.DirectSearch(
            //        x => x[0] * x[1] + 5,
            //        new Dimensions(new double[] {1, -2}),
            //        new Dimensions(new double[] {3, 1}),
            //        0.001)
            //    .Coords
            //    .ToList()
            //    .ForEach(Console.WriteLine);
        }
    }
}