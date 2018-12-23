using System.Collections.Generic;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.Vectorization
{
    public class MovableObject
    {
        private readonly double _time;

        private MovableObject(Coordinate position, Coordinate velocity, double time)
        {
            Position = position;
            Velocity = velocity;
            VisitedPoints = new List<Coordinate>();
            ForceVector = new List<Coordinate>();
            _time = time;
        }

        public List<Coordinate> VisitedPoints { get; }
        public List<Coordinate> ForceVector { get; }

        public Coordinate Position { get; private set; }
        public Coordinate Velocity { get; private set; }

        public static MovableObject Create()
        {
            return new MovableObject((0, 0), (0, 0), Configuration.TimePeriod);
        }

        public void MoveAfterApplyingForce(Coordinate forceVector)
        {
            ForceVector.Add(forceVector);
            Velocity += forceVector * _time;
            VisitedPoints.Add(Position);
            Position += Velocity * _time;
        }
    }
}