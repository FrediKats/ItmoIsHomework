using System;

namespace AppliedMath.LoadBalancer.Tools
{
    public static class Config
    {
        public static readonly Random Random = new Random();

        public const int MaxTaskExecutionTime = 3000;

        public const int BalancerHandlersCount = 4;

        public const int InputStreamMinDelay = 100;
        public const int InputStreamMaxDelay = 500;

    }
}