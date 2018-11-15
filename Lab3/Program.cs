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

            //Simplex simplexMatrix = new Simplex(A, b, c);
            //Console.WriteLine(string.Join(" ",  simplexMatrix.Plan));

            double[] producers = { 120, 280, 160 };
            double[] consumers = { 130, 220, 60, 70 };
            double[][] tariffs =
            {
                new double[] { 1, 7, 9, 5 },
                new double[] { 4, 2, 6, 8 },
                new double[] { 3, 8, 1, 2 }
            };

            TransportationMatrix transportation = new TransportationMatrix(producers, consumers, tariffs);
            transportation.Plan.Dump();
        }
    }
}
