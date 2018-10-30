using System;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.Tools
{
    public static class Generator
    {
        private static readonly Random Random = new Random();
        public static ForceField GenerateRandomField()
        {
            ForceField field = new ForceField();
            for (int y = 0; y < Configuration.DegreeCount; y++)
            {
                for (int x = 0; x < Configuration.SectionCount; x++)
                {
                    field.Field[y, x] = GetRandomDirection() * Configuration.MaxForce;
                }
            }

            return field;
        }

        public static Coordinate GetRandomDirection()
        {
            Coordinate coordinate;
            do
            {
                double x = Random.NextDouble() * 2 - 1;
                double y = Random.NextDouble() * 2 - 1;
                coordinate = (y, x);
            } while (coordinate.GetLength() > 1);

            return coordinate;
        }

        public static (int degree, int section) GenerateIndex()
        {
            return (Random.Next(Configuration.DegreeCount), Random.Next(Configuration.SectionCount));
        }
    }
}