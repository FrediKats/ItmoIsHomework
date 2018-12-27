using System;
using System.Collections.Generic;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Models;
using GeneticWay.Core.Tools;
using GeneticWay.Core.Vectorization;

namespace GeneticWay.Core.RoutingLogic
{
    //TODO: make static
    public static class RouteVectorization
    {
        private const double Time = Configuration.TimePeriod;

        private static void PointToPointVectorSelection(MovableObject movableObject, Coordinate to)
        {
            RecursiveDivision(movableObject, to);
        }

        private static void RecursiveDivision(MovableObject movableObject, Coordinate to)
        {
            var stackOrder = new Stack<Coordinate>();
            stackOrder.Push(to);

            while (stackOrder.Count > 0)
            {
                Coordinate peek = stackOrder.Peek();
                Coordinate directionPath = peek - movableObject.Position;
                Coordinate acceleration =
                    PhysicsFormula.ChooseOptimalAcceleration(directionPath, movableObject.Velocity, Time);

                //TODO: rewrite
                if (acceleration.GetLength() <= Configuration.MaxForce)
                {
                    Coordinate newVelocity = movableObject.Velocity + acceleration * Configuration.TimePeriod;
                    double maxSpeed = PhysicsFormula.GetSpeedSpeedLimit(movableObject.Position.LengthTo((1, 1)),
                        Configuration.MaxForce);

                    if (newVelocity.GetLength() > maxSpeed)
                    {
                        if (stackOrder.Count > 100000)
                        {
                            throw new Exception("Can't find route");
                        }

                        double accelerationLength = PhysicsFormula.OptimalAcceleration(directionPath.GetLength(),
                            movableObject.Velocity.GetLength(), Configuration.TimePeriod);
                        acceleration *= accelerationLength / acceleration.GetLength() / 2;
                        movableObject.MoveAfterApplyingForce(acceleration);
                    }
                    else
                    {
                        movableObject.MoveAfterApplyingForce(acceleration);
                        stackOrder.Pop();
                    }
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

        public static MovableObject ApplyVectorization(List<Coordinate> pathCoordinates)
        {
            MovableObject movableObject = MovableObject.Create();

            foreach (Coordinate coordinate in pathCoordinates)
            {
                if (movableObject.Position.LengthTo(coordinate) < Configuration.Epsilon / 10)
                {
                    continue;
                }

                PointToPointVectorSelection(movableObject, coordinate);
            }

            while (movableObject.Position.LengthTo((1, 1)) > Configuration.Epsilon / 10)
            {
                PointToPointVectorSelection(movableObject, (1, 1));
            }

            movableObject.SaveState();
            return movableObject;
        }
    }
}