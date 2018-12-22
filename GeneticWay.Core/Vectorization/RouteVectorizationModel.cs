using System.Collections.Generic;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.Vectorization
{
    public class RouteVectorizationModel
    {
        public MovableObject MovableObject { get; }

        private const double _time = Configuration.TimePeriod;

        //TODO: add params
        public RouteVectorizationModel(MovableObject movableObject)
        {
            MovableObject = movableObject;
        }

        public void PointToPointVectorSelection(Coordinate to)
        {
            RecursiveDivision(to, MovableObject);
        }

        private static void RecursiveDivision(Coordinate to, MovableObject movableObject)
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

        public void ApplyVectorization(List<Coordinate> pathCoordinates)
        {
            foreach (Coordinate coordinate in pathCoordinates)
            {
                PointToPointVectorSelection(coordinate);
            }
        }
    }
}