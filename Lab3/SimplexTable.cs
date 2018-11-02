using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    class SimplexTable
    {
        private Fraction[][] _data;

        public SimplexTable(Fraction[][] A, Fraction[] b, Fraction[] c)
        {
            //if (matrix.N != targetFunction.Length)
            //{
            //    throw new ArgumentException();
            //}

            Fraction[][] system = A.Zip(b, (x, y) => x.Prepend(0)
                                                        .Append(y)
                                                        .ToArray())
                                    .Prepend(c.Take(c.Length - 1)
                                                .Select(x => -x)
                                                .Prepend(1)
                                                .Append(c.Last())
                                                .ToArray())
                                    .ToArray()
                                    .DiagonalForm();

            _data = system.Select(x => x.Skip(system.Length).ToArray()).ToArray();
            _data.Dump();
            Console.WriteLine();
        }

        public void Solve()
        {
            while (_data[0].Take(_data[0].Length).Any(x => x > 0))
            {
                var coefficients = _data[0].Take(_data[0].Length - 1);
                int toBasis = coefficients.IndexOf(coefficients.Max());

                var ratios = _data.Skip(1).Select(x => x[toBasis] > 0
                                                        ? (double)(x[x.Length - 1] / x[toBasis])
                                                        : double.PositiveInfinity);

                int fromBasis = ratios.IndexOf(ratios.Min()) + 1;
                Fraction solver = _data[fromBasis][toBasis];

                Console.WriteLine(toBasis);
                Console.WriteLine(fromBasis);
                Console.WriteLine();

                _data[fromBasis][toBasis] = 1;
                _data[fromBasis] = _data[fromBasis].Select(x => x / solver)
                                                    .ToArray();

                _data.Dump();
                Console.WriteLine();

                _data = _data.Select((row, i) => i == fromBasis
                                                ? row
                                                : row.Select((e, j) => j == toBasis
                                                                        ? -e / solver
                                                                        : e - _data[fromBasis][j] * _data[i][toBasis])
                                                     .ToArray())
                             .ToArray();

                _data.Dump();
                Console.WriteLine();
                //break;
            }
        }
    }
}
