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
        public static List<SimulationPolygon> CreateMutation(List<SimulationPolygon> simulationList)
        {
            var selected = simulationList.Take(Configuration.SimulationCount / 16);

            var result = new List<SimulationPolygon>();
            foreach (SimulationPolygon polygon in selected)
            {
                result.Add(new SimulationPolygon(polygon.ForceField.Clone()));
                for (int i = 0; i < 3; i++)
                {
                    result.Add(CreteMutation(polygon));
                }   
            }

            return result;
        }

        public static SimulationPolygon CreteMutation(SimulationPolygon polygon)
        {
            var newField = polygon.ForceField.Clone();
            var index = Generator.GenerateIndex();
            newField.Field[index.y][index.y] = Generator.GetRandomDirection();
            return new SimulationPolygon(newField);
        }
    }
}