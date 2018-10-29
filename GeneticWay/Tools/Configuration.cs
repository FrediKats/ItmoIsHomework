using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticWay
{
    public class Configuration
    {
        public static double MaxF { get; }
        public static double TimePeriod { get; } = (int) 1e-3;
        public int MaxIterationCount { get; } = (int)1e8;
        public int SimulationCount { get; } = 128;
    }
}
