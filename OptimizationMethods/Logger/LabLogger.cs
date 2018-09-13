using System;
using System.Collections.Generic;
using Lab1.Models;

namespace Lab1.Logger
{
    public class LabLogger
    {
        public static void ExecuteFirstTask(Func<double, double> function, double left, double right, double epsilon)
        {
            var binData = new CountableFunc(function, left, right, epsilon);
            MinimumSearch.BinarySearch(binData);

            var goldData = new CountableFunc(function, left, right, epsilon);
            MinimumSearch.GoldenRatio(goldData);

            var fiboData = new CountableFunc(function, left, right, epsilon);
            MinimumSearch.FibonacciMethod(fiboData);

            ExcelLogger.LogInterval(new List<(CountableFunc, string)>
            {
                (binData, "Binary search"),
                (goldData, "GoldDiv search"),
                (fiboData, "Fibo search"),
            });
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