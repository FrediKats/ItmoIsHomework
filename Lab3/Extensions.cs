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
            return source.Select((x, i) => new { Value = x, Index = i })
                .First(x => x.Value.Equals(element))
                .Index;
        }

        //rewrite this
        public static (int I, int J) IndexOfMin<T>(this IEnumerable<IEnumerable<T>> source) where T : IComparable
        {
            var minimums = source.Select((x, i) => new { Value = x.Min(), I = i, J = x.IndexOf(x.Min()) });
            var el = minimums.Aggregate((x, y) => y.Value.CompareTo(x.Value) < 0 ? y : x); //srsly???
            return (el.I, el.J);
        }

        //public static IEnumerable<int> Range(this Enumerable source, int start, int count, Func<int, int> rule)
        //{

        //}

        public static Fraction[][] DiagonalForm(this Fraction[][] matrix)
        {
            //write more exceptions

            Fraction[][] data = matrix.Select(x => x.Select(y => y).ToArray()).ToArray();

            if (data.Select(x => x[0]).All(x => x == 0))
            {
                throw new Exception();
            }

            data = data.OrderByDescending(x => Math.Abs((double)x[0])).ToArray();

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    data[i] = data[i].Zip(data[j], (curr, prev) => curr - prev * data[i][j]).ToArray();
                }

                data[i] = data[i].Select(x => x / data[i][i]).ToArray();

                for (int j = i - 1; j >= 0; j--)
                {
                    data[j] = data[j].Zip(data[i], (curr, prev) => curr - prev * data[j][i]).ToArray();
                }
            }

            return data;
        }

        public static void Dump<T>(this IEnumerable<IEnumerable<T>> matrix)
        {
            Console.WriteLine(string.Join("\n", matrix.Select(x => string.Join("\t", x))));
        }
    }
}
