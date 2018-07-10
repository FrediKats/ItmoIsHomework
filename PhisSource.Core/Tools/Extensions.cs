using System;
using PhisSource.Core.Models;

namespace PhisSource.Core.Tools
{
    public static class Extensions
    {
        public static double DistanceTo(this TwoDimensional f, TwoDimensional s)
        {
            return Math.Sqrt(Math.Pow(f.X - s.X, 2) + Math.Pow(f.Y - s.Y, 2));
        }
    }
}