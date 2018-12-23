using System;
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
            var stackOrder = new Stack<Coordinate>();
            stackOrder.Push(to);
            while (stackOrder.Count > 0)
            {
                Coordinate peek = stackOrder.Peek();
                Coordinate directionPath = peek - movableObject.Position;
                Coordinate acceleration = MathComputing.ChooseOptimalAcceleration(directionPath, movableObject.Velocity, _time);

                if (acceleration.GetLength() <= Configuration.MaxForce)
                {
                    movableObject.MoveAfterApplyingForce(acceleration);
                    stackOrder.Pop();
                }
                else
                {
                    if (stackOrder.Count > 100000)
                    {
                        throw new Exception("Can't find route");
                    }

                    Coordinate midPoint = movableObject.Position.MidPointWith(to);
                    stackOrder.Push(midPoint);
                }
            }
        }

        public void ApplyVectorization(List<Coordinate> pathCoordinates)
        {
            foreach (Coordinate coordinate in pathCoordinates)
            {
                PointToPointVectorSelection(coordinate);
            }

            while (MovableObject.Position.LengthTo((1, 1)) > Configuration.Epsilon * 50)
            {
                PointToPointVectorSelection((1, 1));
            }
        }
    }
}