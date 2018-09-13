using System;
using System.Collections.Generic;
using System.Text;
using Lab1.Models;
using Lab1.Tools;

namespace Lab1
{
    public static class MinimumSearch
    {
        public static double BinarySearch(CountableFunc funcData)
        {
            //TODO: fix
            double delta = 0.8 * funcData.Epsilon / 2;

            while (funcData.Right - funcData.Left >= funcData.Epsilon)
            {
                double x1 = (funcData.Left + funcData.Right) / 2 - delta;
                double x2 = (funcData.Left + funcData.Right) / 2 + delta;

                double f1 = funcData.Function(x1);
                double f2 = funcData.Function(x2);

                if (f1 <= f2)
                {
                    funcData.Right = x2;
                }

                if (f1 >= f2)
                {
                    funcData.Left = x1;
                }

                funcData.SaveData();
            }

            return (funcData.Left + funcData.Right) / 2;
        }

        public static double GoldenRatio(CountableFunc funcData)
        {
            double x1 = funcData.Left + (funcData.Right - funcData.Left) * (2 - Constants.PHI);
            double x2 = funcData.Left + (funcData.Right - funcData.Left) * (Constants.PHI - 1);
            double f1 = funcData.Function(x1);
            double f2 = funcData.Function(x2);

            while (funcData.Right - funcData.Left >= funcData.Epsilon)
            {
                if (f1 > f2)
                {
                    funcData.Left = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = funcData.Left + (funcData.Right - funcData.Left) * (Constants.PHI - 1);
                    f2 = funcData.Function(x2);
                }
                else
                {
                    funcData.Right = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = funcData.Left + (funcData.Right - funcData.Left) * (2 - Constants.PHI);
                    f1 = funcData.Function(x1);
                }

                funcData.SaveData();
            }

            return (x1 + x2) / 2;
        }

        public static double FibonacciMethod(CountableFunc funcData)
        {
            FiboGenerator fib = new FiboGenerator(funcData.Left, funcData.Right, funcData.Epsilon);

            double x1 = funcData.Left + fib[fib.Last - 2] * (funcData.Right - funcData.Left) / fib[fib.Last];
            double x2 = funcData.Left + fib[fib.Last - 1] * (funcData.Right - funcData.Left) / fib[fib.Last];
            double f1 = funcData.Function(x1);
            double f2 = funcData.Function(x2);

            int k = fib.Last - 2;
            while (k > 1)
            {
                if (f1 > f2)
                {
                    funcData.Left = x1;
                    x1 = x2;
                    x2 = funcData.Left + fib[k - 1] * (funcData.Right - funcData.Left) / fib[k];
                    f1 = f2;
                    f2 = funcData.Function(x2);
                }
                else
                {
                    funcData.Right = x2;
                    x2 = x1;
                    x1 = funcData.Left + fib[k - 2] * (funcData.Right - funcData.Left) / fib[k];
                    f2 = f1;
                    f1 = funcData.Function(x1);
                }

                funcData.SaveData();
                k--;
            }

            x2 = x1 + funcData.Epsilon;

            return (f1 > funcData.Function(x2) ? x1 + funcData.Right : funcData.Left + x2) / 2;
        }

        public static double DirectSearch(Func<double, double> function, double point, double epsilon)
        {
            double delta = epsilon;
            double value = function(point);

            if(value > function(point + delta))
            {
                delta = epsilon;
            }
            else if (value > function(point - delta))
            {
                delta = epsilon * -1;
            }
            else
            {
                return point;
            }

            point += delta;
            double nextValue = function(point);

            while (value > nextValue)
            {
                delta *= 2;
                point += delta;
                value = nextValue;
                nextValue = function(point);
            }

            return delta > 0
                ? FibonacciMethod(new CountableFunc(function, point - 1.5 * delta, point, epsilon))
                : FibonacciMethod(new CountableFunc(function, point, point - 1.5 * delta, epsilon));
        }
    }
}
