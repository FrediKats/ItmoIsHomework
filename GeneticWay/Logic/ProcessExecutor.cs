using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeneticWay.Models;
using GeneticWay.Tools;
using Newtonsoft.Json;

namespace GeneticWay.Logic
{
    public static class ProcessExecutor
    {
        private static string Path = "backup.json";
        public static void Execute()
        {
            var polygons = TryLoad(Path);
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
                return;
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

        public static List<SimulationPolygon> TryLoad(string path)
        {
            try
            {
                List<SimulationPolygon> res = new List<SimulationPolygon>();
                for (int i = 0; i < Configuration.SimulationCount; i++)
                {
                    var sim = JsonConvert.DeserializeObject<SimReport>(File.ReadAllText(path));
                    res.Add(new SimulationPolygon(sim.Field));
                }

                return res;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }


}