using System;
using Lab1.Logger;
using Lab1.Models;

namespace Lab1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //LabLogger.GenerateLineSearchReport(x => Math.Pow(x - 5, 2), -1, 6, 0.001, "interval_f1.xlsx");
            LabLogger.GenerateLineSearchReport(Math.Sin, -Math.PI / 2, Math.PI / 2, 0.001, "interval_f2.xlsx");
            //LabLogger.GenerateLineSearchReport(Math.Cos, 0, Math.PI, 0.001, "interval_f3.xlsx");
            //LabLogger.GenerateLineSearchReport(x => Math.Pow(x - 2, 2), -2, 20, 0.001, "interval_f4.xlsx");
            //LabLogger.GenerateLineSearchReport(x => Math.Pow(x - 15, 2) + 5, 2, 200, 0.001, "interval_f5.xlsx");
            //LabLogger.GenerateLineSearchReport(Math.Exp, 0, 100, 0.001, "interval_f6.xlsx");
            //LabLogger.GenerateLineSearchReport(x => Math.Pow(x, 2) + 2 * x - 4, -10, -20, 0.001, "interval_f7.xlsx");
            //LabLogger.GenerateLineSearchReport(x => Math.Pow(x, 3) - x, 0, 1, 0.001, "interval_f8.xlsx");

            //Func<Dimensions, double> field = x =>
            //{
            //    double c = 0;
            //    foreach (var a in x.Coords)
            //    {
            //        c += a * a;
            //    }
            //    return c;
            //};

            //var f0 = new CountableMultiDimensionalFunc(field,
            //    new Dimensions(new[] { 1.0, 5.0, 2.0 }),
            //    0.01,
            //    new Dimensions(new[] { 0.1, 0.1, 0.1 }));

            var f1 = new CountableMultiDimensionalFunc(
                x => 100 * Math.Pow(x[1] - Math.Pow(x[0], 2), 2) + Math.Pow(1 - x[0], 2),
                new Dimensions(new [] {1.0, 5.0}),
                0.01,
                new Dimensions(new [] {1.0, 1.0}));

            //var f7 = new CountableMultiDimensionalFunc(
            //    x => Math.Pow(x[0] + x[1], 2) + 5 * Math.Pow(x[2] + x[3], 2) + Math.Pow(x[1] + 2 * x[2], 4)
            //         + 10 * Math.Pow(x[0] - x[3], 4),
            //    new Dimensions(new[] { 1.0, 5.0, 2.0, 1.0}),
            //    0.01,
            //    new Dimensions(new[] { 0.1, 0.1, 0.1, 1.0 })
            //    );

            LabLogger.GenerateGradientReport(
                x => 100 * Math.Pow(x[1] - Math.Pow(x[0], 2), 2) + Math.Pow(1 - x[0], 2),
                new Dimensions(new[] { 5.0, 5.0 }),
                new Dimensions(new[] { 0.001, 0.001 }),
                "gradient.xlsx");

            //TestTools.OneDimensionalTest();
            //TestTools.MultiDimensionalTest();

            // MultidimensionalMinimumSearch.GradientDescent(
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