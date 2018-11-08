using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    class SimplexTable
    {
        private Fraction[][] _data;

        public SimplexTable(IEnumerable<IEnumerable<Fraction>> A, IEnumerable<Fraction> b, IEnumerable<Fraction> c)
        {
            //if (matrix.N != targetFunction.Length)
            //{
            //    throw new ArgumentException();
            //}

            Fraction[][] equations = A.Zip(b, (x, y) => x.Prepend(0)
                                                        .Append(y)
                                                        .ToArray())
                                    .Prepend(c.Take(c.Count() - 1)
                                                .Select(x => -x)
                                                .Prepend(1)
                                                .Append(c.Last())
                                                .ToArray())
                                    .ToArray()
                                    .DiagonalForm();

            _data = equations.Select(x => x.Skip(equations.Length).ToArray()).ToArray();
            Solve();
        }

        private void Solve()
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

                _data[fromBasis][toBasis] = 1;
                _data[fromBasis] = _data[fromBasis].Select(x => x / solver)
                                                    .ToArray();

                _data = _data.Select((row, i) => i == fromBasis
                                                ? row
                                                : row.Select((e, j) => j == toBasis
                                                                        ? -e / solver
                                                                        : e - _data[fromBasis][j] * _data[i][toBasis])
                                                     .ToArray())
                             .ToArray();
            }
        }
    }
}
