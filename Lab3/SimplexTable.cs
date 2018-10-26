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

            Fraction[][] system = new Fraction[][]
            {
                new Fraction[] { 1 }.Concat(c.Take(c.Length - 1).Select(x => -x).Append(c.Last()))
                                    .ToArray()
            }.Concat(A.Zip(b, (x, y) => new Fraction[] { 0 }.Concat(x.Append(y))
                                                            .ToArray()))
                .ToArray().DiagonalForm();

            _data = system.Select(x => x.Skip(system.Length).ToArray()).ToArray();
        }

        public void Solve()
        {
            while (_data[0].Any(x => x < 0))
            {

            }
        }
    }
}
