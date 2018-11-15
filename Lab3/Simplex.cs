using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Lab3
{
    public class Simplex
    {
        private Fraction[][] _matrix;
        private int[] _basis;

        public Fraction[] Plan => Enumerable
            .Range(0, _matrix[0].Length - 1)
            .Select(x => _basis.Contains(x)
                ? _matrix[_basis.IndexOf(x) + 1].Last()
                : 0)
            .ToArray();

        public Fraction Value => -_matrix[0].Last();

        public Simplex(IEnumerable<IEnumerable<Fraction>> A, IEnumerable<Fraction> b, IEnumerable<Fraction> c)
        {

            _matrix = A.Select((x, i) => x.Concat(Enumerable.Repeat(new Fraction(0), i)
                                                            .Append(1)
                                                            .Concat(Enumerable.Repeat(new Fraction(0), b.Count() - i - 1))))
                        .Zip(b, (x, y) => x.Append(y)
                                            .ToArray())
                        .Prepend(c.Concat(Enumerable.Repeat(new Fraction(0), b.Count() + 1))
                                    .ToArray())
                        .ToArray();

            _matrix.Dump();
            Console.WriteLine();
            _matrix = _matrix.Prepend(Enumerable.Repeat(new Fraction(0), c.Count())
                                                .Concat(Enumerable.Repeat(new Fraction(1), b.Count()))
                                                .Append(0)
                                                .ToArray())
                                .ToArray();
#if DEBUG
            _matrix.Dump();
            Console.WriteLine();
#endif
            for (int i = 2; i < _matrix.Length; i++)
            {
                _matrix[0] = _matrix[0].Zip(_matrix[i], (x, y) => x - y).ToArray();
            }

            _matrix.Dump();
            Console.WriteLine();

            _basis = Enumerable.Range(c.Count() + 1, b.Count())
                                .ToArray();

            Solve(2);

            if (_matrix[0].Last() != 0)
            {
                throw new Exception("System of equations cannot be solved");
            }

            _matrix = _matrix.Skip(1)
                            .Select(x => x.Take(c.Count())
                                            .Append(x.Last())
                                            .ToArray())
                            .ToArray();
            _matrix.Dump();

            Solve(1);
        }

        private void Solve(int numberOfFunctions) //le костыль
        {
            while (_matrix[0].Take(_matrix[0].Length - 1).Any(x => x < 0))
            {
                var coefficients = _matrix[0].Take(_matrix[0].Length - 1);
                int toBasis = coefficients.IndexOf(coefficients.Min());

                var ratios = _matrix.Skip(numberOfFunctions).Select(x => x[toBasis] > 0
                                                                        ? (double)(x[x.Length - 1] / x[toBasis])
                                                                        : double.PositiveInfinity);

                int fromBasis = ratios.IndexOf(ratios.Min()) + numberOfFunctions;
                Fraction solver = _matrix[fromBasis][toBasis];

                //Console.WriteLine(fromBasis + " " + toBasis);
                //Console.WriteLine(solver);
                //Console.WriteLine();
                
                _matrix[fromBasis] = _matrix[fromBasis].Select(x => x / solver)
                                                        .ToArray();

                _basis[fromBasis - numberOfFunctions] = toBasis;

                _matrix = _matrix.Select((row, i) => i == fromBasis
                                                ? row
                                                : row.Zip(_matrix[fromBasis], (x, y) => x - y * _matrix[i][toBasis] / _matrix[fromBasis][toBasis])
                                                     .ToArray())
                             .ToArray();

                _matrix.Dump();
                Console.WriteLine();
                Console.WriteLine(string.Join(", ", Plan));
                Console.WriteLine();
            }
        }
    }
}
