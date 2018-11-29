using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lab3
{
    public static class Extensions
    {
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            int index = 0;

            foreach (var el in source)
            {
                if (element.Equals(el))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        public static (int I, int J) IndexOfMin<T>(this IEnumerable<IEnumerable<T>> source, Func<T, T> selector)
        {
            var min = source.Select((x, i) => new { Value = x.Min(selector), I = i, J = x.Select(selector).IndexOf(x.Min(selector)) })
                .OrderBy(x => x.Value).ElementAt(0);

            return (min.I, min.J);
        }

        public static List<List<double>> DiagonalForm(this List<List<double>> matrix)
        {
            //write more exceptions
            if (matrix.All(x => x[0].Equals(0)))
            {
                throw new Exception();
            }

            List<List<double>> matrixClone = matrix.CloneList();

            var order = matrixClone.Aggregate((x, y) => x.Zip(y, (a, b) => a + b).ToList())
                                    .Take(matrixClone.Count)
                                    .Select((x, i) => new {Index = i, Count = x})
                                    .OrderBy(x => x.Count)
                                    .Select(x => x.Index);

            List<List<double>> data = matrix.CloneList();

            foreach (var el in order)
            {
                var index = el == 0 ? data.Count - 1 : matrixClone.Select(x => x[el]).IndexOf(1);
                data[el] = matrixClone[index];
                matrixClone[index] = Enumerable.Repeat(0.0, matrixClone[0].Count).ToList();
            }

            for (int i = 0; i < data.Count; i++)
            {
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

        public static void Dump<T>(this IEnumerable<T> matrix)
        {
            Console.WriteLine(string.Join("\t", matrix));
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
