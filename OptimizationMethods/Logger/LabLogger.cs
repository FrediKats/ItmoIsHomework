using System;
using Lab1.Models;

namespace Lab1.Logger
{
    public class LabLogger
    {
        public static void ExecuteFirstTask(Func<double, double> function, double left, double right, double epsilon)
        {
            var funcData = new CountableFunc(function, left, right, epsilon);
            MinimumSearch.BinarySearch(funcData);
            ExcelLogger.LogInterval(funcData, "binary interval.xlsx");
        }

        public static void ExecuteFirstTaskMulti(Func<double[], double> field, double[] point, double functionEpsilon,
            double[] parameterEpsilons)
        {
            var funcData = new CountableMultiDimensionalFunc(field, point, functionEpsilon, parameterEpsilons);
            var res = MultidimentionalMinimumSearch.GradientDescent(funcData);
            ExcelLogger.LogMultiDimensional(funcData, res, "gradient.xlsx");
        }
    }
}