using System.Collections.Generic;
using GeneticWay.Genetic.Models;
using GeneticWay.Genetic.Tools.Randomization;

namespace GeneticWay.Genetic.Crossovers
{
    public class UniformCrossover : ICrossover
    {
        public UniformCrossover(double probability = 0.5f)
        {
            ParentsNumber = 2;
            ChildrenNumber = 2;
            MixProbability = probability;
        }
        public int ParentsNumber { get; }
        public int ChildrenNumber { get; }
        public double MixProbability { get; }

        public IList<BaseGenotype<T>> Cross<T>(IList<BaseGenotype<T>> parents)
        {
            BaseGenotype<T> firstParent = parents[0];
            BaseGenotype<T> secondParent = parents[1];
            BaseGenotype<T> firstChild = firstParent.CreateNew();
            BaseGenotype<T> secondChild = secondParent.CreateNew();

            for (int i = 0; i < firstParent.Length; i++)
            {
                if (RandomizationProvider.Current.GetDouble() < MixProbability)
                {
                    firstChild.ReplaceGene(i, firstParent.Genes[i]);
                    secondChild.ReplaceGene(i, secondParent.Genes[i]);
                }
                else
                {
                    firstChild.ReplaceGene(i, secondParent.Genes[i]);
                    secondChild.ReplaceGene(i, firstParent.Genes[i]);
                }
            }

            return new List<BaseGenotype<T>> { firstChild, secondChild };
        }
    }
}