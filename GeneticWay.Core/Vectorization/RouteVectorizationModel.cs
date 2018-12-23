using System;
using System.Collections.Generic;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.Vectorization
{
    public class RouteVectorizationModel
    {
        public MovableObject MovableObject { get; }
        private const double Time = Configuration.TimePeriod;

        //TODO: add params
        public RouteVectorizationModel(MovableObject movableObject)
        {
            MovableObject = movableObject;
        }

        public void PointToPointVectorSelection(Coordinate to)
        {
            RecursiveDivision(to);
        }

        private void RecursiveDivision(Coordinate to)
        {
            var stackOrder = new Stack<Coordinate>();
            stackOrder.Push(to);
            while (stackOrder.Count > 0)
            {
                Coordinate peek = stackOrder.Peek();
                Coordinate directionPath = peek - MovableObject.Position;
                Coordinate acceleration = MathComputing.ChooseOptimalAcceleration(directionPath, MovableObject.Velocity, Time);

                if (acceleration.GetLength() <= Configuration.MaxForce)
                {
                    MovableObject.MoveAfterApplyingForce(acceleration);
                    stackOrder.Pop();
                }
                else
                {
                    if (stackOrder.Count > 100000)
                    {
                        //if (to != (1, 1))
                        //    return;
                        throw new Exception("Can't find route");
                    }

                    Coordinate midPoint = MovableObject.Position.MidPointWith(to);
                    stackOrder.Push(midPoint);
                }
            }
        }

        private void VelocityMinimization()
        {
            var rebuiltCoordinate = new Stack<Coordinate>();
            (Coordinate position, Coordinate force) data = MovableObject.Rollback();


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