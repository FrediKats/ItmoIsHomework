using System;
using System.Collections.Generic;
using System.Linq;
using Lab1.Logger;
using Lab1.Models;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace Lab1
{
    internal class Program
    {
        private static void testing(){
            Console.WriteLine("Test 1: f (x) = sin (x), x ∈ [-pi/2, pi/2]");
            Console.WriteLine("Expected: 0");
            Console.WriteLine("Result:");
            printResult(x => Math.Sin(x), -Math.PI/2, Math.PI/2);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 2: f (x) = cos (x), x ∈ [0, pi]");
            Console.WriteLine("Expected: pi");
            Console.WriteLine("Result:");
            printResult(x => Math.Cos(x), 0, Math.PI);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 3: (x - 2)^2, x ∈ [-2, 20]");
            Console.WriteLine("Expected: 2");
            Console.WriteLine("Result:");
            printResult(x => Math.Pow(x-2, 2), -2, 20);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 4: (x - 15)^2 + 5, x ∈ [2, 200]");
            Console.WriteLine("Expected: 15");
            Console.WriteLine("Result:");
            printResult(x => Math.Pow(x-15, 2)+5, 2, 200);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 5: (x + 5)^4, x ∈ [-10, 15]");
            Console.WriteLine("Expected: -5");
            Console.WriteLine("Result:");
            printResult(x => Math.Pow(x+5, 4), -10, 15);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 6: exp (x), x ∈ [0, 100]");
            Console.WriteLine("Expected: 0");
            Console.WriteLine("Result:");
            printResult(x => Math.Exp(x), 0, 100);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 7: x^2 + 2x - 4, x ∈ [-10, 20]");
            Console.WriteLine("Expected: -1");
            Console.WriteLine("Result:");
            printResult(x => Math.Pow(x, 2)+2*x-4, -10, 20);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

            Console.WriteLine("Test 8: x^3 - x, x ∈ [0, 1]");
            Console.WriteLine("Expected: ~0,57-0,58");
            Console.WriteLine("Result:");
            printResult(x =>  Math.Pow(x, 3)-x, 0, 1);
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();

        }
         private static void printResult(Func<double, double> fun, double min, double max ){
            Console.WriteLine("BinarySearch:   " + MinimumSearch.BinarySearch(new CountableFunc(fun, min, max , 0.001)));
            Console.WriteLine("GoldenRatio:   " + MinimumSearch.GoldenRatio(new CountableFunc(fun, min, max, 0.001)));
            Console.WriteLine("FibonacciMethod:   " + MinimumSearch.FibonacciMethod(new CountableFunc(fun, min, max, 0.001)));
            Console.WriteLine("DirectSearch:   " + MinimumSearch.DirectSearch(new CountableFunc(fun, min, max, 0.001)));
        }

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
            // LabLogger.ExecuteFirstTask(func, -1, 6, 0.001, "interval_f1.xlsx");
            // LabLogger.ExecuteFirstTask(Math.Sin, -Math.PI / 2, Math.PI / 2, 0.001, "interval_f2.xlsx");
            // LabLogger.ExecuteFirstTask(Math.Cos, 0, Math.PI, 0.001, "interval_f3.xlsx");
            // LabLogger.ExecuteFirstTask(x => Math.Pow(x - 2, 2), -2, 20, 0.001, "interval_f4.xlsx");
            // LabLogger.ExecuteFirstTask(x => Math.Pow(x - 15, 2) + 5, 2, 200, 0.001, "interval_f5.xlsx");
            // LabLogger.ExecuteFirstTask(Math.Exp, 0, 100, 0.001, "interval_f6.xlsx");
            // LabLogger.ExecuteFirstTask(x => Math.Pow(x, 2) + 2 * x - 4, -10, -20, 0.001, "interval_f7.xlsx");
            // LabLogger.ExecuteFirstTask(x => Math.Pow(x, 3) - x, 0, 1, 0.001, "interval_f8.xlsx");

            var f0 = new CountableMultiDimensionalFunc(field,
                new Dimensions(new[] { 1.0, 5.0, 2.0 }),
                0.01,
                new Dimensions(new[] { 0.1, 0.1, 0.1 }));

            var f1 = new CountableMultiDimensionalFunc(
                x => 100 * Math.Pow(x[1] - Math.Pow(x[0], 2), 2) + Math.Pow(1 - x[0], 2),
                new Dimensions(new [] {1.0, 5.0}),
                0.01,
                new Dimensions(new [] {1.0, 1.0}));

            var f7 = new CountableMultiDimensionalFunc(
                x => Math.Pow(x[0] + x[1], 2) + 5 * Math.Pow(x[2] + x[3], 2) + Math.Pow(x[1] + 2 * x[2], 4)
                     + 10 * Math.Pow(x[0] - x[3], 4),
                new Dimensions(new[] { 1.0, 5.0, 2.0, 1.0}),
                0.01,
                new Dimensions(new[] { 0.1, 0.1, 0.1, 1.0 })
                );

            LabLogger.ExecuteFirstTaskGradient(new List<CountableMultiDimensionalFunc> {f0, f1, f7}, "gradient.xlsx" );

            testing();
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