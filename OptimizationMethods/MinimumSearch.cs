using System;
using System.Collections.Generic;
using System.Text;
using Lab1.Models;
using Lab1.Tools;

namespace Lab1
{
    public static class MinimumSearch
    {
        public static double BinarySearch(CountableFunc args)
        {
            //TODO: fix
            double delta = 0.8 * args.Epsilon / 2;

            while (args.Right - args.Left >= args.Epsilon)
            {
                double x1 = (args.Left + args.Right) / 2 - delta;
                double x2 = (args.Left + args.Right) / 2 + delta;

                double f1 = args.Function(x1);
                double f2 = args.Function(x2);

                if (f1 <= f2)
                {
                    args.Right = x2;
                }

                if (f1 >= f2)
                {
                    args.Left = x1;
                }

                args.SaveData();
            }

            return (args.Left + args.Right) / 2;
        }

        public static double GoldenRatio(CountableFunc args)
        {
            double x1 = args.Left + (args.Right - args.Left) * (2 - Constants.PHI);
            double x2 = args.Left + (args.Right - args.Left) * (Constants.PHI - 1);
            double f1 = args.Function(x1);
            double f2 = args.Function(x2);

            while (args.Right - args.Left >= args.Epsilon)
            {
                if (f1 > f2)
                {
                    args.Left = x1;
                    x1 = x2;
                    f1 = f2;
                    x2 = args.Left + (args.Right - args.Left) * (Constants.PHI - 1);
                    f2 = args.Function(x2);
                }
                else
                {
                    args.Right = x2;
                    x2 = x1;
                    f2 = f1;
                    x1 = args.Left + (args.Right - args.Left) * (2 - Constants.PHI);
                    f1 = args.Function(x1);
                }

                args.SaveData();
            }

            return (x1 + x2) / 2;
        }

        public static double FibonacciMethod(CountableFunc args)
        {
            FiboGenerator fib = new FiboGenerator(args.Left, args.Right, args.Epsilon);

            double x1 = args.Left + fib[fib.Last - 2] * (args.Right - args.Left) / fib[fib.Last];
            double x2 = args.Left + fib[fib.Last - 1] * (args.Right - args.Left) / fib[fib.Last];
            double f1 = args.Function(x1);
            double f2 = args.Function(x2);

            int k = fib.Last - 2;
            while (k > 1)
            {
                if (f1 > f2)
                {
                    args.Left = x1;
                    x1 = x2;
                    x2 = args.Left + fib[k - 1] * (args.Right - args.Left) / fib[k];
                    f1 = f2;
                    f2 = args.Function(x2);
                }
                else
                {
                    args.Right = x2;
                    x2 = x1;
                    x1 = args.Left + fib[k - 2] * (args.Right - args.Left) / fib[k];
                    f2 = f1;
                    f1 = args.Function(x1);
                }

                args.SaveData();
                k--;
            }

            x2 = x1 + args.Epsilon;

            return (f1 > args.Function(x2) ? x1 + args.Right : args.Left + x2) / 2;
        }

        public static double DirectSearch(CountableFunc args)
        {
            double point = args.Left;
            double delta = args.Epsilon;
            double value = args.Function(point);

            if(value > args.Function(point + delta))
            {
                delta = args.Epsilon;
            }
            else if (value > args.Function(point - delta))
            {
                delta = args.Epsilon * -1;
            }
            else
            {
                return point;
            }

            point += delta;
            double nextValue = args.Function(point);

            while (value > nextValue)
            {
                delta *= 2;
                point += delta;
                value = nextValue;
                nextValue = args.Function(point);
            }

            if (delta > 0)
            {
                args.Left = point - 1.5 * delta;
            }
            else
            {
                args.Right = point - 1.5 * delta;
            }
            return FibonacciMethod(args);
        }
    }
}
