using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace AppliedMath.Lab2
{
    public static class MarkovChainExecutor
    {
        public static Vector<double> RunBrutForce(Matrix<double> m, Vector<double> current, bool printData = false)
        {
            Vector<double> prev;
            double delta;
            if (printData)
            {
                Console.WriteLine($"Current state: \n{current.ToVectorString()}");
            }

            do
            {
                prev = current;
                current = m.Multiply(current);
                delta = MathHelper.LengthToVector(prev, current);
                if (printData)
                {
                    Console.WriteLine($"Next state is \n{current.ToVectorString()}with delta {delta}\n");
                }
            } while (delta > 1e-8);

            return current;
        }

        public static Vector<double> SolverWithSystemOfLinearEquations(Matrix<double> p)
        {
            for (var i = 0; i < p.RowCount; i++)
            {
                p[i, i] = p[i, i] - 1;
            }

            p = p.InsertRow(p.RowCount, Vector.Build.Dense(p.ColumnCount, 1));
            Vector<double> result = Vector.Build.Dense(p.RowCount, i => i == p.RowCount - 1 ? 1 : 0);

            return p.Solve(result);
        }
    }
}