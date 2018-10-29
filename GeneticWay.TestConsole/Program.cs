using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticWay.Logic;
using GeneticWay.Models;
using GeneticWay.Tools;
using Newtonsoft.Json;

namespace GeneticWay.TestConsole
{
    class Program
    {
        private static string Path = "backup.json";
        static void Main(string[] args)
        {
            var polygons = TryLoad();
            if (polygons == null)
            {
                polygons = new List<SimulationPolygon>();
                for (var i = 0; i < Configuration.SimulationCount; i++)
                {
                    polygons.Add(
                        new SimulationPolygon(Generator.GenerateRandomField()));
                }
            }
            

            SimReport last = null;
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                var json = JsonConvert.SerializeObject(last);
                File.WriteAllText(Path, json);
            };

            while (true)
            {
                for (int i = 0; i < 100; i++)
                {
                    polygons = Mutation.CreateMutation(polygons);
                    polygons = MakeIteration(polygons);
                }

                last = polygons.Select(p => p.SimReport).First();
                Console.WriteLine(last);
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

        public static List<SimulationPolygon> TryLoad()
        {
            try
            {
                List<SimulationPolygon> res = new List<SimulationPolygon>();
                for (int i = 0; i < Configuration.SimulationCount; i++)
                {
                    var sim = JsonConvert.DeserializeObject<SimReport>(File.ReadAllText(Path));
                    res.Add(new SimulationPolygon(sim.Field));
                }

                return res;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
