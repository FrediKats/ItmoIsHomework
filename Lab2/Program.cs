using System;
using System.Linq;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1

            double[,] matrix =
            {
                { 0, 5, 2, 7, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 6, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 5, 7, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 4, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 2, 8, 0 },
                { 0, 0, 0, 0, 0, 0, 9, 5, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 9 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 7 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };

            var graph = new Graph(matrix);

            graph.ShortestPath(0, graph.Order - 1);
        }
    }
}
