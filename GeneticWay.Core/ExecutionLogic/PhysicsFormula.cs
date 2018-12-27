using System;
using GeneticWay.Core.Models;
using GeneticWay.Core.Tools;

namespace GeneticWay.Core.ExecutionLogic
{
    public static class PhysicsFormula
    {
        public static double ChooseOptimalAcceleration(double length, double velocity, double time)
        {
            //TODO: validate epsilon
            if (Math.Abs(length) < Configuration.Epsilon / 10000)
            {
                return 0;
            }

            //TODO: check formula
            return 2 * (length - velocity * time) / (time * time);
        }

        public static Coordinate ChooseOptimalAcceleration(Coordinate length, Coordinate velocity, double time)
        {
            return new Coordinate(ChooseOptimalAcceleration(length.X, velocity.X, time),
                ChooseOptimalAcceleration(length.Y, velocity.Y, time));
        }

        public static double GetSpeedSpeedLimit(double pathLength, double maxAcceleration)
        {
            return Math.Sqrt(2 * pathLength * maxAcceleration);
        }

        public static double OptimalAcceleration(double pathLength, double currentVelocity, double time)
        {
            //TODO: check formula
            return (Math.Sqrt(2 * pathLength * Configuration.MaxForce) * -1 - currentVelocity) / time;
        }
    }
}