using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace itmo_8_sem_ml
{
    public static class Program
    {
        static void Main(string[] args)
        {
        }

        public static Int32[] ReadArray()
        {
            return Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        }

        public static T2 Return<T1, T2>(this T1 input, Func<T1, T2> result) => result(input);
    }
}