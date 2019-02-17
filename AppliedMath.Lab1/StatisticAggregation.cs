using System;
using System.Collections.Generic;
using System.Linq;

namespace AppliedMath.Lab1
{
    public class StatisticAggregation
    {
        private readonly List<int> _data;
        public StatisticAggregation(List<int> data)
        {
            _data = data;
        }

        public double AverageValue => _data.Average();

        public double Dispersion => CentralMoment(2);

        public double StandardError => Dispersion / Math.Sqrt(_data.Count);

        public double Mode()
        {
            var groups = _data.GroupBy(v => v).OrderBy(g => g.Count());
            int max = groups.First().Count();
            return groups.Where(g => g.Count() == max).Average(v => v.Key);
        }

        public int Median() => Median(_data);

        public int Quartile(QuartileType quartile)
        {
            if (quartile == QuartileType.Second)
                return Median();

            return Median(quartile == QuartileType.First
                ? _data.Take(_data.Count).ToList()
                : _data.Skip(_data.Count).ToList());
        }

        public double StandardDeviation => Math.Sqrt(Dispersion);

        public double Skewness => CentralMoment(3) / Math.Pow(StandardDeviation, 3);

        public double Kurtosis => CentralMoment(4) / Math.Pow(StandardDeviation, 4);

        public int Min => _data.Min();

        public int Max => _data.Max();

        private static int Median(List<int> data)
        {
            int left = data.Take(data.Count / 2).Last();
            int right = data.Skip(data.Count / 2).First();
            return (left + right) / 2;
        }

        private double CentralMoment(int n)
        {
            return _data.Select(x => Math.Pow(x - AverageValue, n)).Average();
        }
    }
}