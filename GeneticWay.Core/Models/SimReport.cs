using System;
using System.Collections.Generic;

namespace GeneticWay.Core.Models
{
    public class SimReport
    {
        private readonly double _distance;
        public SimReport(bool isFinish, double distance, double finalSpeed, int iterationCount,
            List<Coordinate> coordinates, List<Coordinate> forces, ForceField field, List<Zone> zones)
        {
            IsFinish = isFinish;
            _distance = Math.Round(distance, Configuration.EpsilonInt);
            FinalSpeed = finalSpeed;
            IterationCount = iterationCount;
            Coordinates = coordinates;
            Forces = forces;
            Field = field;
            Zones = zones;
        }

        public bool IsFinish { get; }
        public double Distance => IsFinish ? 0 : _distance;
        public double FinalSpeed { get; }
        public int IterationCount { get; }
        public List<Coordinate> Coordinates { get; }
        public List<Coordinate> Forces { get; }
        public ForceField Field { get; }
        public List<Zone> Zones { get; }

        public override string ToString()
        {
            string res = IsFinish ? "+" : "-";
            return $"{res}|{Distance:F6}|{FinalSpeed:F6}|{IterationCount,8}";
        }
    }
}