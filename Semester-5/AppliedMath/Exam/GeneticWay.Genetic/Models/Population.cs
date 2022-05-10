using System;
using System.Collections.Generic;
using GeneticWay.Genetic.Tools;

namespace GeneticWay.Genetic.Models
{
    public class Population<T>
    {
        public Population(int minSize, int maxSize, BaseGenotype<T> adamChromosome, int maxGenerationCount = 4)
        {
            if (minSize < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(minSize),
                    "The minimum size for a population is 2 chromosomes.");
            }

            if (maxSize < minSize)
            {
                throw new ArgumentOutOfRangeException(nameof(maxSize),
                    "The maximum size for a population should be equal or greater than minimum size.");
            }

            ExceptionTools.ThrowIfNull(nameof(adamChromosome), adamChromosome);

            MinSize = minSize;
            MaxSize = maxSize;
            AdamChromosome = adamChromosome;
            Generations = new List<Generation<T>>();
            MaxGenerationCount = maxGenerationCount;
        }

        public IList<Generation<T>> Generations { get; protected set; }
        public Generation<T> CurrentGeneration { get; protected set; }
        public int GenerationsNumber { get; protected set; }

        public int MinSize { get; }
        public int MaxSize { get; }
        public int MaxGenerationCount { get; }

        public BaseGenotype<T> BestChromosome { get; protected set; }
        protected BaseGenotype<T> AdamChromosome { get; set; }

        public event EventHandler<BaseGenotype<T>> BestChromosomeChanged;

        public virtual void CreateNewGeneration(List<BaseGenotype<T>> chromosomes)
        {
            ExceptionTools.ThrowIfNull("chromosomes", chromosomes);

            GenerationsNumber++;
            CurrentGeneration = new Generation<T>(GenerationsNumber, chromosomes);
            Generations.Add(CurrentGeneration);

            if (Generations.Count > MaxGenerationCount)
            {
                Generations.RemoveAt(0);
            }
        }

        public virtual void CreateInitialGeneration()
        {
            GenerationsNumber = 0;
            Generations = new List<Generation<T>>();
            var chromosomes = new List<BaseGenotype<T>>();

            for (var i = 0; i < MinSize; i++)
            {
                BaseGenotype<T> c = AdamChromosome.Clone();
                chromosomes.Add(c);
            }

            CreateNewGeneration(chromosomes);
        }

        public virtual void EndCurrentGeneration()
        {
            CurrentGeneration.End(MaxSize);

            if (BestChromosome != CurrentGeneration.BestChromosome)
            {
                BestChromosome = CurrentGeneration.BestChromosome;
                BestChromosomeChanged?.Invoke(this, BestChromosome);
            }
        }
    }
}