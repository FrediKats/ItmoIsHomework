using System;
using System.Collections.Generic;
using Lab1.Models;

namespace Lab1.Logger
{
    public class LabLogger
    {
        public static void ExecuteFirstTask(Func<double, double> function, double left, double right, double epsilon, string fileName)
        {
            var binData = new CountableFunc(function, left, right, epsilon);
            var binDataResult = MinimumSearch.BinarySearch(binData);

            var goldData = new CountableFunc(function, left, right, epsilon);
            var goldDataResult = MinimumSearch.GoldenRatio(goldData);

            var fiboData = new CountableFunc(function, left, right, epsilon);
            var fiboDataResult = MinimumSearch.FibonacciMethod(fiboData);

            var directSearch = new CountableFunc(function, left, right, epsilon);
            var directSearchResult = MinimumSearch.DirectSearch(directSearch);

            List<(double, int)> binEps = new List<(double, int)>();
            List<(double, int)> goldEps = new List<(double, int)>();
            List<(double, int)> fiboEps = new List<(double, int)>();
            List<(double, int)> dirEps = new List<(double, int)>();
            for (double e = 0.1; e >= 0.000001; e /= 10)
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
                new LinearLogData(directSearch, "Direct search search", directSearchResult, dirEps),
            }, fileName);
        }

        public static void ExecuteFirstTaskGradient(List<CountableMultiDimensionalFunc> argsList, string fileName)
        {
            var result = new List<(CountableMultiDimensionalFunc, Dimensions)>();

            foreach (var args in argsList)
            {
                var res = MultidimensionalMinimumSearch.GradientDescent(args);
                result.Add((args, res));
            }
            ExcelLogger.LogMultiDimensional(result, fileName);
        }
    }
}