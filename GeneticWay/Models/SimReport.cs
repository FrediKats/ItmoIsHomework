using System.Collections.Generic;
using System.Windows.Documents;

namespace GeneticWay.Models
{
    public class SimReport
    {
        public SimReport(bool isFinish, float distance, float finalSpeed, int iterationCount, List<Coordinate> coordinates)
        {
            IsFinish = isFinish;
            Distance = distance;
            FinalSpeed = finalSpeed;
            IterationCount = iterationCount;
            Coordinates = coordinates;
        }

        public float Points
        {
            get
            {
                if (FinalSpeed > 0)
                {
                    return Distance * FinalSpeed;
                }
                else
                {
                    return Distance * 100;
                }
            }
        }
        
        public bool IsFinish { get; }
        public float Distance { get; }
        public float FinalSpeed { get; }
        public int IterationCount { get; }
        public List<Coordinate> Coordinates{ get; set; }

        public override string ToString()
        {
            string res = IsFinish ? "+" : "-";
            return $"{res}|{Distance:F6}|{FinalSpeed:F6}|{IterationCount, 8}";
        }
    }
}