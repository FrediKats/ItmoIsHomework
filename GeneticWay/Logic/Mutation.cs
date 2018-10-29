using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using GeneticWay.Models;
using GeneticWay.Tools;

namespace GeneticWay.Logic
{
    public static class Mutation
    {
        public static List<SimulationPolygon> CreateMutation(IEnumerable<SimulationPolygon> simulationList)
        {
            var selected = simulationList.Take(Configuration.SimulationCount / Configuration.CopyCount);

            var result = new List<SimulationPolygon>();
            foreach (SimulationPolygon polygon in selected)
            {
                result.Add(new SimulationPolygon(polygon.ForceField.Clone()));
                for (int i = 0; i < Configuration.CopyCount; i++)
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
            newField.Field[degree, section] = Generator.GetRandomDirection();
            return new SimulationPolygon(newField); ;
        }
    }
}