using System;
using System.Linq;

namespace GeneticWay.Models
{
    public class ForceField
    {
        public Coordinate[,] Field { get; set; }

        public ForceField(int size)
        {
            Field = new Coordinate[size, size];
        }

        public ForceField(Coordinate[,] field)
        {
            Field = field;
        }

        public ForceField Clone()
        {
            Coordinate[,] storage = new Coordinate[Field.GetLength(0), Field.GetLength(1)];
            Array.Copy(Field, storage, Field.Length);
            //return new ForceField(Field.Select(a => a.ToArray()).ToArray());
            return new ForceField(storage);
        }
    }
}