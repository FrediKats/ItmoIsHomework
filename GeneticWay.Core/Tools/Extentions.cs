using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.Models;
using GeneticWay.Core.Services;

namespace GeneticWay.Core.Tools
{
    public static class Extentions
    {
        public static IEnumerable<SimulationPolygon> OptimalOrder(this IEnumerable<SimulationPolygon> reports)
        {
            return reports.OrderBy(p => p.SimReport.FinishStatus)
                .ThenBy(p => p.SimReport.Distance)
                //.ThenBy(p => p.SimReport.FinalSpeed)
                .ThenBy(p => p.SimReport.IterationCount)
                .ToList();
        }

        public static string ToString(this FinishStatus s)
        {
            switch (s)
            {
                case FinishStatus.Done:
                    return "+";
                case FinishStatus.IterationLimit:
                    return "L";
                case FinishStatus.OutOfRange:
                    return "R";
                case FinishStatus.InZone:
                    return "Z";
                default:
                    throw new ArgumentOutOfRangeException(nameof(s), s, null);
            }
        }

    }
}