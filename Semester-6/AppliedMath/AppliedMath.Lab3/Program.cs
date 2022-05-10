using System;
using AppliedMath.Lab2;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AppliedMath.Lab3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Matrix<double> p = Matrix.Build.DenseOfRowArrays(MatrixReader.ReadMatrix());
            p = p.InsertRow(p.RowCount, Vector.Build.Dense(p.ColumnCount, 1));
            Vector<double> result = Vector.Build.Dense(p.RowCount, i => i == p.RowCount - 1 ? 1 : 0);

            Console.WriteLine(p);
            Console.WriteLine(result);
            Console.WriteLine(p.Solve(result).ToString());
        }
    }
}