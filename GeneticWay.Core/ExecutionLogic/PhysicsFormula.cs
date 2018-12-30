using System;
using GeneticWay.Core.Models;
using GeneticWay.Core.Tools;

namespace GeneticWay.Core.ExecutionLogic
{
    public static class PhysicsFormula
    {
        /// <summary>
        /// length = velocity * time + acceleration * time^2 /2
        /// <para></para>
        /// acceleration = 2 * (length - velocity * time) / time^2
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// length = velocity * time + acceleration * time^2 /2
        /// <para></para>
        /// acceleration = 2 * (length - velocity * time) / time^2
        /// </summary>
        /// <returns></returns>
        public static Coordinate ChooseOptimalAcceleration(Coordinate length, Coordinate velocity, double time)
        {
            return new Coordinate(ChooseOptimalAcceleration(length.X, velocity.X, time),
                ChooseOptimalAcceleration(length.Y, velocity.Y, time));
        }

        public static Coordinate GetVelocitySpeedLimit(Coordinate path)
        {
            double Convert(double l) => Math.Sqrt(2 * Math.Abs(l) * Configuration.MaxForce);
            return new Coordinate(Convert(path.X), Convert(path.Y));
        }

        public static bool CheckForVelocityLimit(Coordinate path, Coordinate velocity)
        {
            Coordinate velocityLimit = GetVelocitySpeedLimit(path);

            if (velocityLimit.X < Math.Abs(velocity.X))
                return false;
            if (velocityLimit.Y < Math.Abs(velocity.Y))
                return false;

            return true;
        }

        /// <summary>
        /// length = velocity * time + acceleration * time^2 /2
        /// <para></para>
        /// acceleration = 2 * (length - velocity * time) / time^2
        /// </summary>
        /// <returns></returns>
        public static double OptimalAccelerationWithSpeedLimit(double pathLength, double currentVelocity, double time)
        {
            //TODO: check formula
            double acceleration =
                (Math.Sqrt(2 * Math.Abs(pathLength) * Configuration.MaxForce) - currentVelocity) / time;
            return pathLength >= 0 ? acceleration : acceleration * -1;
        }


        public static Coordinate OptimalAccelerationWithSpeedLimit(Coordinate path, Coordinate velocity)
        {
            //TODO: check formula
            return new Coordinate(OptimalAccelerationWithSpeedLimit(path.X, velocity.X, Configuration.TimePeriod),
                OptimalAccelerationWithSpeedLimit(path.Y, velocity.Y, Configuration.TimePeriod));
        }

        public static Coordinate AfterMovementPosition(Coordinate startPosition, Coordinate velocity, Coordinate acceleration)
        {
            double time = Configuration.TimePeriod;
            return startPosition + velocity * time + acceleration * time * time / 2;
        }
    }
}