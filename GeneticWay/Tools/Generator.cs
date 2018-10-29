using System;
using GeneticWay.Models;

namespace GeneticWay.Tools
{
    public static class Generator
    {
        private static readonly Random Random = new Random();
        public static ForceField GenerateRandomField(int blockCount, double maxForce)
        {
            ForceField field = new ForceField(blockCount);
            for (int y = 0; y < blockCount; y++)
            {
                for (int x = 0; x < blockCount; x++)
                {
                    field.Field[y][x] = GetRandomDirection() * maxForce;
                }
            }

            return field;
        }

        public static Coordinate GetRandomDirection()
        {
            Coordinate coordinate;
            do
            {
                coordinate = (Random.NextDouble(), Random.NextDouble());
            } while (coordinate.GetLength() > 1);

            return coordinate;
        }
    }
}