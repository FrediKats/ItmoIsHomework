using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AppliedMath.Lab2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const bool isPrintOutput = true;

            double[][] matrix =
            {
                new[] {0.4, 0, 0.6, 0, 0, 0, 0, 0},
                new[] {0.7, 0.1, 0.2, 0, 0, 0, 0, 0},
                new[] {0, 0, 0.6, 0.3, 0.1, 0, 0, 0},
                new[] {0, 0, 0.8, 0.1, 0.1, 0, 0, 0},
                new[] {0, 0, 0.1, 0.4, 0.5, 0, 0, 0},
                new[] {0, 0, 0, 0.2, 0.3, 0.1, 0.2, 0.2},
                new[] {0, 0, 0, 0, 0, 0.1, 0.8, 0.1},
                new[] {0, 0, 0, 0, 0, 0.2, 0.6, 0.2}
            };

            
            Matrix<double> p = Matrix.Build.DenseOfRowArrays(matrix).Transpose();

            Console.WriteLine("First try");
            Vector<double> firstStart = Vector.Build.Dense(new double[] {1, 0, 0, 0, 0, 0, 0, 0});
            Vector<double> firstResult = MarkovChainExecutor.RunBrutForce(p, firstStart, isPrintOutput);

            Console.WriteLine("\n\nSecond try");
            Vector<double> secondStart = Vector.Build.Dense(new[] {0.2, 0.3, 0.2, 0.2, 0.02, 0.03, 0.03, 0.02});
            Vector<double> secondResult = MarkovChainExecutor.RunBrutForce(p, secondStart, isPrintOutput);

            Console.WriteLine("\n\n With \\pi * P = \\pi");
            Vector<double> answer = MarkovChainExecutor.SolverWithSystemOfLinearEquations(p);
            Console.WriteLine(answer.ToVectorString());
        }
    }
}