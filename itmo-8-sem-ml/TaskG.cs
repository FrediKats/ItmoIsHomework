using System.Collections.Generic;
using System.Linq;

using static System.Console;
using static System.Math;

namespace itmo_8_sem_ml
{
    public class TaskG
    {
        public static void Run()
        {
            var M = int.Parse(ReadLine());
            List<double[]> layer = new List<double[]>();

            for (int i = 0; i < 1 << M; ++i)
            {
                var f = int.Parse(ReadLine());

                if (f == 1)
                {
                    layer.Add(new double[M + 1]);
                    layer.Last()[M] = 0.5 - M;

                    for (int j = 0; j < M; ++j)
                    {
                        if ((i & (1 << j)) != 0)
                            layer.Last()[j] = 1.0;
                        else
                        {
                            var tmp = layer.Last();
                            tmp[j] = -1.0;
                            tmp[M] += 1.0;
                        }
                    }
                }
            }

            if (layer.Count == 0)
            {
                WriteLine("1\n1");
                Write(string.Join(" ", Enumerable.Repeat(0.0, M)));
                Write(" -0.5");
                return;
            }

            if (layer.Count == 1)
            {
                WriteLine("1\n1");
                Write(string.Join(" ", layer[0]));
                return;
            }

            WriteLine($"2\n{layer.Count} 1");
            WriteLine(string.Join("\n", layer.Select(l => string.Join(" ", l))));

            Write(string.Join(" ", Enumerable.Repeat(1.0, layer.Count)));
            Write(" -0.5");
        }
    }
}