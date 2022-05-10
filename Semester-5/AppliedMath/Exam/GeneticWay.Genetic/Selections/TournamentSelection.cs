using System.Collections.Generic;
using System.Linq;
using GeneticWay.Genetic.Models;
using GeneticWay.Genetic.Tools.Randomization;

namespace GeneticWay.Genetic.Selections
{
    public class TournamentSelection : ISelection
    {
        public TournamentSelection(bool isWinnerContinue, int genotypeInRound)
        {
            IsWinnerContinue = isWinnerContinue;
            GenotypeInRound = genotypeInRound;
        }

        public bool IsWinnerContinue { get; }
        public int GenotypeInRound { get; }

        public List<BaseGenotype<T>> SelectChromosomes<T>(int number, IList<BaseGenotype<T>> generation)
        {
            List<BaseGenotype<T>> genotypesPool = generation.ToList();
            var selectedList = new List<BaseGenotype<T>>(number);

            for (int iteration = 0; iteration < number; iteration++)
            {
                int[] randomIndexes = RandomizationProvider.Current.GetUniqueInts(GenotypeInRound, 0, genotypesPool.Count);
                BaseGenotype<T> tournamentWinner = genotypesPool
                    .Where((c, i) => randomIndexes.Contains(i))
                    .OrderByDescending(c => c.Fitness)
                    .First();

                selectedList.Add(tournamentWinner);

                if (!IsWinnerContinue)
                {
                    genotypesPool.Remove(tournamentWinner);
                }
            }

            return selectedList;
        }
    }
}