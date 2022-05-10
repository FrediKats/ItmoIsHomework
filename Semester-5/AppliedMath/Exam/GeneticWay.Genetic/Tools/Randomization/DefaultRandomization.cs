using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GeneticWay.Genetic.Tools.Randomization
{
    /// <summary>
    ///     An IRandomization implementation using System.Random has pseudo-number generator.
    ///     Ave John Skit
    /// </summary>
    /// <remarks>
    ///     https://codeblog.jonskeet.uk/2009/11/04/revisiting-randomness/
    /// </remarks>
    public class DefaultRandomization : IRandomization
    {
        private static readonly Random GlobalRandom = new Random();
        private static readonly object GlobalLock = new object();
        private static readonly ThreadLocal<Random> ThreadRandom = new ThreadLocal<Random>(NewRandom);

        private static Random Instance => ThreadRandom.Value;

        public int GetInt(int min, int max)
        {
            return Instance.Next(min, max);
        }

        public int GetInt(int max)
        {
            return Instance.Next(max);
        }

        public int[] GetInts(int length, int min, int max)
        {
            var ints = new int[length];

            for (var i = 0; i < length; i++)
            {
                ints[i] = GetInt(min, max);
            }

            return ints;
        }

        public int[] GetUniqueInts(int length, int min, int max)
        {
            int diff = max - min;
            List<int> orderedValues = Enumerable.Range(min, diff).ToList();
            var ints = new int[length];

            for (var i = 0; i < length; i++)
            {
                int removeIndex = GetInt(0, orderedValues.Count);
                ints[i] = orderedValues[removeIndex];
                orderedValues.RemoveAt(removeIndex);
            }

            return ints;
        }

        public float GetFloat()
        {
            return (float) Instance.NextDouble();
        }

        public float GetFloat(float min, float max)
        {
            return min + (max - min) * GetFloat();
        }

        public double GetDouble()
        {
            return (float) Instance.NextDouble();
        }

        public double GetDouble(double min, double max)
        {
            return min + (max - min) * GetDouble();
        }

        private static Random NewRandom()
        {
            lock (GlobalLock)
            {
                return new Random(GlobalRandom.Next());
            }
        }
    }
}