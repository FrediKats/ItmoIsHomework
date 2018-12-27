using System;
using System.Collections.Generic;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Models;
using GeneticWay.Core.Tools;

namespace GeneticWay.Core.Vectorization
{
    public class RouteVectorizationModel
    {
        private const double Time = Configuration.TimePeriod;

        //TODO: add params
        public RouteVectorizationModel(MovableObject movableObject)
        {
            MovableObject = movableObject;
        }

        public MovableObject MovableObject { get; }

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
                Coordinate acceleration =
                    PhysicsFormula.ChooseOptimalAcceleration(directionPath, MovableObject.Velocity, Time);

                //TODO: rewrite
                if (acceleration.GetLength() <= Configuration.MaxForce)
                {
                    Coordinate newVelocity = MovableObject.Velocity + acceleration * Configuration.TimePeriod;
                    double maxSpeed = PhysicsFormula.GetSpeedSpeedLimit(MovableObject.Position.LengthTo((1, 1)),
                        Configuration.MaxForce);

                    if (newVelocity.GetLength() > maxSpeed)
                    {
                        double accelerationLength = PhysicsFormula.OptimalAcceleration(directionPath.GetLength(),
                            MovableObject.Velocity.GetLength(), Configuration.TimePeriod);
                        acceleration *= accelerationLength / acceleration.GetLength();
                        if (stackOrder.Count > 100000)
                        {
                            throw new Exception("Can't find route");
                        }

                        MovableObject.MoveAfterApplyingForce(acceleration);
                    }
                    else
                    {
                        MovableObject.MoveAfterApplyingForce(acceleration);
                        stackOrder.Pop();
                    }
                }
                else
                {
                    if (stackOrder.Count > 100000)
                    {
                        throw new Exception("Can't find route");
                    }

                    Coordinate midPoint = MovableObject.Position.MidPointWith(to);
                    stackOrder.Push(midPoint);
                }
            }
        }

        public void ApplyVectorization(List<Coordinate> pathCoordinates)
        {
            foreach (Coordinate coordinate in pathCoordinates)
            {
                if (MovableObject.Position.LengthTo(coordinate) < Configuration.Epsilon / 10)
                {
                    continue;
                }

                PointToPointVectorSelection(coordinate);
            }

            while (MovableObject.Position.LengthTo((1, 1)) > Configuration.Epsilon / 10)
            {
                PointToPointVectorSelection((1, 1));
            }

            MovableObject.SaveState();
        }
    }
}