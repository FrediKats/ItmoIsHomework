using System.Collections.Generic;
using System.Linq;
using GeneticWay.Logic;
using GeneticWay.Models;

namespace GeneticWay.Tools
{
    public static class Extentions
    {
        public static IEnumerable<SimulationPolygon> OptimalOrder(this IEnumerable<SimulationPolygon> reports)
        {
            return reports.OrderByDescending(p => p.SimReport.IsFinish)
                .ThenBy(p => p.SimReport.Distance)
                .ThenBy(p => p.SimReport.FinalSpeed)
                .ThenBy(p => p.SimReport.IterationCount)
                .ToList();
        }
    }
}