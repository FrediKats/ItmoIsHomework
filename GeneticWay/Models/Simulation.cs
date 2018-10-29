using System.Collections.Generic;
using System.Windows.Documents;

namespace GeneticWay.Models
{
    public class SimulationPolygon
    {
        public ForceField ForceField { get; }
        public SimReport SimReport { get; set; }

        public SimulationPolygon(ForceField field)
        {
            ForceField = field;
        }

        public void Start()
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
                if (force == (-1, -1) || IsOutOfField(coordinate))
                {
                    SimReport = new SimReport(false, coordinate.LengthTo((1, 1)), velocity.GetLength(), currentIteration, coordinates);
                    return;
                }
                velocity += force;
                coordinates.Add(coordinate);

                if (coordinate == (1, 1))
                {
                    SimReport = new SimReport(true, 0, velocity.GetLength(), currentIteration, coordinates);
                    return;
                }

                //TODO: check if in Zone
            }

            SimReport = new SimReport(false, coordinate.LengthTo((1, 1)), velocity.GetLength(), currentIteration, coordinates);
            return;
        }

        private Coordinate GetForce(Coordinate coordinate)
        {
            if (IsOutOfField(coordinate))
                return (-1, -1);
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