using System;
using GeneticWay.Models;

namespace GeneticWay.Tools
{
    public static class Generator
    {
        private static readonly Random Random = new Random();
        public static ForceField GenerateRandomField()
        {
            ForceField field = new ForceField(Configuration.BlockCount);
            for (int y = 0; y < Configuration.BlockCount; y++)
            {
                for (int x = 0; x < Configuration.BlockCount; x++)
                {
                    field.Field[y][x] = GetRandomDirection() * Configuration.MaxForce;
                }
            }

            return field;
        }

        public static Coordinate GetRandomDirection()
        {
            Coordinate coordinate;
            float x = (float) Random.NextDouble() * 2 - 1;
            float y = (float) Math.Sqrt(1 - Math.Pow(x, 2));
            return (x, y);
        }

        public static (int x, int y) GenerateIndex()
        {
            return (Random.Next(Configuration.BlockCount), Random.Next(Configuration.BlockCount));
        }
    }
}