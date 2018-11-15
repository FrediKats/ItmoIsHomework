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

            double[] producers = { 50, 30, 10 };
            double[] consumers = { 30, 30, 10, 20 };

            double[][] tariffs =
            {
                new double[] { 1, 2, 4, 1 },
                new double[] { 2, 3, 1, 5 },
                new double[] { 3, 2, 4, 4 },
            };

            TransportationMatrix transportation = new TransportationMatrix(producers, consumers, tariffs);
            transportation.Plan.Dump();
        }
    }
}
