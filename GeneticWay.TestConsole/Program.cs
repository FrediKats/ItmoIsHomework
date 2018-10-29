using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticWay.Logic;
using GeneticWay.Models;
using GeneticWay.Tools;

namespace GeneticWay.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var polygons = new List<SimulationPolygon>();
            for (var i = 0; i < Configuration.SimulationCount; i++)
            {
                polygons.Add(
                    new SimulationPolygon(Generator.GenerateRandomField()));
            }

            while (true)
            {
                for (int i = 0; i < 100; i++)
                {
                    polygons = Mutation.CreateMutation(polygons);
                    polygons = MakeIteration(polygons);
                }

                Console.WriteLine(polygons.Select(p => p.SimReport).First());
            }
        }

        public static List<SimulationPolygon> MakeIteration(List<SimulationPolygon> polygons)
        {
            polygons.AsParallel().ForAll(p => p.Start());

            polygons = polygons.OrderByDescending(p => p.SimReport.IsFinish)
                .ThenBy(p => p.SimReport.Distance)
                .ThenBy(p => p.SimReport.FinalSpeed)
                .ThenBy(p => p.SimReport.IterationCount)
                .ToList();
            return polygons;
        }
    }
}
