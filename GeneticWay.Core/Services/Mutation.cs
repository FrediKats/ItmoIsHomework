using System.Collections.Generic;
using System.Linq;
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

            var result = new List<SimulationPolygon>();
            foreach (SimulationPolygon polygon in selected)
            {
                result.Add(new SimulationPolygon(polygon.ForceField.Clone()));
                for (var i = 0; i < Configuration.CopyCount; i++)
                {
                    result.Add(CreteMutation(polygon));
                }
            }

            return result;
        }

        private static SimulationPolygon CreteMutation(SimulationPolygon polygon)
        {
            ForceField newField = polygon.ForceField.Clone();
            (int degree, int section) = Generator.GenerateIndex();
            newField[degree, section] = Generator.GetRandomDirection();
            return new SimulationPolygon(newField);
        }
    }
}