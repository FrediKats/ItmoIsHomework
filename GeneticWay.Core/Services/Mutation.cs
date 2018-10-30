using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneticWay.Core.Models;
using GeneticWay.Core.Tools;

namespace GeneticWay.Core.Services
{
    public static class Mutation
    {
        public static List<SimulationPolygon> CreateMutation(IEnumerable<SimulationPolygon> simulationList)
        {
            IEnumerable<SimulationPolygon> selected =
                simulationList.Take(Configuration.SimulationCount / Configuration.CopyCount);

            var result = new ConcurrentBag<SimulationPolygon>();
            foreach (SimulationPolygon polygon in selected)
            {
                result.Add(new SimulationPolygon(polygon.ForceField.Clone()));

                Enumerable.Range(0, Configuration.CopyCount - 1)
                    .AsParallel()
                    .ForAll(i => result.Add(CreteMutation(polygon)));
            }

            return result.ToList().Shuffle();
        }

        private static SimulationPolygon CreteMutation(SimulationPolygon polygon)
        {
            ForceField newField = polygon.ForceField.Clone();
            (int degree, int section) = Generator.GenerateIndex();
            newField.Field[degree, section] = Generator.GetRandomDirection();
            return new SimulationPolygon(newField);
        }
    }
}