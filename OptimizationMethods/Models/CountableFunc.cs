using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1.Models
{
    public class CountableFunc
    {
        private readonly Func<double, double> _function;

        public CountableFunc(Func<double, double> function, double left, double right, double epsilon)
        {
            _function = function;
            Left = left;
            Right = right;
            Epsilon = epsilon;

            var interval = Right - Left;
            IntervalData.Add(new FuncRecord(interval, 1, _function(Left), _function(Right)));
            CallCount = 0;
        }

        public List<FuncRecord> IntervalData { get; } = new List<FuncRecord>();

        public Func<double, double> Function
        {
            get
            {
                CallCount++;
                return _function;
            }
        }

        public int CallCount { get; private set; }

        public double Left { get; set; }
        public double Right { get; set; }
        public double Epsilon { get; }

        public void SaveData()
        {
            var last = IntervalData.Last();
            var interval = Right - Left;
            IntervalData.Add(new FuncRecord(interval, interval / last.Interval, _function(Left), _function(Right)));
        }
    }
}