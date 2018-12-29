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
        private static double Time = Configuration.TimePeriod;

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
                if (double.IsNaN(acceleration.X) || acceleration == (0, 0))
                {
                     acceleration = (0, 0);
                }

                //TODO: rewrite
                if (acceleration.GetLength() <= Configuration.MaxForce)
                {
                    Coordinate newVelocity = movableObject.Velocity + acceleration * Configuration.TimePeriod;
                    double maxSpeed = PhysicsFormula.GetSpeedSpeedLimit(peek.LengthTo((1, 1)));

                    if (newVelocity.GetLength() > maxSpeed)
                    {
                        if (stackOrder.Count > 100000)
                        {
                            throw new Exception("Can't find route");
                        }

                        Coordinate accelerationLength = PhysicsFormula.OptimalAcceleration(directionPath,
                            movableObject.Velocity);
                        acceleration = (accelerationLength / accelerationLength.GetLength() * Configuration.MaxForce);
                        //acceleration = movableObject.Velocity *
                        //               (-1 / movableObject.Velocity.GetLength() * Configuration.MaxForce);

                        if (double.IsNaN(acceleration.X) || acceleration == (0, 0))
                        {
                            acceleration = (0, 0);
                        }
                        movableObject.MoveAfterApplyingForce(acceleration);
                        stackOrder.Pop();
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

                    Coordinate midPoint = movableObject.Position.MidPointWith(peek);
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