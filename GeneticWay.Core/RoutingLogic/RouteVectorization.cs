using System;
using System.Collections.Generic;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Models;
using GeneticWay.Core.Tools;
using GeneticWay.Core.Vectorization;

namespace GeneticWay.Core.RoutingLogic
{
    public static class RouteVectorization
    {
        private static readonly double Time = Configuration.TimePeriod;

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
                if (stackOrder.Count > 100000)
                {
                    throw new Exception("Can't find route");
                }

                Coordinate peek = stackOrder.Peek();
                Coordinate directionPath = peek - movableObject.Position;
                Coordinate acceleration =
                    PhysicsFormula.ChooseOptimalAcceleration(directionPath, movableObject.Velocity, Time);

                if (double.IsNaN(acceleration.X))
                {
                    acceleration = (0, 0);
                }

                //TODO: check situation when velocity is too big and has wrong direction
                if (acceleration.GetLength() > Configuration.MaxForce)
                {
                    PreventBigAccelerationVector(movableObject, peek, acceleration, stackOrder);
                }
                else
                {
                    Coordinate newVelocity = movableObject.Velocity + acceleration * Configuration.TimePeriod;
                    double maxSpeed = PhysicsFormula.GetSpeedSpeedLimit((1, 1) - peek);

                    if (newVelocity.GetLength() > maxSpeed)
                    {
                        Coordinate accelerationLength = PhysicsFormula.OptimalAccelerationWithSpeedLimit(directionPath,
                            movableObject.Velocity);

                        acceleration = MathComputing.ResizeVector(accelerationLength, Configuration.MaxForce);
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
            }
        }

        private static void PreventBigAccelerationVector(MovableObject movableObject, Coordinate targetPosition,
            Coordinate currentAcceleration, Stack<Coordinate> stack)
        {
            Coordinate newAcceleration = MathComputing.ResizeVector(currentAcceleration, Configuration.MaxForce);
            Coordinate nextPosition = PhysicsFormula.AfterMovementPosition(movableObject.Position,
                movableObject.Velocity, newAcceleration);

            if (movableObject.Position.LengthTo(nextPosition) < movableObject.Position.LengthTo(targetPosition))
            {
                stack.Push(movableObject.Position.MidPointWith(targetPosition));
            }
            else
            {
                stack.Pop();
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