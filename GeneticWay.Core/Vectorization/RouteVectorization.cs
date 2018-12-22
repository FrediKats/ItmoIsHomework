using System;
using System.Collections.Generic;
using GeneticWay.Core.ExecutionLogic;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.Vectorization
{
    public class RouteVectorization
    {
        //TODO: add params
        public RouteVectorization(List<Coordinate> pathCoordinates, MovableObject movableObject)
        {
            _pathCoordinates = pathCoordinates;
            _movableObject = movableObject;
        }

        private readonly List<Coordinate> _pathCoordinates;
        private readonly MovableObject _movableObject;
        private double _time = Configuration.TimePeriod;

        private void PointToPointVectorSelection(Coordinate to, MovableObject movableObject)
        {
            RecursiveDivision(to, movableObject);

            //Coordinate directionPath = to - movableObject.Position;
            //Coordinate acceleration =
            //    MathComputing.ChooseOptimalAcceleration(directionPath, movableObject.Velocity, _time);

            //if (acceleration.GetLength() > Configuration.MaxForce * 1.5)
            //{
                

            //    directionPath = to - movableObject.Position;
            //    acceleration = MathComputing.ChooseOptimalAcceleration(directionPath, movableObject.Velocity, _time);
            //    movableObject.ApplyForceVector(acceleration, _time);
            //    movableObject.Move(_time);
            //}
            //else
            //{
            //    movableObject.ApplyForceVector(acceleration, _time);
            //    movableObject.Move(_time);
            //}


            //TODO: fix
            //if (acceleration.GetLength() > Configuration.MaxForce)
            //    throw new Exception("Point too far, setup other epsilon");

            
        }

        private void RecursiveDivision(Coordinate to, MovableObject movableObject)
        {
            while (true)
            {
                Coordinate directionPath = to - movableObject.Position;
                Coordinate acceleration = MathComputing.ChooseOptimalAcceleration(directionPath, movableObject.Velocity, _time);

                if (acceleration.GetLength() <= Configuration.MaxForce)
                {
                    movableObject.ApplyForceVector(acceleration, _time);
                    movableObject.Move(_time);
                }
                else
                {
                    acceleration = acceleration * (1 / acceleration.GetLength());
                    movableObject.ApplyForceVector(acceleration, _time);
                    movableObject.Move(_time);

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
