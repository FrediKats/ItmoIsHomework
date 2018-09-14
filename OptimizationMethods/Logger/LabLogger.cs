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
            CountableFunc data;
            for (double e = 0.1; e >= 0.000001; e /= 10)
            {
                data = new CountableFunc(function, left, right, e);
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
                new List<LogData>
            {
                new LogData(binData, "Binary search", binDataResult, binEps),
                new LogData(goldData, "GoldDiv search", goldDataResult, goldEps),
                new LogData(fiboData, "Fibo search", fiboDataResult, fiboEps),
                new LogData(directSearch, "Direct search search", directSearchResult, dirEps),
            }, fileName);
        }

        public static void ExecuteFirstTaskMulti(Func<Dimensions, double> field, Dimensions point,
            double functionEpsilon,
            Dimensions parameterEpsilons)
        {
            var funcData = new CountableMultiDimensionalFunc(field, point, functionEpsilon, parameterEpsilons);
            var res = MultidimensionalMinimumSearch.GradientDescent(funcData);
            ExcelLogger.LogMultiDimensional(funcData, res, "gradient.xlsx");
        }
    }
}