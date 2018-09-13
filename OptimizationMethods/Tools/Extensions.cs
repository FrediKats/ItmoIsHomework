﻿using System;
using System.Linq;
using Lab1.Models;

namespace Lab1.Tools
{
    public static class Extensions
    {
        public static bool EqualsValues(this Dimensions point, Dimensions direction)
        {
            //TODO: 0 / 0 must be valid
            var div = point
                .Coords
                .Zip(direction.Coords, (p, d) => p / d)
                .Select(v => double.IsNaN(v) ? v : v);
            return div.All(x => x == div.First());
        }

        //TODO: rename
        public static int GetDirection(this Dimensions point, Dimensions direction)
        {
            if (direction.Coords.ToList().Find(x => x != 0) == point.Coords.ToList().Find(x => x != 0))
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public static double Sqrt(this Dimensions point)
        {
            return Math.Sqrt(point.Coords.Select(x => x * x).Sum());
        }

        //TODO: rename
        public static bool CheckEpsilon(this Dimensions point, Dimensions prevPoint, Dimensions parameterEpsilons)
        {
            return point.Coords.Zip(prevPoint.Coords, (po, pr) => Math.Abs(po - pr))
                .Zip(parameterEpsilons.Coords, (diff, eps) => diff < eps)
                .All(i => i);
        }

        public static Dimensions NewGradientPoint(this Dimensions point, Dimensions direction, double lambda)
        {
            return new Dimensions(point.Coords.Zip(direction.Coords, (x, y) => x - y * lambda));
        }
    }
}