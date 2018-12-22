namespace GeneticWay.Core
{
    public static class Configuration
    {
        public const double MaxForce = 4;
        public const double TimePeriod = 1e-3;

        public const int MaxIterationCount = (int) 1e7;
        public const int SimulationCount = 64;

        public const double Epsilon = 1e-4;
        public const int EpsilonInt = 4;

        public const int DegreeCount = 18;
        public const int SectionCount = 300;

        public const int CrossoverProbability = 75;
        public const int MutationProbability = 10;
    }
}