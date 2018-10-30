using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeneticWay.Core.Models;
using GeneticWay.Core.Tools;
using Newtonsoft.Json;

namespace GeneticWay.Core.Services
{
    public class SimulationManager
    {
        private const string Path = "backup.json";
        private List<SimulationPolygon> _polygons;
        private SimReport PeekReport => _polygons.Select(p => p.SimReport).First();
        public SimulationManager()
        {
            _polygons = TryLoad();
        }

        public void MakeIteration(int iterationCount)
        {
            for (var i = 0; i < iterationCount; i++)
            {
                _polygons = Mutation.CreateMutation(_polygons);
                _polygons.AsParallel().ForAll(p => p.Start());

                _polygons = _polygons
                    .OptimalOrder()
                    .ToList();
            }

            SaveData();
            Console.WriteLine(PeekReport);
        }

        public void SaveData()
        {
            string json = JsonConvert.SerializeObject(PeekReport);
            File.WriteAllText(Path, json);
        }

        private static List<SimulationPolygon> TryLoad()
        {
            try
            {
                var res = new List<SimulationPolygon>();
                for (var i = 0; i < Configuration.SimulationCount; i++)
                {
                    var sim = JsonConvert.DeserializeObject<SimReport>(File.ReadAllText(Path));
                    res.Add(new SimulationPolygon(sim.Field));
                }

                return res;
            }
            catch (Exception)
            {
                var res = new List<SimulationPolygon>();
                for (var i = 0; i < Configuration.SimulationCount; i++)
                {
                    res.Add(new SimulationPolygon(Generator.GenerateRandomField()));
                }

                return res;
            }
        }
    }
}