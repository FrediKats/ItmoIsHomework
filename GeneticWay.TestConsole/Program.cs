using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticWay.Models;
using GeneticWay.Tools;

namespace GeneticWay.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var polygons = new List<SimulationPolygon>();
            var reports = new List<SimReport>();
            for (var i = 0; i < Configuration.SimulationCount; i++)
            {
                Console.WriteLine(i);
                polygons.Add(
                    new SimulationPolygon(Generator.GenerateRandomField(Configuration.BlockCount, Configuration.MaxForce)));
                reports.Add(polygons[i].Start());
            }

            var ordered = reports.OrderByDescending(r => r.IsFinish)
                .ThenBy(r => r.Distance)
                .ThenBy(r => r.FinalSpeed)
                .ThenBy(r => r.IterationCount);

            foreach (SimReport report in ordered)
            {
                Console.WriteLine(report);
            }
        }
    }
}
