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
        private static void Main(string[] args)
        {
            Func<Dimensions, double> func = x => 100 * Math.Pow(x[1] - Math.Pow(x[0], 2), 2) + Math.Pow(1 - x[0], 2);

            Console.WriteLine("Test 1: f(x) = 100(x2 - x1^2)^2 + (1 - x1)^2");
            Console.WriteLine("Expected: x1 = 1, x2 = 1");
            Console.WriteLine("Result:");

            Dimensions res = MultidimensionalMinimumSearch.GradientDescent(
                   new CountableMultiDimensionalFunc(func,
                       new Dimensions(new[] { 1.0, 5.0 }),
                       1e-1,
                       new Dimensions(new[] { 1e-1, 1e-1 })));

            res.Coords
               .ToList()
               .ForEach(Console.WriteLine);

            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();
            Console.WriteLine(func(res));
        }
    }
}