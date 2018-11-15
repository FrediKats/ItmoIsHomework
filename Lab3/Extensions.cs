using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lab3
{
    public static class Extensions
    {
        public static int IndexOf<T>(this IEnumerable<T> source, T element)
        {
            return source
            .Select((x, i) => new { Value = x, Index = i })
            .First(x => x.Value.Equals(element))
            .Index;
        }

        public static (int I, int J) IndexOfMin<T>(this IEnumerable<IEnumerable<T>> source) where T : IComparable
        {
            var minimums = source.Select((x, i) => new { Value = x.Min(), I = i, J = x.IndexOf(x.Min()) });
            var el = minimums.Aggregate((x, y) => y.Value.CompareTo(x.Value) < 0 ? y : x); //srsly???
            
            return (el.I, el.J);
        }

        public static List<List<double>> DiagonalForm(this List<List<double>> matrix)
        {
            //write more exceptions
            if (matrix.All(x => x[0] == 0))
            {
                throw new Exception();
            }

            List<List<double>> data = matrix.CloneList();

            data = data.OrderByDescending(x => Math.Abs(x[0])).ToList();

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i][i] == 0)
                {
                    int j;

                    for (j = 0; j < data.Count; j++)
                    {
                        if (data[j][i] != 0) break;
                    }

                    data[i] = data[i].Zip(data[j], (x, y) => x - y).ToList();
                }

                data[i] = data[i].Select(x => x / data[i][i]).ToList();

                for (int j = 0; j < i; j++)
                {
                    data[i] = data[i].Zip(data[j], (curr, prev) => curr - prev * data[i][j]).ToList();
                }

                for (int j = i - 1; j >= 0; j--)
                {
                    data[j] = data[j].Zip(data[i], (curr, prev) => curr - prev * data[j][i]).ToList();
                }
            }

            return data;
        }

        public static void Dump<T>(this IEnumerable<IEnumerable<T>> matrix)
        {
            Console.WriteLine(string.Join("\n", matrix.Select(x => string.Join("\t", x))));
        }

        public static double[][] CloneArray(this double[][] source)
        {
            return source.Select(s => s.ToArray()).ToArray();
        }

        public static List<List<double>> CloneList(this List<List<double>> source)
        {
            return source.Select(s => s.ToList()).ToList();
        }

        public static bool Compare(this IReadOnlyList<double[]> f, double[][] s)
        {
            return f
                .SelectMany(el => el)
                .Zip(s.SelectMany(el => el), (a, b) => a == b)
                .All(x => x);
        }

        public static bool Compare(this Fraction[] f, Fraction[] s)
        {
            return f
                .Zip(s, (a, b) => a == b)
                .All(x => x);
        }
    }
}
