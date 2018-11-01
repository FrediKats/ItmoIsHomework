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
        private List<ForceField> _forceFields;
        public List<SimReport> Reports;
        public List<Zone> Zones;

        public SimulationManager()
        {
            Load();
        }

        private void Load()
        {
            _forceFields = TryLoad();

            Zones = new List<Zone>
            {
                new Zone((0.5, 0.25), 0.1),
                new Zone((0.75, 0.5), 0.07),
                new Zone((0.75, 0.65), 0.05),
                new Zone((0.25, 0.5), 0.05),
                new Zone((0.8, 0.9), 0.05),
                new Zone((0.92, 0.92), 0.05),
                new Zone((0.92, 0.92), 0.05),
                new Zone((0.6, 0.65), 0.15),
                new Zone((0.9, 0.8), 0.1)
            };
        }

        public void MakeIteration(int iterationCount)
        {
            for (var i = 0; i < iterationCount; i++)
            {
                _forceFields = Mutation.CreateMutation(_forceFields);

                Reports = _forceFields
                    .AsParallel()
                    .Select(p => SimulationPolygon.Start(p, Zones))
                    .OptimalOrder()
                    .ToList();

                _forceFields = Reports
                    .Select(r => r.Field)
                    .ToList();
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