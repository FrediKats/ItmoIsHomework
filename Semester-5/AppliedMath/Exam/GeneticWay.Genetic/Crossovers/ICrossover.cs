using System.Collections.Generic;
using GeneticWay.Genetic.Models;

namespace GeneticWay.Genetic.Crossovers
{
    public interface ICrossover
    {
        int ParentsNumber { get; }
        int ChildrenNumber { get; }
        IList<BaseGenotype<T>> Cross<T>(IList<BaseGenotype<T>> parents);
    }
}