using System;
using GeneticWay.Genetic.Tools;

namespace GeneticWay.Genetic.Models
{
    public class Chromosome<T>
    {
        private readonly Func<Gene<T>> _generation;

        public Chromosome(int length, Func<Gene<T>> generation)
        {
            ValidateLength(length);

            Length = length;
            _generation = generation;

            Genes = new Gene<T>[length];
            for (var i = 0; i < Length; i++)
            {
                Genes[i] = generation();
            }
        }

        public Chromosome(Gene<T>[] genes)
        {
            Length = genes.Length;
            ValidateLength(genes.Length);

            //TODO: check references
            Genes = genes;
        }

        public Gene<T>[] Genes { get; }

        public int Length { get; }

        public double? Fitness { get; set; }

        public Gene<T> GetGene(int index)
        {
            return Genes[index];
        }

        public Chromosome<T> Clone()
        {
            var clone = new Chromosome<T>(Length, _generation);
            clone.ReplaceGenes(0, Genes);
            clone.Fitness = Fitness;

            return clone;
        }

        public void ReplaceGene(int index, Gene<T> gene)
        {
            if (index < 0 || index >= Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index),
                    $"There is no Gene on index {index} to be replaced.");
            }

            Genes[index] = gene;
            Fitness = null;
        }

        public void ReplaceGenes(int startIndex, Gene<T>[] genes)
        {
            ExceptionTools.ThrowIfNull(nameof(genes), genes);

            if (genes.Length <= 0)
            {
                return;
            }

            if (startIndex < 0 || startIndex >= Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex),
                    $"There is no Gene on index {startIndex} to be replaced.");
            }

            int genesToBeReplacedLength = genes.Length;
            int availableSpaceLength = Length - startIndex;

            if (genesToBeReplacedLength > availableSpaceLength)
            {
                throw new ArgumentException(
                    nameof(genes),
                    $"The number of genes to be replaced is greater than available space, there is {availableSpaceLength}"
                    + $"genes between the index {startIndex} and the end of chromosome,"
                    + $"but there is {genesToBeReplacedLength} genes to be replaced.");
            }

            Array.Copy(genes, 0, Genes, startIndex, genes.Length);

            Fitness = null;
        }

        private static void ValidateLength(int length)
        {
            if (length < 2)
            {
                throw new ArgumentException("The minimum length for a chromosome is 2 genes.", nameof(length));
            }
        }
    }
}