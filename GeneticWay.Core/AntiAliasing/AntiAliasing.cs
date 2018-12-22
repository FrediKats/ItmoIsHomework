using System;
using System.Collections.Generic;
using System.Text;
using GeneticWay.Core.Models;
using GeneticWay.Core.Vectorization;

namespace GeneticWay.Core.AntiAliasing
{
    public class AntiAliasing
    {
        public AntiAliasing(List<Coordinate> forcesList)
        {
            ForcesList = forcesList;
        }

        public MovableObject GenerateRoute()
        {
            MovableObject movableObject = MovableObject.Create();
            foreach (Coordinate force in ForcesList)
            {
                movableObject.ApplyForceVector(force);
                movableObject.Move();
            }

            return movableObject;
        }

        public List<Coordinate> ForcesList { get; }
    }
}
