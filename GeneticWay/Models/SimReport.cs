using System;
using System.Collections.Generic;

namespace GeneticWay.Models
{
    public class SimReport
    {
        public SimReport(bool isFinish, double distance, double finalSpeed, int iterationCount,
            List<Coordinate> coordinates, List<Coordinate> forces, ForceField field)
        {
            IsFinish = isFinish;
            Distance = Math.Round(distance, 6);
            FinalSpeed = finalSpeed;
            IterationCount = iterationCount;
            Coordinates = coordinates;
            Forces = forces;
            Field = field;
        }

        public bool IsFinish { get; }
        public double Distance { get; }
        public double FinalSpeed { get; }
        public int IterationCount { get; }
        public List<Coordinate> Coordinates { get; }
        public List<Coordinate> Forces { get; }
        public ForceField Field { get; }

        public override string ToString()
        {
            string res = IsFinish ? "+" : "-";
            return $"{res}|{Distance:F6}|{FinalSpeed:F6}|{IterationCount,8}";
        }
    }
}