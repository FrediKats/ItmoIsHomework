using System;
using System.Linq;

namespace Lab2
{
    class Program
    {
        public static void Dfs(int[,] e, int[] v, int c)
        {
            v[c] = 0;
            for (int i = 0; i < e.GetLength(1); i++)
            {
                if (v[i] == -1 && e[c, i] != 0)
                {
                    Dfs(e, v, i);
                }

                if ((v[c] == 0 || e[c, i] + v[i] < v[c]) && e[c, i] != 0)
                {
                    v[c] = e[c, i] + v[i];
                }
            }
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

            int[] v = Enumerable.Repeat(-1, 10).ToArray();
            Dfs(matrix, v, 0);
            Console.WriteLine(string.Join(' ', v));
        }
    }
}
