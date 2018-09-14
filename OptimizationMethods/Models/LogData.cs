using System.Collections.Generic;

namespace Lab1.Models
{
    public class LogData
    {
        public LogData(CountableFunc args, string title, double result, List<(double, int)> epsilonData)
        {
            Args = args;
            Title = title;
            Result = result;
            EpsilonData = epsilonData;
        }

        public CountableFunc Args { get; }
        public string Title { get; }
        public double Result { get; }
        public List<(double, int)> EpsilonData { get; }
    }
}