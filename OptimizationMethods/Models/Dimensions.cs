using System.Collections.Generic;
using System.Linq;

namespace Lab1.Models
{
    public class Dimensions
    {
        public double[] Coords { get; set; }
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

        public double this[int i]
        {
            get => Coords[i];
            set => Coords[i] = value;
        }
    }
}