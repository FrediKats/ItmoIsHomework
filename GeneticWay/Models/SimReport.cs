using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace GeneticWay.Models
{
    public class SimReport
    {
        public SimReport(bool isFinish, double distance, double finalSpeed, int iterationCount, List<Coordinate> coordinates)
        {
            IsFinish = isFinish;
            Distance = Math.Round(distance, 5);
            FinalSpeed = finalSpeed;
            IterationCount = iterationCount;
            Coordinates = coordinates;
        }
        
        public bool IsFinish { get; }
        public double Distance { get; }
        public double FinalSpeed { get; }
        public int IterationCount { get; }
        public List<Coordinate> Coordinates{ get; set; }

        public override string ToString()
        {
            string res = IsFinish ? "+" : "-";
            return $"{res}|{Distance:F6}|{FinalSpeed:F6}|{IterationCount, 8}";
        }
    }
}