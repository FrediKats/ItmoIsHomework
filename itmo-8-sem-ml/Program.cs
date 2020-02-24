using System;
using System.Collections.Generic;
using System.Linq;

namespace itmo_8_sem_ml
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var c = new TaskC();
        }

        public static Int32[] ReadArray()
        {
            return Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        }

        public static T2 Return<T1, T2>(this T1 input, Func<T1, T2> result) => result(input);
    }

    public class TaskA
    {
        public static void Run()
        {
            var input = Program.ReadArray();
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

    public static class TaskB
    {
        public static void Run()
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
    }

    public class TaskC
    {
        public TaskC()
        {
            Int32[] input = Program.ReadArray();
            List<(Int32[], int)> dataSet = new List<(Int32[], int)>(input[0]);
            for (int i = 0; i < input[0]; i++)
            {
                var c = Program.ReadArray();

                dataSet.Add((c.Take(input[1]).ToArray(), c.Last()));
            }
            Int32[] target = Program.ReadArray();

            var context = new Context(
                dataSet,
                target,
                Console.ReadLine(),
                Console.ReadLine(),
                Console.ReadLine(),
                Int32.Parse(Console.ReadLine()));

            Console.WriteLine(context.Execute());
        }

        

        public class Context
        {
            public Context(List<(Int32[], int)> dataSet, Int32[] targetPoint, String distType, String coreType, String windowType, Int32 windowArgument)
            {
                DataSet = dataSet;
                TargetPoint = targetPoint;
                DistType = distType;
                CoreType = coreType;
                WindowType = windowType;
                WindowArgument = windowArgument;
            }

            public Int32[] TargetPoint { get; set; }
            public String DistType { get; set; }
            public String CoreType { get; set; }
            public String WindowType { get; set; }
            public Int32 WindowArgument { get; set; }
            public List<(Int32[], int)> DataSet { get; set; }

            public double Execute()
            {
                var distFunc = GetDistFunc(DistType);
                Func<double, double> funcK = GetFuncK(CoreType);

                IEnumerable<(Double, Int32)> dists = DataSet
                    .Select(p => (distFunc(p.Item1, TargetPoint), p.Item2));

                double window = GetWindow(dists);

                var cores = dists
                    .Select(d => (d.Item1 == 0 ? funcK(d.Item1) : window == 0.0 ? 0 : funcK(d.Item1 / window),
                        d.Item2));

                if (cores.Sum(c => c.Item1) == 0)
                    return cores.Average(c => c.Item2);

                return cores
                    .Select(c => c.Item1 * (c.Item2))
                    .Sum()
                    .Return(s => s / cores.Sum(c => c.Item1));
            }

            public Func<double, double> GetFuncK(string funcName)
            {
                switch (funcName)
                {
                    case "uniform":
                        return u => 0.5.Return(v => Math.Abs(u) >= 1.0 ? 0 : v);
                    case "triangular":
                        return u => 1 - Math.Abs(u).Return(v => Math.Abs(u) > 1.0 ? 0 : v);
                    case "epanechnikov":
                        return u => 0.75 * (1 - Math.Pow(u, 2)).Return(v => Math.Abs(u) > 1.0 ? 0 : v);
                    case "quartic":
                        return u => (15.0 / 16) * Math.Pow(1 - Math.Pow(u, 2), 2).Return(v => Math.Abs(u) > 1.0 ? 0 : v);
                    case "triweight":
                        return u => (35.0 / 32) * Math.Pow(1 - Math.Pow(u, 2), 3).Return(v => Math.Abs(u) > 1.0 ? 0 : v);
                    case "tricube":
                        return u => (70.0 / 81) * Math.Pow(1 - Math.Pow(Math.Abs(u), 3), 3).Return(v => Math.Abs(u) > 1.0 ? 0 : v);
                    case "gaussian":
                        return u => (1.0 / Math.Sqrt(2 * Math.PI)) * Math.Pow(Math.E, -1.0 / 2 * Math.Pow(u, 2));
                    case "cosine":
                        return u => Math.PI / 4 * Math.Cos(Math.PI / 2 * u).Return(v => Math.Abs(u) > 1.0 ? 0 : v);
                    case "logistic":
                        return u => 1.0 / (Math.Pow(Math.E, u) + 2 + Math.Pow(Math.E, -u));
                    case "sigmoid":
                        return u => 2.0 / Math.PI / (Math.Pow(Math.E, u) + Math.Pow(Math.E, -u));
                }

                throw new NotSupportedException();
            }

            public Func<int[], int[], double> GetDistFunc(string funcName)
            {
                switch (funcName)
                {
                    case "manhattan":
                        return (a, b) => a.Zip(b, (ae, be) => (double) ae - be).Sum(Math.Abs);
                    case "euclidean":
                        return (a, b) => a
                            .Zip(b, (ae, be) => (double) ae - be)
                            .Select(s => Math.Pow(s, 2))
                            .Sum()
                            .Return(Math.Sqrt);
                    case "chebyshev":
                        return (a, b) => a.Zip(b, (ae, be) => (double) ae - be).Max(Math.Abs);
                }

                throw new NotSupportedException();
            }

            public double GetWindow(IEnumerable<(Double, Int32)> elements)
            {
                return WindowType == "fixed"
                    ? WindowArgument
                    : elements.OrderBy(e => e.Item1).ElementAt(WindowArgument).Item1;
            }
        }
    }

}