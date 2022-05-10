using System.Collections.Generic;
using GeneticWay.Genetic.Models;

namespace GeneticWay.Genetic.Crossovers
{
    public class CycleCrossover : ICrossover
    {
        public CycleCrossover()
        {
            ParentsNumber = 2;
            ChildrenNumber = 2;
        }
        public int ParentsNumber { get; }
        public int ChildrenNumber { get; }
        public IList<BaseGenotype<T>> Cross<T>(IList<BaseGenotype<T>> parents)
        {
            throw new System.NotImplementedException();
        }
    }
}