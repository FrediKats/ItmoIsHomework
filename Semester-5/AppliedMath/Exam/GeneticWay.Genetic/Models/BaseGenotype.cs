using System;

namespace GeneticWay.Genetic.Models
{
    public abstract class BaseGenotype<T>
    {
        protected BaseGenotype(int length)
        {
            Length = length;
            Genes = new Gene<T>[length];
        }

        public double? Fitness { get; set; }
        public int Length { get; }
        public Gene<T>[] Genes { get; }

        public abstract Gene<T> GenerateGene(int geneIndex);
        public abstract BaseGenotype<T> CreateNew();

        public virtual BaseGenotype<T> Clone()
        {
            BaseGenotype<T> clone = CreateNew();
            clone.ReplaceGenes(0, Genes);
            clone.Fitness = Fitness;

            return clone;
        }

        public void ReplaceGene(int index, Gene<T> gene)
        {
            if (index < 0 || index >= Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index),
                    $"There is no Gene on index {index} to be replaced.");
            }

            Genes[index] = gene;
            Fitness = null;
        }

        public void ReplaceGenes(int startIndex, Gene<T>[] genes)
        {
            //TODO:
            //ExceptionHelper.ThrowIfNull(nameof(genes), genes);

            if (genes.Length <= 0)
            {
                return;
            }

            if (startIndex < 0 || startIndex >= Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(startIndex),
                    $"There is no Gene on index {startIndex} to be replaced.");
            }

            int genesToBeReplacedLength = genes.Length;
            int availableSpaceLength = Length - startIndex;

            if (genesToBeReplacedLength > availableSpaceLength)
            {
                throw new ArgumentException(
                    nameof(genes),
                    "The number of genes to be replaced is greater than available space,"
                    + "there is {availableSpaceLength} genes between the index {startIndex} and the end of chromosome,"
                    + "but there is {genesToBeReplacedLength} genes to be replaced.");
            }

            Array.Copy(genes, 0, Genes, startIndex, genes.Length);

            Fitness = null;
        }
    }
}