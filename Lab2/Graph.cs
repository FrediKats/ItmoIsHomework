using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Lab2
{
    public class Graph
    {
        private readonly double[,] _adjacencyMatrix;

        public int Order => _adjacencyMatrix.GetLength(0);

        public Graph(double[,] adjacencyMatrix)
        {
            if (adjacencyMatrix != null &&
                adjacencyMatrix.Rank != 2 &&
                adjacencyMatrix.GetLength(0) != adjacencyMatrix.GetLength(1))
            {
                throw new ArgumentException("Non-correct matrix", nameof(adjacencyMatrix));
            }

            _adjacencyMatrix = adjacencyMatrix;
        }

        public double ShortestPath(int start, int finish)
        {
            List<double> nodes = Enumerable.Range(0, 10).Select(x => double.PositiveInfinity).ToList();
            nodes[start] = 0;

            while (double.IsInfinity(nodes[start]))
            {
                

                //currentNodes.ForEach(Console.WriteLine);
                break;
            }

            return 0;
        }
    }
}
