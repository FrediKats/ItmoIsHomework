using System.Collections.Generic;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.Vectorization
{
    public class MovableObject
    {
        public List<Coordinate> VisitedPoints { get; }
        public MovableObject(Coordinate position, Coordinate velocity)
        {
            Position = position;
            Velocity = velocity;
            VisitedPoints = new List<Coordinate>();
        }

        public static MovableObject Create()
        {
            return new MovableObject((0, 0), (0, 0));
        }

        //TODO: Time scale
        public void Move(double time)
        {
            VisitedPoints.Add(Position);
            Position += (Velocity * time);
        }

        public void ApplyForceVector(Coordinate forceVector, double time)
        {
            Velocity += (forceVector * time);
        }

        public Coordinate Position { get; private set; }
        public Coordinate Velocity { get; private set; }
    }
}