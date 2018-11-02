using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.Services
{
    public static class SimulationPolygon
    {
        public static SimReport Start(ForceField forceField, List<Zone> zones)
        {
            var coordinates = new List<Coordinate>();
            var forces = new List<Coordinate>();

            var currentIteration = 0;
            Coordinate coordinate = (0, 0);
            Coordinate velocity = (0, 0);

            SimReport CreateReport(FinishStatus state)
            {
                return new SimReport(state, coordinate.LengthTo((1, 1)), velocity.GetLength(), currentIteration,
                    coordinates, forces, forceField);
            }

            while (currentIteration < Configuration.MaxIterationCount)
            {
                currentIteration++;
                coordinate += velocity * Configuration.TimePeriod;
                if (coordinate == (1, 1))
                {
                    return CreateReport(FinishStatus.Done);
                }

                if (zones.Any(z => z.IsInZone(coordinate)))
                {
                    return CreateReport(FinishStatus.InZone);
                }

                Coordinate? force = GetForce(coordinate, forceField);
                if (force == null)
                {
                    return CreateReport(FinishStatus.OutOfRange);
                }

                forces.Add(force.Value);
                velocity += force.Value * Configuration.MaxForce * Configuration.TimePeriod;
                coordinates.Add(coordinate.WithEpsilon(Configuration.EpsilonInt));
            }

            return CreateReport(FinishStatus.IterationLimit);
        }

        private static Coordinate? GetForce(Coordinate coordinate, ForceField field)
        {
            if (IsOutOfField(coordinate))
            {
                return null;
            }

            Coordinate dist = (1, 1) - coordinate;
            var degree = (int) (Math.Round(Math.Atan(dist.Y / dist.X)) / 3.14 * Configuration.DegreeCount);
            var len = (int) (dist.LengthTo((1, 1)) * (Configuration.SectionCount / 1.5));

            return field.Field[degree, len];
        }

        private static bool IsOutOfField(Coordinate coordinate)
        {
            return coordinate.X < 0 || coordinate.X > 1 || coordinate.Y < 0 || coordinate.Y > 1;
        }
    }
}