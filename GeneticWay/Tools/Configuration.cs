using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticWay
{
    public static class Configuration
    {
        public static float MaxForce { get; } = 1;
        public static float TimePeriod { get; } = (float)1e-3;
        public static int MaxIterationCount { get; } = (int)1e5;
        public static int SimulationCount { get; } = 128;
        public static int BlockCount { get; } = 256;
    }
}
