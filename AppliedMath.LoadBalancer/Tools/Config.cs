using System;

namespace AppliedMath.LoadBalancer.Tools
{
    public static class Config
    {
        public static readonly Random Random = new Random();

        public static int MaxTaskDelay = 3000;
    }
}