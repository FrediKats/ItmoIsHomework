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
                List<SimReport> reports = null;
                for (int i = 0; i < 100; i++)
                {
                    polygons = MakeIteration(polygons);
                    reports = polygons.Select(p => p.SimReport).ToList();
                    polygons = Mutation.CreateMutation(polygons);
                }

                Console.WriteLine(reports.First());
                //foreach (SimReport report in reports)
                //{
                //    Console.WriteLine(report);
                //}

                //Console.SetCursorPosition(0, 0);
                //Console.ReadKey();
                //Console.Clear();
            }
        }

        public static List<SimulationPolygon> MakeIteration(List<SimulationPolygon> polygons)
        {
            polygons.AsParallel().ForAll(p => p.Start());

            polygons = polygons.OrderByDescending(p => p.SimReport.IsFinish)
                .ThenBy(p => p.SimReport.Points)
                .ThenBy(p => p.SimReport.IterationCount)
                .ToList();
            return polygons;
        }
    }
}
