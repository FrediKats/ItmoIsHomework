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
            Fraction[] c = { 0, 0, 0, 1, -1, 0 };

            SimplexTable simplexMatrix = new SimplexTable(A, b, c);

            //var 2

            double[] producers = { 30, 90, 50 };
            double[] consumers = { 70, 60, 30 };

            double[][] tariffs =
            {
                new double[] { 8, 4, 5 },
                new double[] { 3, 7, 2 },
                new double[] { 1, 4, 6 },
            };

            TransportationMatrix transportation = new TransportationMatrix(producers, consumers, tariffs);
        }
    }
}
