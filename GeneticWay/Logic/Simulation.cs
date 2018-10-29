using System;
using System.Collections.Generic;
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
        public SimReport SimReport { get; set; }

        public void Start()
        {
            List<Zone> zones = new List<Zone>();
            zones.Add(new Zone((0.5, 0.25), 0.1));

            var coordinates = new List<Coordinate>();
            var Forces = new List<Coordinate>();

            var currentIteration = 0;
            Coordinate coordinate = (0, 0);
            Coordinate velocity = (0, 0);

            while (currentIteration < Configuration.MaxIterationCount)
            {
                currentIteration++;
                coordinate += velocity * Configuration.TimePeriod;
                if (coordinate == (1, 1))
                {
                    SimReport = new SimReport(true, coordinate.LengthTo((1, 1)), velocity.GetLength(), currentIteration,
                        coordinates, Forces, ForceField);
                    return;
                }

                foreach (Zone zone in zones)
                {
                    if (zone.IsInZone(coordinate))
                    {
                        SimReport = new SimReport(false, coordinate.LengthTo((1, 1)), velocity.GetLength(), currentIteration,
                            coordinates, Forces, ForceField);
                        return;
                    }
                }
                Coordinate? force = GetForce(coordinate);
                if (force == null)
                {
                    SimReport = new SimReport(false, coordinate.LengthTo((1, 1)), velocity.GetLength(),
                        currentIteration, coordinates, Forces, ForceField);
                    return;
                }

                Forces.Add(force.Value);
                velocity += force.Value * Configuration.MaxForce * Configuration.TimePeriod;
                coordinates.Add(coordinate.WithEpsilon(Configuration.EpsilonInt));

                //TODO: check if in Zone
            }

            SimReport = new SimReport(false, coordinate.LengthTo((1, 1)), velocity.GetLength(), currentIteration,
                coordinates, Forces, ForceField);
        }

        private Coordinate? GetForce(Coordinate coordinate)
        {
            if (IsOutOfField(coordinate))
            {
                return null;
            }

            var dist = (1, 1) - coordinate;
            int degree = (int)(Math.Round(Math.Atan(dist.Y / dist.X)) / 3.14 * Configuration.DegreeCount);
            int len = (int)(dist.LengthTo((1, 1)) * (Configuration.SectionCount / 1.5));

            return ForceField.Field[degree, len];
        }

        private bool IsOutOfField(Coordinate coordinate)
        {
            return coordinate.X < 0 || coordinate.X > 1 || coordinate.Y < 0 || coordinate.Y > 1;
        }
    }
}