using System;
using MathNet.Numerics.LinearAlgebra;

namespace AppliedMath.Lab2
{
    public class MarkovChainExecutor
    {
        public static Vector<double> RunBrutForce(Matrix<double> m, Vector<double> current)
        {
            Vector<double> prev;

            do
            {
                prev = current;
                current = m.Multiply(current);
                Console.WriteLine(current.ToString());
                Console.WriteLine(MathHelper.LengthToVector(prev, current));
            } while (MathHelper.LengthToVector(prev, current) > 1e-15);

            return current;
        }
    }
}