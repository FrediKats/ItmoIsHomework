using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AppliedMath.Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] data = DataReader.ReadInput("InputData.txt");
            List<int> ints = data.Select(int.Parse).OrderBy(i => i).ToList();

            StatisticAggregation aggregation = new StatisticAggregation(ints);
            Console.WriteLine(aggregation);

            File.WriteAllText("out.txt", string.Join("\n", ints));
        }
    }
}
