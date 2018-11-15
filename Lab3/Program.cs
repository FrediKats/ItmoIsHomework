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
                new Fraction[] { 1, 2, -1, 2, 4 },
                new Fraction[] { 0, -1, 2, 1, 3 },
                new Fraction[] { 1, -3, 2, 2, 0 }
            };

            Fraction[] b = { 1, 3, 4 };
            Fraction[] c = { 1, -3, 2, 1, 4 };

            Simplex simplexMatrix = new Simplex(A, b, c);
            Console.WriteLine(string.Join(" ",  simplexMatrix.Plan));
            
            //var 2

            double[] producers = { 30, 90, 50 };
            double[] consumers = { 70, 60, 30 };

            double[][] tariffs =
            {
                new double[] { 8, 4, 5 },
                new double[] { 3, 7, 2 },
                new double[] { 1, 4, 6 },
            };

            //TransportationMatrix transportation = new TransportationMatrix(producers, consumers, tariffs);
        }
    }
}
