using System;
using System.Collections.Generic;
using System.Linq;

namespace AppliedMath.Lab1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] data = DataReader.ReadInput("InputData.txt");
            List<int> valueList = data.Select(int.Parse).OrderBy(i => i).ToList();

            var aggregation = new StatisticAggregation(valueList);
            Console.WriteLine(aggregation);
        }
    }
}