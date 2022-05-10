using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticWay.Genetic.Models
{
    public class Generation<T>
    {
        public Generation(int number, List<BaseGenotype<T>> chromosomes)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(number),
                    $"Generation number {number} is invalid. Generation number should be positive and start in 1.");
            }

            if (chromosomes == null || chromosomes.Count < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(chromosomes),
                    "A generation should have at least 2 chromosomes.");
            }
        }

        public int Number { get; private set; }
        public List<BaseGenotype<T>> Chromosomes { get; internal set; }
        public BaseGenotype<T> BestChromosome { get; private set; }

        public void End(int chromosomesNumber)
        {
            Chromosomes = Chromosomes
                .OrderByDescending(c => c.Fitness.Value)
                .ToList();

            if (Chromosomes.Count > chromosomesNumber)
            {
                Chromosomes = Chromosomes.Take(chromosomesNumber).ToList();
            }

            BestChromosome = Chromosomes.First();
        }
    }
}