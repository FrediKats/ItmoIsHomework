using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticWay
{
    public static class Configuration
    {
        public static double MaxForce { get; } = 1;
        public static double TimePeriod { get; } = 1e-1;
        public static int MaxIterationCount { get; } = (int)1e5;
        public static int SimulationCount { get; } = 128;
        public static int BlockCount { get; } = 4;
    }
}
