using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.Models;
using GeneticWay.Core.Tools;

namespace GeneticWay.Core.Services
{
    //TODO: write static extension for List<ForceField>
    public static class GeneticAlgo
    {
        public static List<ForceField> CreateNextGeneration(List<(ForceField gen, int fit)> population)
        {
            List<ForceField> selected = RouletteWheelSelection(population);
            List<ForceField> afterCrossover = PopulationCrossover(selected);
            List<ForceField> afterMutation = PopulationMutation(afterCrossover);

            return afterMutation
                .Shuffle()
                .ToList();
        }

        

        public static List<ForceField> RouletteWheelSelection(List<(ForceField gen, int fit)> population)
        {
            List<ForceField> mattingPool = new List<ForceField>();
            foreach ((ForceField gen, int fit) chromosome in population)
            {
                for (int i = 0; i < chromosome.fit; i++)
                {
                    mattingPool.Add(chromosome.gen.Clone());
                }
            }

            return mattingPool
                .Shuffle()
                .Take(population.Count)
                .ToList();
        }

        public static List<ForceField> PopulationCrossover(List<ForceField> population)
        {
            var shuffledPopulation = population.Shuffle();
            var resultSet = new List<ForceField>(shuffledPopulation.Count);

            for (int i = 0; i + 1 < shuffledPopulation.Count; i += 2)
            {
                if (Generator.CheckProbability(Configuration.CrossoverProbability))
                {
                    resultSet.Add(shuffledPopulation[i]);
                    resultSet.Add(shuffledPopulation[i + 1]);
                }
                else
                {
                    int locus = Generator.Random.Next(shuffledPopulation[i].Field.Length);
                    resultSet.Add (PairCrossover(shuffledPopulation[i], shuffledPopulation[i + 1], locus));
                    resultSet.Add (PairCrossover(shuffledPopulation[i + 1], shuffledPopulation[i], locus));
                }
            }

            return resultSet;
        }

        private static ForceField PairCrossover(ForceField firstGenotype, ForceField secondGenotype, int locus)
        {
            Coordinate[] result = firstGenotype.Field
                .Cast<Coordinate>()
                .Take(locus)
                .Concat(secondGenotype.Field
                    .Cast<Coordinate>()
                    .Skip(locus))
                .ToArray();

            return new ForceField(result.ToMatrix(Configuration.DegreeCount, Configuration.SectionCount));
        }

        private static List<ForceField> PopulationMutation(List<ForceField> population)
        {
            return population
                .Select(c => Generator.CheckProbability(Configuration.MutationProbability)
                        ? ChromosomeMutation(c)
                        : c)
                .ToList();
        }

        private static ForceField ChromosomeMutation(ForceField forceField)
        {
            ForceField newField = forceField.Clone();
            (int degree, int section) = Generator.GenerateIndex();
            newField.Field[degree, section] = Generator.GetRandomDirection();
            return newField;
        }
    }
}