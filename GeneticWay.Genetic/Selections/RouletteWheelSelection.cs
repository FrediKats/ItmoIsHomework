using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Genetic.Models;
using GeneticWay.Genetic.Tools.Randomization;

namespace GeneticWay.Genetic.Selections
{
    public class RouletteWheelSelection : ISelection
    {
        public RouletteWheelSelection()
        {
        }

        public List<BaseGenotype<T>> SelectChromosomes<T>(int number, IList<BaseGenotype<T>> generation)
        {
            IList<BaseGenotype<T>> chromosomes = generation;
            IList<double> rouletteWheel = GenerateWheel(chromosomes);
            return SelectFromWheel(number, chromosomes, rouletteWheel);
        }

        private List<double> GenerateWheel<T>(IList<BaseGenotype<T>> chromosomes)
        {
            var rouletteWheel = new List<double>();

            double sumFitness = chromosomes.Sum(c => c.Fitness.Value);
            var cumulativePercent = 0.0;

            foreach (BaseGenotype<T> c in chromosomes)
            {
                cumulativePercent += c.Fitness.Value / sumFitness;
                rouletteWheel.Add(cumulativePercent);
            }

            return rouletteWheel;
        }

        private List<BaseGenotype<T>> SelectFromWheel<T>(int number, IList<BaseGenotype<T>> chromosomes,
            IList<double> rouletteWheel)
        {
            var selected = new List<BaseGenotype<T>>();

            for (int i = 0; i < number; i++)
            {
                double pointer = RandomizationProvider.Current.GetDouble();

                int chromosomeIndex = rouletteWheel
                    .Select((value, index) => new { Value = value, Index = index })
                    .First(r => r.Value >= pointer)
                    .Index;
                selected.Add(chromosomes[chromosomeIndex]);
            }

            return selected;
        }
    }
}