using System.Collections.Generic;
using GeneticWay.Genetic.Models;

namespace GeneticWay.Genetic.Selections
{
    public interface ISelection
    {
        List<BaseGenotype<T>> SelectChromosomes<T>(int number, IList<BaseGenotype<T>> generation);
    }
}