using System;
using System.Collections.Generic;
using Lab1.Models;

namespace Lab1.Logger
{
    public static class LabLogger
    {
        public static void GenerateLineSearchReport(Func<double, double> function, double left, double right,
            double epsilon, string fileName)
        {
            var binData = new CountableFunc(function, left, right, epsilon);
            double binDataResult = MinimumSearch.BinarySearch(binData);

            var goldData = new CountableFunc(function, left, right, epsilon);
            double goldDataResult = MinimumSearch.GoldenRatio(goldData);

            var fiboData = new CountableFunc(function, left, right, epsilon);
            double fiboDataResult = MinimumSearch.FibonacciMethod(fiboData);

            var directSearch = new CountableFunc(function, left, right, epsilon);
            double directSearchResult = MinimumSearch.DirectSearch(directSearch);

            var binEps = new List<(double, int)>();
            var goldEps = new List<(double, int)>();
            var fiboEps = new List<(double, int)>();
            var dirEps = new List<(double, int)>();
            for (var e = 0.1; e >= 1e-7; e /= 10)
            {
                var data = new CountableFunc(function, left, right, e);
                MinimumSearch.BinarySearch(data);
                binEps.Add((e, data.CallCount));

                data = new CountableFunc(function, left, right, e);
                MinimumSearch.GoldenRatio(data);
                goldEps.Add((e, data.CallCount));

                data = new CountableFunc(function, left, right, e);
                MinimumSearch.FibonacciMethod(data);
                fiboEps.Add((e, data.CallCount));

                data = new CountableFunc(function, left, right, e);
                MinimumSearch.DirectSearch(data);
                dirEps.Add((e, data.CallCount));
            }

            ExcelLogger.LogInterval(
                new List<LinearLogData>
                {
                    new LinearLogData(binData, "Binary search", binDataResult, binEps),
                    new LinearLogData(goldData, "GoldDiv search", goldDataResult, goldEps),
                    new LinearLogData(fiboData, "Fibo search", fiboDataResult, fiboEps),
                    new LinearLogData(directSearch, "Direct search search", directSearchResult, dirEps)
                }, fileName);
        }

        public static void GenerateGradientReport(Func<Dimensions, double> function, Dimensions startPoint,
            Dimensions parameterEpsilon, string fileName)
        {
            var result = new List<(CountableMultiDimensionalFunc, Dimensions, double)>();
            var maxE = 1e-7;
#if DEBUG
            maxE = 1e-4;
#endif
            for (var e = 0.1; e >= maxE; e /= 10)
            {
#if DEBUG
                Console.WriteLine($"==== Check for e = {e} =====");
                parameterEpsilon = new Dimensions(e, e);
#endif
                var data = new CountableMultiDimensionalFunc(function, startPoint.Copy(), e, parameterEpsilon);
                Dimensions res = MultidimensionalMinimumSearch.GradientDescent(data);
                result.Add((data, res, function(res)));
            }

            ExcelLogger.LogMultiDimensional(result, fileName);
        }
    }
}