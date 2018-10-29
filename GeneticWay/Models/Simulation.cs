using System.Collections.Generic;
using System.Windows.Documents;

namespace GeneticWay.Models
{
    public class SimulationPolygon
    {
        public ForceField ForceField { get; set; }

        public SimulationPolygon(ForceField field)
        {
            ForceField = field;
        }

        public SimReport Start()
        {
            List<Coordinate> coordinates = new List<Coordinate>();

            int currentIteration = 0;
            Coordinate coordinate = (0, 0);
            Coordinate velocity = (0, 0);
            while (currentIteration < Configuration.MaxIterationCount)
            {
                currentIteration++;

                coordinate += velocity * Configuration.TimePeriod;
                var force = GetForce(coordinate);
                if (force == null || IsOutOfField(coordinate))
                {
                    return new SimReport(false, coordinate.LengthTo((1, 1)), velocity.GetLength(), currentIteration, coordinates);
                }
                velocity += force;
                coordinates.Add(coordinate);

                if (coordinate == (1, 1))
                {
                    return new SimReport(true, 0, velocity.GetLength(), currentIteration, coordinates);
                }

                //TODO: check if in Zone
            }

            return new SimReport(false, coordinate.LengthTo((1, 1)), velocity.GetLength(), currentIteration, coordinates);
        }

        private Coordinate GetForce(Coordinate coordinate)
        {
            if (IsOutOfField(coordinate))
                return null;
            int x = GetIndex(Configuration.BlockCount, coordinate.X);
            int y = GetIndex(Configuration.BlockCount, coordinate.Y);
            return ForceField.Field[y][x];
        }

        private int GetIndex(int size, double coordinate)
        {
            return (int) coordinate * size;
        }

        private bool IsOutOfField(Coordinate coordinate)
        {
            return coordinate.X < 0 || coordinate.X > 1 || coordinate.Y < 0 || coordinate.Y > 1;
        }
    }
}