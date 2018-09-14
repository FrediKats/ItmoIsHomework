using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1.Models
{
    public class Dimensions
    {
        public double[] Coords { get; }
        public Dimensions(double[] coords)
        {
            Coords = coords;
        }

        public Dimensions(IEnumerable<double> coords)
        {
            Coords = coords.ToArray();
        }

        public Dimensions Copy()
        {
            return new Dimensions(Coords.Select(x => x));
        }

        public double Norm(Dimensions point)
        {
            return Math.Sqrt(point.Coords.Select(x => x * x).Sum());
        }

        public int Length => Coords.Length;

        public double this[int i]
        {
            get => Coords[i];
            set => Coords[i] = value;
        }
    }
}