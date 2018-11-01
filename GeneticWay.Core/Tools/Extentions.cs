using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.Tools
{
    public static class Extentions
    {
        public static IEnumerable<SimReport> OptimalOrder(this IEnumerable<SimReport> reports)
        {
            return reports.OrderBy(p => p.FinishStatus)
                .ThenBy(p => p.Distance)
                .ThenBy(p => p.FinalSpeed)
                .ThenBy(p => p.IterationCount)
                .ToList();
        }

        public static string ToChar(this FinishStatus s)
        {
            switch (s)
            {
                case FinishStatus.Done:
                    return "+";
                case FinishStatus.IterationLimit:
                    return "L";
                case FinishStatus.OutOfRange:
                    return "R";
                //case FinishStatus.InZone:
                //    return "Z";
                default:
                    throw new ArgumentOutOfRangeException(nameof(s), s, null);
            }
        }
    }
}