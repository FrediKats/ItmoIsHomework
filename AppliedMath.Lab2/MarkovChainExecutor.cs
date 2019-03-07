using MathNet.Numerics.LinearAlgebra;
using Vector = MathNet.Numerics.LinearAlgebra.Double.Vector;

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
            } while (MathHelper.LengthToVector(prev, current) > 1e-15);

            return current;
        }

        public static Vector<double> Convertor(Matrix<double> p)
        {
            for (int i = 0; i < p.RowCount; i++)
            {
                p[i, i] = p[i, i] - 1;
            }

            p = p.InsertRow(p.RowCount, Vector.Build.Dense(p.ColumnCount, 1));
            Vector<double> result = Vector.Build.Dense(p.RowCount, i => i == p.RowCount - 1 ? 1 : 0);

            return p.Solve(result);
        }
    }
}