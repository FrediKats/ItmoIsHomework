using System;
using System.Collections.Generic;
using System.Linq;

namespace itmo_8_sem_ml
{
    class Program
    {
        public class Context
        {
            public static int Beta = 1;

            public Int32[] C { get; set; }
            public Int32[] P { get; set; }
            public Int32[] T { get; set; }

            public Int32[] TP { get; set; }
            public Int32[] FP { get; set; }
            public Int32[] FN { get; set; }
            public Int32[] TN { get; set; }

            public Context(List<Int32[]> dataSet)
            {
                C = new Int32[dataSet.Count];
                P = new Int32[dataSet.Count];
                T = new Int32[dataSet.Count];
                TP = new Int32[dataSet.Count];
                FP = new Int32[dataSet.Count];
                FN = new Int32[dataSet.Count];
                TN = new Int32[dataSet.Count];
                
                for (int i = 0; i < dataSet.Count; i++)
                {
                    for (int j = 0; j < dataSet.Count; j++)
                    {
                        var value = dataSet[j][i];

                        C[j] += value;
                        P[i] += value;

                        if (i == j)
                        {
                            T[i] += value;
                            TP[i] += value;
                        }
                        else
                        {
                            FP[i] += value;
                            FN[j] += value;
                        }
                    }
                }

                for (int i = 0; i < TP.Length; i++) TN[i] = C[i] - TP[i] - FP[i] - FN[i];
            }

            public int All => C.Sum();

            public double Prec(int i) => ValueOrZero((double)TP[i] / (TP[i] + FP[i]));
            public double Recall(int i) => ValueOrZero((double)TP[i] / (TP[i] + FN[i]));

            public double PrecW => T.Select((_, i) => ValueOrZero((double)(T[i] * C[i]) / P[i])).Sum() / All;
            public double RecallW => (double)T.Sum() / All;
            public double FScore(int i) => ValueOrZero(2.0 * (Prec(i) * Recall(i)) / (Prec(i) + Recall(i)));
            public double MicroF() => C.Select((t, i) => t * FScore(i) / All).Sum();

            public double MacroF() => (1 + Beta) * (PrecW * RecallW) / (Beta * Beta * PrecW + RecallW);

            private double ValueOrZero(double value) => double.IsNaN(value) ? 0 : value;
        }

        static void Main(string[] args)
        {
            
        }

        public static void TaskB()
        {
            var count = int.Parse(Console.ReadLine());

            List<Int32[]> dataSet = new List<Int32[]>(count);
            for (int i = 0; i < count; i++)
            {
                dataSet.Add(Console.ReadLine().Split(' ').Select(int.Parse).ToArray());
            }

            var context = new Context(dataSet);

            Console.WriteLine(context.MacroF());
            Console.WriteLine(context.MicroF());
        }


        public static void TaskA()
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse);
            Console.ReadLine()
                .Split(' ')
                .Select((s, i) => (Value: int.Parse(s), Index: i + 1))
                .OrderBy(t => t.Value)
                .Select((t, newIndex) => (Prev: t.Index, New: newIndex))
                .GroupBy(t => t.New % input.ElementAt(2), t => t.Prev)
                .Select(g => $"{g.Count()} {string.Join(" ", g)}")
                .ToList()
                .ForEach(Console.WriteLine);

        }
    }
}