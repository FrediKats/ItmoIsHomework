using System;
using System.Collections.Generic;
using System.Linq;
using GeneticWay.Models;
using GeneticWay.Tools;

namespace GeneticWay.Logic
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

            var coordinates = new List<Coordinate>();
            var forces = new List<Coordinate>();

            var currentIteration = 0;
            Coordinate coordinate = (0, 0);
            Coordinate velocity = (0, 0);

            SimReport CreateReport(bool state)
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
                    SimReport = CreateReport(true);
                    return;
                }

                if (zones.Any(z => z.IsInZone(coordinate)))
                {
                    SimReport = CreateReport(false);
                    return;
                }

                Coordinate? force = GetForce(coordinate);
                if (force == null)
                {
                    SimReport = CreateReport(false);
                    return;
                }

                forces.Add(force.Value);
                velocity += force.Value * Configuration.MaxForce * Configuration.TimePeriod;
                coordinates.Add(coordinate.WithEpsilon(Configuration.EpsilonInt));
            }

            SimReport = CreateReport(false);
        }

        private Coordinate? GetForce(Coordinate coordinate)
        {
            if (IsOutOfField(coordinate))
            {
                return null;
            }

            Coordinate dist = (1, 1) - coordinate;
            var degree = (int) (Math.Round(Math.Atan(dist.Y / dist.X)) / 3.14 * Configuration.DegreeCount);
            var len = (int) (dist.LengthTo((1, 1)) * (Configuration.SectionCount / 1.5));

            return ForceField[degree, len];
        }

        private static bool IsOutOfField(Coordinate coordinate)
        {
            return coordinate.X < 0 || coordinate.X > 1 || coordinate.Y < 0 || coordinate.Y > 1;
        }
    }
}