﻿namespace GeneticWay.Models
{
    public class ForceField
    {
        public Coordinate[][] Field { get; set; }
        public ForceField(int size)
        {
            Field = new Coordinate[size][];
            for (int i = 0; i < size; i++)
            {
                Field[i] = new Coordinate[size];
            }
        }

        public ForceField(Coordinate[][] field)
        {
            Field = field;
        }

        public ForceField Clone()
        {
            return new ForceField((Coordinate[][])Field.Clone());
        }
    }
}