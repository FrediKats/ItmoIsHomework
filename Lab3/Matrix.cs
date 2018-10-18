using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    class Matrix
    {
        private double[,] _data;

        public double[,] Data => _data; //TODO: rewrite

        public Matrix(double[,] data)
        {
            _data = data;
        }

        public void DiagonalForm()
        {
            if (Data.GetLength(0) >= Data.GetLength(1))
            {
                throw new ArgumentException();
            }
        }
    }
}
