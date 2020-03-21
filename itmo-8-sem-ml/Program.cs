using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace itmo_8_sem_ml
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Codeforces.TaskJ.Run();
        }

        public static Int32[] ReadArray() => Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

        public static (Int32, Int32) ReadPair()
        {
            int[] el = Program.ReadArray();
            return (el[0], el[1]);
        }

        public static T2 Return<T1, T2>(this T1 input, Func<T1, T2> result) => result(input);
        public static List<T2> To<T1, T2>(this List<T1> input, Func<T1, T2> result) => input.Select(result).ToList();

        public static List<double> Rank(this List<Int32> data)
        {
            Dictionary<int, double> map = data
                .OrderBy(t => t)
                .Select((v, i) => (v, i + 1))
                .GroupBy(t => t.Item1)
                .Select(g => (g.Key, g.Average(t => t.Item2)))
                .ToDictionary(t => t.Item1, t => t.Item2);
            return data.To(v => map[v]);
        }
    }
}