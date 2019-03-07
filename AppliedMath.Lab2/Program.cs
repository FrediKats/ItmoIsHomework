using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AppliedMath.Lab2
{
    class Program
    {
        private static void Main(string[] args)
        {
            Matrix<double> p = Matrix.Build.DenseOfRowArrays(MatrixReader.ReadMatrix()).Transpose();

            Vector<double> firstStart = Vector.Build.Dense(new double[] { 1, 0, 0, 0, 0, 0, 0, 0 });
            Vector<double> firstResult = MarkovChainExecutor.RunBrutForce(p, firstStart);

            Vector<double> secondStart = Vector.Build.Dense(new[] { 0.2, 0.3, 0.2, 0.2, 0.02, 0.03, 0.03, 0.02 });
            Vector<double> secondResult = MarkovChainExecutor.RunBrutForce(p, secondStart);

            Vector<double> answer = MarkovChainExecutor.Convertor(p);
            Console.WriteLine(firstResult);
            Console.WriteLine(secondResult);
            Console.WriteLine(answer);
        }
    }
}
