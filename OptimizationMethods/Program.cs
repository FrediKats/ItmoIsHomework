using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MinimumSearch.BinarySearch(x => Math.Pow(x - 5, 2), -1, 6, 0.001));
            Console.WriteLine(MinimumSearch.GoldenRatio(x => Math.Pow(x - 5, 2), -1, 6, 0.001));
        }
    }
}
