using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.Models;
using GeneticWay.Core.Tools;

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
            VelocityVectors = new List<Coordinate>();
            _time = time;
        }

        public List<Coordinate> VisitedPoints { get; }
        public List<Coordinate> ForceVector { get; }
        public List<Coordinate> VelocityVectors { get; }

        public Coordinate Position { get; private set; }
        public Coordinate Velocity { get; private set; }

        public static MovableObject Create()
        {
            return new MovableObject((0, 0), (0, 0), Configuration.TimePeriod);
        }

        public void MoveAfterApplyingForce(Coordinate forceVector)
        {
            ForceVector.Add(forceVector);
            VelocityVectors.Add(Velocity);
            VisitedPoints.Add(Position);

            //Velocity += forceVector * _time;
            //Position += Velocity * _time;

            //TODO: fix with correct formula
            Position += Velocity * _time + forceVector * _time * _time * 0.5;
            Velocity += forceVector * _time;
        }

        //TODO: remove
        public (Coordinate position, Coordinate force) Rollback()
        {
            Coordinate position = VisitedPoints.Last();
            Coordinate force = ForceVector.Last();

            VisitedPoints.RemoveAt(VisitedPoints.Count - 1);
            Position -= Velocity * _time;
            Velocity -= force * _time;
            ForceVector.RemoveAt(ForceVector.Count - 1);
            return (position, force);
        }

        public void SaveState()
        {
            VelocityVectors.Add(Velocity);
            VisitedPoints.Add(Position);
        }
    }
}