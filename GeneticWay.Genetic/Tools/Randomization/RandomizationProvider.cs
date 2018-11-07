namespace GeneticWay.Genetic.Tools.Randomization
{
    public static class RandomizationProvider
    {
        static RandomizationProvider()
        {
            Current = new DefaultRandomization();
        }

        public static IRandomization Current { get; set; }
    }
}