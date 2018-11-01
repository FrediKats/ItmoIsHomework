using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.Models;
using GeneticWay.Core.Tools;

namespace GeneticWay.Core.Services
{
    public static class Mutation
    {
        public static List<ForceField> CreateMutation(IEnumerable<ForceField> simulationList)
        {
            //TODO rewrite mutation
            IEnumerable<ForceField> selected = simulationList
                .Take(Configuration.SimulationCount / Configuration.CopyCount);

            var result = new ConcurrentBag<ForceField>();
            foreach (ForceField field in selected)
            {
                result.Add(field.Clone());

                for (var i = 0; i < Configuration.CopyCount - 1; i++)
                {
                    result.Add(CreteMutation(field));
                }
            }

            return result.ToList().Shuffle();
        }

        private static ForceField CreteMutation(ForceField forceField)
        {
            ForceField newField = forceField.Clone();
            (int degree, int section) = Generator.GenerateIndex();
            newField.Field[degree, section] = Generator.GetRandomDirection();
            return newField;
        }
    }
}