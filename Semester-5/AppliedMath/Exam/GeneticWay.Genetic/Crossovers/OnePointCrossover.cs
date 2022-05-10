using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Genetic.Models;
using GeneticWay.Genetic.Tools.Randomization;

namespace GeneticWay.Genetic.Crossovers
{
    public class OnePointCrossover : ICrossover
    {
        public OnePointCrossover()
        {
            ParentsNumber = 2;
            ChildrenNumber = 2;
        }
        public int ParentsNumber { get; }
        public int ChildrenNumber { get; }
        public IList<BaseGenotype<T>> Cross<T>(IList<BaseGenotype<T>> parents)
        {
            var firstChild = CreateChild(parents[0], parents[1]);
            var secondChild = CreateChild(parents[1], parents[0]);

            return new List<BaseGenotype<T>>() { firstChild, secondChild };
        }

        protected virtual BaseGenotype<T> CreateChild<T>(BaseGenotype<T> leftParent, BaseGenotype<T> rightParent)
        {
            int swapPoint = RandomizationProvider.Current.GetInt(leftParent.Length);

            BaseGenotype<T> child = leftParent.CreateNew();
            child.ReplaceGenes(0, leftParent.Genes.Take(swapPoint).ToArray());
            child.ReplaceGenes(swapPoint, rightParent.Genes.Skip(swapPoint).ToArray());

            return child;
        }
    }
}