using System;

namespace GeneticWay.Genetic.Models
{
    public class BinaryGenotype : BaseGenotype<int>
    {
        public BinaryGenotype(int length) : base(length)
        {
        }

        public override Gene<int> GenerateGene(int geneIndex)
        {
            //TODO: add random
            throw new NotImplementedException();
        }

        public override BaseGenotype<int> CreateNew()
        {
            return new BinaryGenotype(Length);
        }
    }
}