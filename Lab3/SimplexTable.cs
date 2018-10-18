using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    class SimplexTable
    {
        private double[][] _data;

        public SimplexTable(Matrix matrix/*, TargetFunction*/)
        {
            var diagonalMatrix = matrix.Clone() as Matrix;
            diagonalMatrix.DiagonalForm();

            _data = new double[matrix.M + 1][];
        }
    }
}
