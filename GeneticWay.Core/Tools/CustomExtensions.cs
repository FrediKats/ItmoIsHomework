using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.Tools
{
    public static class CustomExtensions
    {
        public static IEnumerable<SimReport> OptimalOrder(this IEnumerable<SimReport> reports)
        {
            return reports.OrderBy(p => p.FinishStatus)
                .ThenBy(p => p.Distance)
                .ThenBy(p => p.FinalSpeed)
                .ThenBy(p => p.IterationCount)
                .ToList();
        }

        public static string ToChar(this FinishStatus s)
        {
            switch (s)
            {
                case FinishStatus.Done:
                    return "+";
                case FinishStatus.IterationLimit:
                    return "L";
                case FinishStatus.OutOfRange:
                    return "R";
                //case FinishStatus.InZone:
                //    return "Z";
                default:
                    throw new ArgumentOutOfRangeException(nameof(s), s, null);
            }
        }

        public static List<T> Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Generator.Random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        public static T[,] ToMatrix<T>(this IEnumerable<T> array, int height, int width)
        {
            var res = new T[height, width];
            int i = 0;
            foreach (var element in array)
            {
                res[i / width, i % width] = element;
                i++;
            }

            return res;
        }
    }
}