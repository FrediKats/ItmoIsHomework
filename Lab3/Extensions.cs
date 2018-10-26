using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    public static class Extensions
    {
        public static Fraction[][] DiagonalForm(this Fraction[][] matrix)
        {
            //write more exceptions

            Fraction[][] data = matrix.Clone() as Fraction[][];

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

        public static string ToMatrixString(this Fraction[][] matrix)
        {
            return string.Join("\n", matrix.Select(x => string.Join("\t", x)));
        }
    }
}
