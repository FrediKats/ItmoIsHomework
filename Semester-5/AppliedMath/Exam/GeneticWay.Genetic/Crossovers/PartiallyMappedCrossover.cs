using System.Collections.Generic;
using System.Linq;
using GeneticWay.Genetic.Models;
using GeneticWay.Genetic.Tools.Randomization;

namespace GeneticWay.Genetic.Crossovers
{
    public class PartiallyMappedCrossover : ICrossover
    {
        public PartiallyMappedCrossover()
        {
            ParentsNumber = 2;
            ChildrenNumber = 2;
        }

        public int ParentsNumber { get; }
        public int ChildrenNumber { get; }

        public IList<BaseGenotype<T>> Cross<T>(IList<BaseGenotype<T>> parents)
        {
            BaseGenotype<T> leftParent = parents[0];
            BaseGenotype<T> rightParent = parents[1];

            (int firstCut, int secondCut) = RandomizationProvider.Current.GetCoupleOfInt(1, leftParent.Length);

            Gene<T>[] leftParentMappingSection = leftParent.Genes.Skip(firstCut).Take((secondCut - firstCut) + 1).ToArray();
            Gene<T>[] rightParentMappingSection = rightParent.Genes.Skip(firstCut).Take((secondCut - firstCut) + 1).ToArray();

            BaseGenotype<T> offspring1 = leftParent.CreateNew();
            BaseGenotype<T> offspring2 = rightParent.CreateNew();

            offspring2.ReplaceGenes(firstCut, leftParentMappingSection);
            offspring1.ReplaceGenes(firstCut, rightParentMappingSection);

            int length = leftParent.Length;

            for (int i = 0; i < length; i++)
            {
                if (i >= firstCut && i <= secondCut)
                {
                    continue;
                }

                Gene<T> geneForOffspring1 = CheckForMapping(leftParent.Genes[i], rightParentMappingSection, leftParentMappingSection);
                offspring1.ReplaceGene(i, geneForOffspring1);

                Gene<T> geneForOffspring2 = CheckForMapping(rightParent.Genes[i], leftParentMappingSection, rightParentMappingSection);
                offspring2.ReplaceGene(i, geneForOffspring2);
            }

            return new List<BaseGenotype<T>>() { offspring1, offspring2 };
        }

        private Gene<T> CheckForMapping<T>(Gene<T> candidateGene, Gene<T>[] mappingSection, Gene<T>[] otherParentMappingSection)
        {
            var index = mappingSection
                .Select((item, i) => new { Gene = item, Index = i })
                .FirstOrDefault(g => g.Gene.Equals(candidateGene));

            if (index != null)
            {
                return CheckForMapping(otherParentMappingSection[index.Index], mappingSection, otherParentMappingSection);
            }

            return candidateGene;
        }
    }
}