using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Lab3
{
    class Matrix : IEnumerable<Fraction[]>, ICloneable
    {
        private Fraction[][] _data;

        public int M => _data.Length;
        public int N => _data[0].Length;

        public Matrix(Fraction[][] data)
        {
            if (data.Any(x => x.Length != data[0].Length))
            {
                throw new ArgumentException();
            }

            _data = data;
        }

        public void DiagonalForm() //mb return new one
        {
            //write more exceptions

            if (_data.Select(x => x[0]).All(x => x == 0))
            {
                throw new Exception();
            }

            _data = _data.OrderByDescending(x => Math.Abs((double)x[0])).ToArray();

            for (int i = 0; i < _data.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    _data[i] = _data[i].Zip(_data[j], (curr, prev) => curr - prev * _data[i][j]).ToArray();
                }

                _data[i] = _data[i].Select(x => x / _data[i][i]).ToArray();

                for (int j = i - 1; j >= 0; j--)
                {
                    _data[j] = _data[j].Zip(_data[i], (curr, prev) => curr - prev * _data[j][i]).ToArray();
                }
            }
        }

        public IEnumerator<Fraction[]> GetEnumerator()
        {
            return ((IEnumerable<Fraction[]>)_data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("\n", this.Select(x => string.Join("\t", x.Select(y => y))));
        }

        public object Clone()
        {
            return new Matrix(_data);
        }
    }
}
