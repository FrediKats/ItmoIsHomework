using System;
using System.Linq;
using GeneticWay.Genetic.Models;
using GeneticWay.Genetic.Tools;
using GeneticWay.Genetic.Tools.Randomization;

namespace GeneticWay.Genetic.Crossovers
{
    public class TwoPointCrossover : OnePointCrossover
    {
        protected override BaseGenotype<T> CreateChild<T>(BaseGenotype<T> leftParent, BaseGenotype<T> rightParent)
        {
            (int firstSwap, int secondSwap) = RandomizationProvider.Current.GetCoupleOfInt(1, leftParent.Length);

            BaseGenotype<T> child = leftParent.CreateNew();
            child.ReplaceGenes(0, leftParent.Genes.Take(firstSwap).ToArray());
            child.ReplaceGenes(firstSwap, rightParent.Genes.Skip(firstSwap).Take(secondSwap - firstSwap).ToArray());
            child.ReplaceGenes(secondSwap, leftParent.Genes.Skip(secondSwap).ToArray());

            return child;
        }
    }
}