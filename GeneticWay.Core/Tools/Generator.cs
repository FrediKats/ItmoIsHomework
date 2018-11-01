using System;
using System.Collections.Generic;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.Tools
{
    public static class Generator
    {
        private static readonly Random Random = new Random();

        public static ForceField GenerateRandomField()
        {
            var field = new ForceField();
            for (var y = 0; y < Configuration.DegreeCount; y++)
            {
                for (var x = 0; x < Configuration.SectionCount; x++)
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

        public static List<T> Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        public static (int degree, int section) GenerateIndex()
        {
            return (Random.Next(Configuration.DegreeCount), Random.Next(Configuration.SectionCount));
        }
    }
}