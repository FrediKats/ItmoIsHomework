using System;
using GeneticWay.Tools;

namespace GeneticWay.Models
{
    public class ForceField
    {
        private readonly Coordinate[,] _field;

        public ForceField()
        {
            _field = new Coordinate[Configuration.DegreeCount, Configuration.SectionCount];
        }

        public ForceField(Coordinate[,] field)
        {
            _field = field;
        }

        public Coordinate this[int y, int x]
        {
            get => _field[y, x];
            set => _field[y, x] = value;
        }

        public ForceField Clone()
        {
            var storage = new Coordinate[Configuration.DegreeCount, Configuration.SectionCount];
            Array.Copy(_field, storage, _field.Length);
            return new ForceField(storage);
        }
    }
}