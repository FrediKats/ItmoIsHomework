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
            Console.WriteLine(_data.ToMatrixString());
        }

        public void Solve()
        {
            while (_data[0].Any(x => x < 0))
            {
                int toBasis = _data[0].IndexOf(_data[0].Max());
                Console.WriteLine(toBasis);

                var ratios = _data.Select(x => x[toBasis] > 0
                                                ? (double)(x[x.Length - 1] / x[toBasis])
                                                : double.PositiveInfinity);

                int fromBasis = ratios.IndexOf(ratios.Min());

                Console.WriteLine(fromBasis);
                break;
            }
        }
    }
}
