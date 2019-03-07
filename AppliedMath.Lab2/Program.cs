using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AppliedMath.Lab2
{
    class Program
    {
        private static void Main(string[] args)
        {
            Matrix<double> m = Matrix.Build.DenseOfRowArrays(MatrixReader.ReadMatrix()).Transpose();
            Vector<double> current = Vector.Build.Dense(new double[] { 1, 0, 0, 0, 0, 0, 0, 0 });

            Vector<double> result = MarkExecutor.RunBrutForce(m, current);
        }
    }
}
