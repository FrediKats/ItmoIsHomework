using System;
using System.Linq;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Fraction[][] A = 
            {
                new Fraction[] { 1, 0, 0, 1, -2 },
                new Fraction[] { 0, 1, 0, -2, 1 },
                new Fraction[] { 0, 0, 1, 3, 1 }
            };

            Fraction[] b = { 1, 2, 3 };
            Fraction[] c = { 0, 0, 0, 1, -1 , 0 };

            SimplexTable simplexMatrix = new SimplexTable(A, b, c);
            simplexMatrix.Solve();
        }
    }
}
