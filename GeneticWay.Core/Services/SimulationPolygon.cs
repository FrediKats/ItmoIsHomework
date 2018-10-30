using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Core.Models;

namespace GeneticWay.Core.Services
{
    public class SimulationPolygon
    {
        public SimulationPolygon(ForceField field)
        {
            ForceField = field;
        }

        public ForceField ForceField { get; }
        public SimReport SimReport { get; private set; }

        public void Start()
        {
            var zones = new List<Zone>();
            zones.Add(new Zone((0.5, 0.25), 0.1));
            zones.Add(new Zone((0.75, 0.5), 0.15));
            zones.Add(new Zone((0.75, 0.65), 0.05));
            zones.Add(new Zone((0.25, 0.5), 0.05));
            zones.Add(new Zone((0.8, 0.9), 0.05));
            zones.Add(new Zone((0.92, 0.92), 0.05));
            zones.Add(new Zone((0.92, 0.92), 0.05));
            zones.Add(new Zone((0.5, 0.75), 0.2));
            zones.Add(new Zone((0.9, 0.8), 0.15));


            var coordinates = new List<Coordinate>();
            var forces = new List<Coordinate>();

            var currentIteration = 0;
            Coordinate coordinate = (0, 0);
            Coordinate velocity = (0, 0);

            SimReport CreateReport(FinishStatus state)
            {
                return new SimReport(state, coordinate.LengthTo((1, 1)), velocity.GetLength(), currentIteration,
                    coordinates, forces, ForceField, zones);
            }

            while (currentIteration < Configuration.MaxIterationCount)
            {
                currentIteration++;
                coordinate += velocity * Configuration.TimePeriod;
                if (coordinate == (1, 1))
                {
                    SimReport = CreateReport(FinishStatus.Done);
                    return;
                }

                if (zones.Any(z => z.IsInZone(coordinate)))
                {
                    SimReport = CreateReport(FinishStatus.InZone);
                    return;
                }

                Coordinate? force = GetForce(coordinate);
                if (force == null)
                {
                    SimReport = CreateReport(FinishStatus.OutOfRange);
                    return;
                }

                forces.Add(force.Value);
                velocity += force.Value * Configuration.MaxForce * Configuration.TimePeriod;
                coordinates.Add(coordinate.WithEpsilon(Configuration.EpsilonInt));
            }

            SimReport = CreateReport(FinishStatus.IterationLimit);
        }

        private Coordinate? GetForce(Coordinate coordinate)
        {
            if (IsOutOfField(coordinate))
            {
                return null;
            }

            Coordinate dist = (1, 1) - coordinate;
            var degree = (int)(Math.Round(Math.Atan(dist.Y / dist.X)) / 3.14 * Configuration.DegreeCount);
            var len = (int)(dist.LengthTo((1, 1)) * (Configuration.SectionCount / 1.5));

            return ForceField.Field[degree, len];
        }

        private static bool IsOutOfField(Coordinate coordinate)
        {
            return coordinate.X < 0 || coordinate.X > 1 || coordinate.Y < 0 || coordinate.Y > 1;
        }
    }
}