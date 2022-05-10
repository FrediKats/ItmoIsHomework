using System.Collections.Generic;
using System.Linq;
using GeneticWay.Genetic.Models;

namespace GeneticWay.Genetic.Selections
{
    public class EliteSelection : ISelection
    {
        public List<BaseGenotype<T>> SelectChromosomes<T>(int number, IList<BaseGenotype<T>> generation)
        {
            var ordered = generation.OrderByDescending(c => c.Fitness);
            return ordered.Take(number).ToList();
        }
    }
}