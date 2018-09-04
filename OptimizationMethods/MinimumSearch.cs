using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public static class MinimumSearch
    {
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

            return (right + left) / 2;
        }

        public static double GoldenRatio(Func<double, double> function, double left, double right, double epsilon)
        {
            double x1 = default;
            double x2 = default;
            double f1 = default;
            double f2 = default;

            if (right - left >= epsilon)
            {
                x1 = left + (right - left) * (3 - Math.Sqrt(5)) / 2;
                x2 = left + (right - left) * (Math.Sqrt(5) - 1) / 2;

                f1 = function(x1);
                f2 = function(x2);

                if (f1 >= f2)
                {
                    left = x1;
                }
                else
                {
                    right = x2;
                }
            }

            while (right - left >= epsilon)
            {
                if (x1 == left)
                {
                    x1 = x2;
                    f1 = f2;
                    x2 = left + (right - left) * (Math.Sqrt(5) - 1) / 2;
                    f2 = function(x2);
                }
                else
                {
                    x2 = x1;
                    f2 = f1;
                    x1 = left + (right - left) * (3 - Math.Sqrt(5)) / 2;
                    f1 = function(x1);
                }

                if (f1 >= f2)
                {
                    left = x1;
                }
                else
                {
                    right = x2;
                }
            }

            return (right + left) / 2;
        }

        public static double Fibonacci(Func<double, double> function, double left, double right, double epsilon)
        {


            return (right - left) / 2;
        }
    }
}
