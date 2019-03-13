using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AppliedMath.Lab2
{
    class Program
    {
        private static void Main(string[] args)
        {
            bool isPrintOutput = true;
            Matrix<double> p = Matrix.Build.DenseOfRowArrays(MatrixReader.ReadMatrix()).Transpose();

            Console.WriteLine("First try");
            Vector<double> firstStart = Vector.Build.Dense(new double[] { 1, 0, 0, 0, 0, 0, 0, 0 });
            Vector<double> firstResult = MarkovChainExecutor.RunBrutForce(p, firstStart, isPrintOutput);
            //Console.WriteLine(firstResult);

            Console.WriteLine("\n\nSecond try");
            Vector<double> secondStart = Vector.Build.Dense(new[] { 0.2, 0.3, 0.2, 0.2, 0.02, 0.03, 0.03, 0.02 });
            Vector<double> secondResult = MarkovChainExecutor.RunBrutForce(p, secondStart, isPrintOutput);
            //Console.WriteLine(secondResult);

            Console.WriteLine("\n\n With \\pi * P = \\pi");
            Vector<double> answer = MarkovChainExecutor.SolverWithSystemOfLinearEquations(p);
            Console.WriteLine(answer.ToVectorString());
        }
    }
}
