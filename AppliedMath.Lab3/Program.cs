using System;
using System.IO;
using System.Linq;
using AppliedMath.Lab2;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AppliedMath.Lab3
{
    class Program
    {
        public static double[][] ReadMatrix()
        {
            return File
                .ReadAllLines("Input.txt")
                .Select(row => row.Split("\t").Select(double.Parse).ToArray())
                .ToArray();
        }

        static void Main(string[] args)
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
