using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public static class MinimumSearch
    {
        private static double PHI => (Math.Sqrt(5) + 1) / 2;

        public static double BinarySearch(Func<double, double> function, double left, double right, double epsilon)
        {
            double delta = 0.8 * epsilon / 2;

            while (right - left >= epsilon)
            {
                double x1 = (left + right) / 2 - delta;
                double x2 = (left + right) / 2 + delta;

                double f1 = function(x1);
                double f2 = function(x2);

                if (f1 <= f2)
                {
                    right = x2;
                }

                if (f1 >= f2)
                {
                    left = x1;
                }
            }

            return (left + right) / 2;
        }

        public static double GoldenRatio(Func<double, double> function, double left, double right, double epsilon)
        {
            double x1 = left + (right - left) * (3 - Math.Sqrt(5)) / 2;
            double x2 = left + (right - left) * (Math.Sqrt(5) - 1) / 2;
            double f1 = function(x1);
            double f2 = function(x2);

            while (right - left >= epsilon)
            {
                if (f1 > f2)
                {
                    left = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = left + (right - left) * (Math.Sqrt(5) - 1) / 2;
                    f2 = function(x2);
                }
                else
                {
                    right = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = left + (right - left) * (3 - Math.Sqrt(5)) / 2;
                    f1 = function(x1);
                }
            }

            return (left + right) / 2;
        }

        public static double FibonacciMethod(Func<double, double> function, double left, double right, double epsilon)
        {
            int n = FibonacciOrder((int)Math.Ceiling((right - left) / epsilon));

            int[] fibNumbers = new int[n];
            fibNumbers[1] = 1;

            for (int i = 2; i < n; i++)
            {
                fibNumbers[i] = fibNumbers[i - 1] + fibNumbers[i - 2];
            }

            n--;
            int k = 0;

            double x1 = left + fibNumbers[n - 2] * (right - left) / fibNumbers[n];
            double x2 = left + fibNumbers[n - 1] * (right - left) / fibNumbers[n];
            double f1 = function(x1);
            double f2 = function(x2);

            while (++k != n)
            {
                if (f1 > f2)
                {
                    left = x1;
                    x1 = x2;
                    x2 = left + fibNumbers[n - k - 1] * (right - left) / fibNumbers[n - k];
                    f1 = f2;
                    f2 = function(x2);
                }
                else
                {
                    right = x2;
                    x2 = x1;
                    x1 = left + fibNumbers[n - k - 2] * (right - left) / fibNumbers[n - k];
                    f2 = f1;
                    f1 = function(x1);
                }
            }

            return (left + right) / 2;
        }

        private static int FibonacciOrder(int number) => number == 0 ? 0 : (int)Math.Round((Math.Log(number) + Math.Log(5) / 2) / Math.Log(PHI));

        //private static int FibonacciNumber(int n) => (int)Math.Round((Math.Pow(Phi, n) - Math.Pow(-Phi, -n)) / (2 * Phi - 1));
    }
}
