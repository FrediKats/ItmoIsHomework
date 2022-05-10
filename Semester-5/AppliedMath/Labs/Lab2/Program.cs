using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    class Program
    {
        public static void BreadthFirstSearch(int[,] adjacency, int[] distance, List<int> nodeList)
        {
            if (nodeList.Count == 0)
                return;

            List<int> nextNodesList = new List<int>();
            nodeList.ForEach(c => distance[c] = -1);

            foreach (int currentNode in nodeList)
            {
                for (int i = 0; i < adjacency.GetLength(0); i++)
                {
                    if (distance[i] == -2 && adjacency[currentNode, i] != 0)
                    {
                        distance[i] = -1;
                        nextNodesList.Add(i);
                    }
                }
            }

            BreadthFirstSearch(adjacency, distance, nextNodesList);

            foreach (var currentNode in nodeList)
            {
                for (int i = 0; i < distance.GetLength(0); i++)
                {
                    if ((distance[currentNode] == -1
                         || adjacency[currentNode, i] + distance[i] < distance[currentNode]) && adjacency[currentNode, i] != 0)
                    {
                        distance[currentNode] = adjacency[currentNode, i] + distance[i];
                    }
                }

                if (distance[currentNode] == -1)
                {
                    distance[currentNode] = 0;
                }
            }
        }

        public static void Dfs(int[,] e, int[] dist, int currentNode)
        {
            dist[currentNode] = 0;
            int nextNode = -1;

            for (int i = 0; i < e.GetLength(1); i++)
            {
                if (dist[i] == -1 && e[currentNode, i] != 0)
                {
                    Dfs(e, dist, i);
                }

                if ((dist[currentNode] == 0 || e[currentNode, i] + dist[i] < dist[currentNode]) && e[currentNode, i] != 0)
                {
                    dist[currentNode] = e[currentNode, i] + dist[i];
                    nextNode = i;
                }
            }

#if DEBUG
            if (nextNode != -1)
            {
                Console.WriteLine($"Optimal way from {currentNode + 1} to {nextNode + 1}");
            }
            else
            {
                Console.WriteLine($"{nextNode + 1} - end point");
            }
#endif
        }

        static void Main(string[] args)
        {
            int[,] matrix =
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

            int[] v = Enumerable.Repeat(-2, 10).ToArray();
            //Dfs(matrix, v, 0);
            BreadthFirstSearch(matrix, v, new List<int> { 0});
            Console.WriteLine(string.Join(' ', v));

        }
    }
}
