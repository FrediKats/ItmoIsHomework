using System;
using System.Linq;
using Lab1.Models;

namespace Lab1.Tools
{
    public static class Extensions
    {
        public static bool CheckEpsilon(this Dimensions point, Dimensions prevPoint, Dimensions parameterEpsilons)
        {
            return point.Coords.Zip(prevPoint.Coords, (po, pr) => Math.Abs(po - pr))
                .Zip(parameterEpsilons.Coords, (diff, eps) => diff < eps)
                .All(i => i);
        }
    }
}