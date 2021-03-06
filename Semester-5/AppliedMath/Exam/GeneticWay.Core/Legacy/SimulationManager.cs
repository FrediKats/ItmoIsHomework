using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeneticWay.Core.Models;
using GeneticWay.Core.Tools;
using Newtonsoft.Json;

namespace GeneticWay.Core.Legacy
{
    public class SimulationManager
    {
        private const string Path = "backup.json";

        public readonly List<Circle> Zones;
        private List<ForceField> _forceFields;
        public List<SimReport> Reports;

        public SimulationManager()
        {
            Zones = new List<Circle> {new Circle((0.5, 0.5), 0.2)};
            Load();
        }

        private void Load()
        {
            _forceFields = TryLoad();
        }

        public void MakeIteration(int iterationCount)
        {
            for (var i = 0; i < iterationCount; i++)
            {
                Reports = _forceFields
                    .AsParallel()
                    .Select(p => SimulationPolygon.Start(p, Zones))
                    .OptimalOrder()
                    .ToList();

                _forceFields = Reports
                    .Select(r => r.Field)
                    .ToList();

                _forceFields = GeneticAlgo.CreateNextGeneration(Reports
                    .Select(r => (r.Field, (int)Math.Log(1 / r.Distance, 2)))
                    .ToList());

            }

            SaveToJson();
            Console.WriteLine(Reports.First());
        }

        public void SaveToJson()
        {
            string json = JsonConvert.SerializeObject(Reports.First());
            File.WriteAllText(Path, json);
        }

        private static List<ForceField> TryLoad()
        {
            List<ForceField> res;
            try
            {
                res = new List<ForceField>();
                for (var i = 0; i < Configuration.SimulationCount; i++)
                {
                    var sim = JsonConvert.DeserializeObject<SimReport>(File.ReadAllText(Path));
                    res.Add(sim.Field);
                }
            }
            catch (Exception)
            {
                res = new List<ForceField>();
                for (var i = 0; i < Configuration.SimulationCount; i++)
                {
                    res.Add(Generator.GenerateRandomField());
                }
            }

            return res;
        }
    }
}