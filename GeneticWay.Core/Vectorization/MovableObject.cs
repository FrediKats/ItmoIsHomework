using System.Collections.Generic;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.Vectorization
{
    public class MovableObject
    {
        public List<Coordinate> VisitedPoints { get; }
        public MovableObject(Coordinate position, Coordinate directionVector)
        {
            Position = position;
            DirectionVector = directionVector;
            VisitedPoints = new List<Coordinate>();
        }

        //TODO: Time scale
        public void Move(double time)
        {
            VisitedPoints.Add(Position);
            Position += (DirectionVector * time);
        }

        public void ApplyForceVector(Coordinate forceVector, double time)
        {
            DirectionVector += (forceVector * time);
        }

        public Coordinate Position { get; private set; }
        public Coordinate DirectionVector { get; private set; }
    }
}