using System;

namespace ServeYourself.Client.Tools
{
    public static class RandomInstance
    {
        public static readonly Random Random;

        static RandomInstance()
        {
            Random = new Random();
        }
    }
}