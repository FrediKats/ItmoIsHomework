using System.Collections.Generic;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.Vectorization
{
    public class RouteVectorization
    {
        private readonly MovableObject _movableObject;

        private readonly List<Coordinate> _pathCoordinates;

        private const double _time = Configuration.TimePeriod;

        //TODO: add params
        public RouteVectorization(List<Coordinate> pathCoordinates, MovableObject movableObject)
        {
            _pathCoordinates = pathCoordinates;
            _movableObject = movableObject;
        }

        private void PointToPointVectorSelection(Coordinate to, MovableObject movableObject)
        {
            RecursiveDivision(to, movableObject);
        }

        private void RecursiveDivision(Coordinate to, MovableObject movableObject)
        {
            while (true)
            {
                Coordinate directionPath = to - movableObject.Position;
                Coordinate acceleration =
                    MathComputing.ChooseOptimalAcceleration(directionPath, movableObject.Velocity, _time);

                if (acceleration.GetLength() <= Configuration.MaxForce)
                {
                    movableObject.ApplyForceVector(acceleration);
                    movableObject.Move();
                }
                else
                {
                    acceleration = acceleration * (1 / acceleration.GetLength());
                    movableObject.ApplyForceVector(acceleration);
                    movableObject.Move();

                    continue;
                }

                break;
            }
        }

        public List<Coordinate> ApplyVectorization()
        {
            foreach (Coordinate coordinate in _pathCoordinates)
            {
                PointToPointVectorSelection(coordinate, _movableObject);
            }

            return _movableObject.VisitedPoints;
        }
    }
}