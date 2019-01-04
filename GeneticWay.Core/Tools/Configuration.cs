namespace GeneticWay.Core.Tools
{
    public static class Configuration
    {
        //TODO: update configuration
        public static void Setup(double maxForce, double time)
        {
            MaxForce = maxForce;
            TimePeriod = time;
        }

        static Configuration()
        {
            MaxForce = 1;
            TimePeriod = 0.01;
            Epsilon = 1e-3;
        }

        public static double MaxForce { get; private set; }
        public static double TimePeriod { get; private set; }
        public static double Epsilon { get; private set; }

        //TODO:deprecated
        public const int MaxIterationCount = (int) 1e7;
        public const int SimulationCount = 64;
        public const int EpsilonInt = 4;

        public const int DegreeCount = 18;
        public const int SectionCount = 300;

        public const int CrossoverProbability = 75;
        public const int MutationProbability = 10;
    }
}