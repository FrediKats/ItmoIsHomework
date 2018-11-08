using System;

namespace GeneticWay.Genetic.Tools.Randomization
{
    public static class RandomizationExtensions
    {
        public static (int first, int last) GetCoupleOfInt(this IRandomization r, int min, int max)
        {
            int[] result = RandomizationProvider.Current.GetUniqueInts(2, min, max);
            Array.Sort(result);
            return (result[0], result[1]);
        }
    }
}