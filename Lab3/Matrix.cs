using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lab3
{
    class Matrix
    {
        private double[][] _data;

        //public double[][] Data => _data; //TODO: rewrite or delete

        public Matrix(double[][] data)
        {
            _data = data;
        }

        public void DiagonalForm()
        {
            //write more exceptions

            if (_data.Select(x => x[0]).All(x => x == 0))
            {
                throw new Exception();
            }

            _data = _data.OrderByDescending(x => Math.Abs(x[0])).ToArray();

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
    }
}
